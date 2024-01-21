using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed;
        public GameObject throwingSnowballPrefab;
        public GameObject rollingSnowballPrefab;
        public Transform throwPoint;
        public Transform rollPoint;
        public float throwForce;
        public float pushForce;
        public bool isRollingSnowball;
        public float rotationSpeed = 5.0f;
        public float rollingSnowballScaleFactor = 0.1f;
        
        private InputControls _inputControls;
        private Vector2 _moveInput;
        private Vector2 _mousePosition;
        private Rigidbody _rb;
        private Camera _camera;
        private GameObject _rollingSnowball;
        
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
            _inputControls.Gameplay.RollSnowball.performed += _ => RollSnowball();
            _inputControls.Gameplay.RollSnowball.canceled += _ => ReleaseSnowball();
        }

        private void OnEnable()
        {
            _inputControls.Enable();
        }

        private void OnDisable()
        {
            _inputControls.Disable();
        }

        private void Update()
        {
            aimingLineRenderer.enabled = !isRollingSnowball;
        }

        private void FixedUpdate()
        {
            var moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y).normalized;
            var currentVerticalVelocity = _rb.velocity.y;

            _rb.velocity = new Vector3(moveDirection.x * moveSpeed, currentVerticalVelocity, moveDirection.z * moveSpeed);
            
            if (!isRollingSnowball)
            {
                RotateTowardsMouse();
                UpdateAimingLine();
            }
            else
            {
                if (moveDirection != Vector3.zero)
                {
                    var targetRotation = Quaternion.LookRotation(moveDirection);
                    _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
                    var scaleIncrease = new Vector3(rollingSnowballScaleFactor, rollingSnowballScaleFactor, rollingSnowballScaleFactor) * Time.fixedDeltaTime;
                    _rollingSnowball.transform.localScale += scaleIncrease;
                }
                _rollingSnowball.transform.position = rollPoint.position;
                _rollingSnowball.transform.rotation = rollPoint.rotation;
            }
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
            _rb.rotation = Quaternion.Slerp(_rb.rotation, lookRotation, Time.fixedDeltaTime * rotationSpeed);
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

            for (var i = 0; i < lineSegmentCount; i++)
            {
                var t = i / (float)lineSegmentCount;
                points[i] = startingPosition + t * startingVelocity;
                points[i].y = startingPosition.y + t * startingVelocity.y + 0.5f * Physics.gravity.y * t * t;
            }

            aimingLineRenderer.positionCount = lineSegmentCount;
            aimingLineRenderer.SetPositions(points);
        }

        private void RollSnowball()
        {
            isRollingSnowball = true;
            if (rollingSnowballPrefab == null || rollPoint == null) return;
            _rollingSnowball = Instantiate(rollingSnowballPrefab, rollPoint.position, rollPoint.rotation);
        }

        private void ReleaseSnowball()
        {
            isRollingSnowball = false;
            var snowballRb = _rollingSnowball.GetComponent<Rigidbody>();
            if (snowballRb == null) return;
            var pushDirection = rollPoint.forward;
            snowballRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            
            _rollingSnowball = null;
        }
    }
}
