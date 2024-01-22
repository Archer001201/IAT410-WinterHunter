using System;
using Snowball;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Attributes")]
        public float moveSpeed;
        public float rotationSpeed = 5.0f;
        [Header("Throwing Snowballs")]
        public GameObject throwingSnowballPrefab;
        public Transform throwPoint;
        public float throwForce;
        public LineRenderer throwingLineRenderer;
        public int lineSegmentCount = 20;
        [Header("Rolling Snowballs")]
        public bool isRollingSnowball;
        public GameObject rollingSnowballPrefab;
        public Transform rollPoint;
        public float pushForce;
        public float rollingSnowballScaleFactor = 0.1f;
        public GameObject rollingLine;
        
        private InputControls _inputControls;
        private Vector2 _moveInput;
        private Vector2 _mousePosition;
        private Rigidbody _rb;
        private Camera _camera;
        private GameObject _rollingSnowball;
        private RollingSnowball _rollingSnowballScript;
        private Rigidbody _rollingSnowballRb;
        
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
            throwingLineRenderer.enabled = !isRollingSnowball;
            rollingLine.SetActive(isRollingSnowball);
        }

        private void FixedUpdate()
        {
            var moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y).normalized;
            var currentVerticalVelocity = _rb.velocity.y;

            _rb.velocity = new Vector3(moveDirection.x * moveSpeed, currentVerticalVelocity, moveDirection.z * moveSpeed);
            
            if (!isRollingSnowball)
            {
                RotateTowardsMouse();
                UpdateThrowingLine();
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
                if (_rollingSnowball == null) return;
                UpdateRollingLine();
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
        
        private void UpdateThrowingLine()
        {
            var points = new Vector3[lineSegmentCount];
            var startingPosition = throwPoint.position;
            var startingVelocity = throwPoint.forward * throwForce;

            for (var i = 0; i < lineSegmentCount; i++)
            {
                var t = i / (float)lineSegmentCount;
                points[i] = startingPosition + t * startingVelocity;
                points[i].y = startingPosition.y + t * startingVelocity.y + 0.5f * Physics.gravity.y * t * t;
            }

            throwingLineRenderer.positionCount = lineSegmentCount;
            throwingLineRenderer.SetPositions(points);
        }

        private void UpdateRollingLine()
        {
            var rollingDistance = _rollingSnowballScript.rollingDistance - 5f;
            rollingLine.transform.localScale = new Vector3(_rollingSnowball.transform.localScale.x,1,rollingDistance);
            rollingLine.transform.position = rollPoint.position;
            rollingLine.transform.rotation = rollPoint.rotation;
        }

        private void RollSnowball()
        {
            isRollingSnowball = true;
            if (rollingSnowballPrefab == null || rollPoint == null) return;
            _rollingSnowball = Instantiate(rollingSnowballPrefab, rollPoint.position, rollPoint.rotation);
            if (_rollingSnowball == null) return;
            _rollingSnowballScript = _rollingSnowball.GetComponent<RollingSnowball>();
            _rollingSnowballRb = _rollingSnowball.GetComponent<Rigidbody>();
        }

        public void ReleaseSnowball()
        {
            isRollingSnowball = false;
            if (_rollingSnowball == null) return;
            _rollingSnowballScript.SetReleasingState();
            var pushDirection = rollPoint.forward;
            _rollingSnowballRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            
            _rollingSnowball = null;
            Debug.Log("released");
        }
    }
}
