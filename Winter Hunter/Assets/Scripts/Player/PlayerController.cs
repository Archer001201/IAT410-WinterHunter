using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Parameters")]
        public float moveSpeed;
        public float rotationSpeed = 5.0f;
        public float staminaRecoveryTimer;
        [Header("Player State")]
        public bool isRollingSnowball = false;
        
        private InputControls _inputControls;
        private Vector2 _moveInput;
        private Vector2 _mousePosition;
        private Rigidbody _rb;
        private Camera _camera;
        private Coroutine _staminaCoroutine;

        private ThrowSnowball _throwSnowballScript;
        private RollSnowball _rollSnowballScript;
        private PlayerAttribute _playerAttr;
        
        private void Awake()
        {
            _inputControls = new InputControls();
            _rb = GetComponent<Rigidbody>();
            _throwSnowballScript = GetComponent<ThrowSnowball>();
            _rollSnowballScript = GetComponent<RollSnowball>();
            _playerAttr = GetComponent<PlayerAttribute>();
            _camera = Camera.main;

            _inputControls.Gameplay.Move.performed += context => _moveInput = context.ReadValue<Vector2>();
            _inputControls.Gameplay.Move.canceled += _ => _moveInput = Vector2.zero;
            _inputControls.Gameplay.MousePosition.performed += context => _mousePosition = context.ReadValue<Vector2>();
            _inputControls.Gameplay.ThrowSnowball.performed += _ => OnThrowingSnowballStart();
            _inputControls.Gameplay.ThrowSnowball.canceled += _ => OnThrowingSnowballEnd();
            _inputControls.Gameplay.RollSnowball.performed += _ => OnRollingSnowballStart();
            _inputControls.Gameplay.RollSnowball.canceled += _ => OnRollingSnowballEnd();

            _rollSnowballScript.enabled = false;
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
            var moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y).normalized;
            var currentVerticalVelocity = _rb.velocity.y;

            _rb.velocity = new Vector3(moveDirection.x * moveSpeed, currentVerticalVelocity, moveDirection.z * moveSpeed);
            
            if (!isRollingSnowball)
            {
                RotateTowardsMouse();
                _throwSnowballScript.UpdateAimingLine();
            }
            else
            {
                if (moveDirection != Vector3.zero)
                {
                    RotateTowardMovingDirection(moveDirection);
                }
                _rollSnowballScript.UpdateSnowball(moveDirection);
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
        
        private void RotateTowardMovingDirection(Vector3 moveDir)
        {
            var targetRotation = Quaternion.LookRotation(moveDir);
            _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
        }

        private void OnThrowingSnowballStart()
        {
            StopStaminaCoroutine();
            _throwSnowballScript.Attack();
        }

        private void OnThrowingSnowballEnd()
        {
            StartStaminaCoroutine();
        }

        private void OnRollingSnowballStart()
        {
            StopStaminaCoroutine();
            
            _rollSnowballScript.enabled = true;
            _throwSnowballScript.enabled = false;
            
            isRollingSnowball = true;
            _rollSnowballScript.CreateSnowball();
        }

        public void OnRollingSnowballEnd()
        {
            isRollingSnowball = false;
            _rollSnowballScript.Attack();
            
            _throwSnowballScript.enabled = true;
            _rollSnowballScript.enabled = false;
            
            StartStaminaCoroutine();
        }

        private IEnumerator RecoverStaminaAfterDelay()
        {
            yield return new WaitForSeconds(staminaRecoveryTimer);
            while (_playerAttr.stamina < _playerAttr.maxStamina)
            {
                _playerAttr.stamina += _playerAttr.staminaRecovery * Time.deltaTime;
                yield return null;
            }
        }

        private void StartStaminaCoroutine()
        {
            _staminaCoroutine ??= StartCoroutine(RecoverStaminaAfterDelay());
        }

        private void StopStaminaCoroutine()
        {
            if (_staminaCoroutine != null)
            {
                StopCoroutine(_staminaCoroutine);
                _staminaCoroutine = null;
            }
        }
    }
}
