using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public float speed;
        public GameObject throwingSnowballPrefab;
        public Transform throwingPoint;
        public float throwAngle;
    
        private InputControls _inputControls;
        private Vector2 _moveInput;
        private Vector2 _mousePosition;
        private Rigidbody _rb;
        private Camera _camera;

        private void Awake()
        {
            _inputControls = new InputControls();
            _rb = GetComponent<Rigidbody>();
            _camera = Camera.main;

            _inputControls.Gameplay.Move.performed += context => _moveInput = context.ReadValue<Vector2>();
            _inputControls.Gameplay.Move.canceled += _ => _moveInput = Vector2.zero;
            _inputControls.Gameplay.MousePosition.performed += context => _mousePosition = context.ReadValue<Vector2>();
            _inputControls.Gameplay.ThrowSnowball.performed += _ => ThrowSnowball();
        }

        private void OnEnable()
        {
            _inputControls.Enable();
        }

        private void OnDisable()
        {
            _inputControls.Disable();
        }

        private void FixedUpdate()
        {
            var move = new Vector3(_moveInput.x, 0, _moveInput.y) * speed;
            _rb.MovePosition(_rb.position + move * Time.fixedDeltaTime);
        
            RotateTowardsMouse();
        }

        private void RotateTowardsMouse()
        {
            var ray = _camera.ScreenPointToRay(_mousePosition);

            if (!Physics.Raycast(ray, out var hit)) return;
            var target = hit.point;
            var trans = transform.position;
            target.y = trans.y;
            var direction = (target - trans).normalized;
            var lookRotation = Quaternion.LookRotation(direction);
            _rb.MoveRotation(lookRotation);
        }

        private void ThrowSnowball()
        {
            if (throwingSnowballPrefab == null && throwingPoint == null) return;
        
            var ray = _camera.ScreenPointToRay((_mousePosition));
            if (!Physics.Raycast(ray, out var hit)) return;

            var targetPosition = hit.point;
            var throwingSnowball = Instantiate(throwingSnowballPrefab, throwingPoint.position, Quaternion.identity);

            var snowballRb = throwingSnowball.GetComponent<Rigidbody>();
            if (snowballRb == null) return;
        
            var throwVelocity = CalculateThrowVelocity(throwingPoint.position, targetPosition);
            snowballRb.velocity = throwVelocity;
        }
    
        private Vector3 CalculateThrowVelocity(Vector3 origin, Vector3 target)
        {
            var toTarget = target - origin;
            var toTargetXZ = toTarget;
            toTargetXZ.y = 0;

            var x = toTargetXZ.magnitude;
            var y = toTarget.y;
            var g = Physics.gravity.magnitude;
            var angle = Mathf.Deg2Rad * throwAngle;

            if (x <= Mathf.Epsilon) return Vector3.zero;

            var v0 = Mathf.Sqrt(g * x * x / (2 * Mathf.Cos(angle) * Mathf.Cos(angle) * (x * Mathf.Tan(angle) - y)));

            if (float.IsNaN(v0)) return Vector3.zero;

            var velocity = new Vector3(toTargetXZ.normalized.x * v0 * Mathf.Cos(angle), v0 * Mathf.Sin(angle),
                toTargetXZ.normalized.z * v0 * Mathf.Cos(angle));
    
            return velocity;
        }
    }
}
