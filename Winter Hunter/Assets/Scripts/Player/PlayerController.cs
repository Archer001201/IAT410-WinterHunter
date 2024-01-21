using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed;
        public GameObject throwingSnowballPrefab;
        public Transform throwPoint;
        public float throwForce;
    
        private InputControls _inputControls;
        private Vector2 _moveInput;
        private Vector2 _mousePosition;
        private Rigidbody _rb;
        private Camera _camera;
        
        public LineRenderer aimingLineRenderer;
        public int lineSegmentCount = 20;

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
            var move = new Vector3(_moveInput.x, 0, _moveInput.y) * moveSpeed;
            _rb.MovePosition(_rb.position + move * Time.fixedDeltaTime);
        
            RotateTowardsMouse();
            UpdateAimingLine();
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
            if (throwingSnowballPrefab == null || throwPoint == null) return;
            var throwingSnowball = Instantiate(throwingSnowballPrefab, throwPoint.position, throwPoint.rotation);
            
            var snowballRb = throwingSnowball.GetComponent<Rigidbody>();
            if (snowballRb == null) return;
            
            var throwDirection = throwPoint.forward;
            
            snowballRb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }
        
        private void UpdateAimingLine()
        {
            if (aimingLineRenderer == null) return;

            var points = new Vector3[lineSegmentCount];
            var startingPosition = throwPoint.position;
            var startingVelocity = throwPoint.forward * throwForce;

            for (int i = 0; i < lineSegmentCount; i++)
            {
                float t = i / (float)lineSegmentCount;
                points[i] = startingPosition + t * startingVelocity;
                points[i].y = startingPosition.y + t * startingVelocity.y + 0.5f * Physics.gravity.y * t * t;
            }

            aimingLineRenderer.positionCount = lineSegmentCount;
            aimingLineRenderer.SetPositions(points);
        }


    }
}
