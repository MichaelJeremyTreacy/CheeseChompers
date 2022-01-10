using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // First person variables
    public float MouseSensitivityX = 250f;
    public float MouseSensitivityY = 250f;

    private float _verticalLookRotation;
    public Vector2 PitchMinMax = new Vector2(-75f, 65f);

    private Vector3 _moveAmount;
    private Vector3 _smoothMoveVelocity;

    private Camera _firstCamera;

    // Third person variables
    private float _targetMoveSpeed;
    private float _currentMoveSpeed;

    public float SpeedSmoothTime = 0.1f;
    private float _speedSmoothVelocity;

    public float TurnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;

    private Camera _thirdCamera;

    // Other
    public float DefaultMoveSpeed = 12f;
    private Animator _animator;
    private Rigidbody _rigidbody;

    public IPlayerInput PlayersInput { get; set; }

    private void Start()
    {
        GameObject firstCamGameObject = transform.Find("FirstCamera").gameObject;
        GameObject thirdCamGameObject = transform.Find("ThirdCamera").gameObject;
        _firstCamera = firstCamGameObject.GetComponent<Camera>();
        _thirdCamera = thirdCamGameObject.GetComponent<Camera>();
        SetFirstCamera(true);

        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

        if (PlayersInput == null)
        {
            Debug.Log("Finding PlayerInput");

            PlayersInput = (IPlayerInput)FindObjectOfType<PlayerInput>();

            Debug.Assert(PlayersInput != null, "PlayerInput is null");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_firstCamera.enabled)
            {
                SetFirstCamera(false);
            }
            else if (!_firstCamera.enabled)
            {
                SetFirstCamera(true);
            }
        }

        if (_firstCamera.enabled)
        {
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * MouseSensitivityX);
            _verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * MouseSensitivityY;
            _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, PitchMinMax.x, PitchMinMax.y);
            _firstCamera.transform.localEulerAngles = Vector3.left * _verticalLookRotation;

            Vector3 moveDir = new Vector3(PlayersInput.Horizontal, 0f, PlayersInput.Vertical).normalized;
            Vector3 targetMoveAmount = moveDir * DefaultMoveSpeed;
            _moveAmount = Vector3.SmoothDamp(_moveAmount, targetMoveAmount, ref _smoothMoveVelocity, 0.15f);
        }
        else if (!_firstCamera.enabled)
        {
            Vector2 input = new Vector2(PlayersInput.Horizontal, PlayersInput.Vertical);
            Vector2 inputDir = input.normalized;

            if (inputDir != Vector2.zero)
            {
                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + _thirdCamera.transform.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _turnSmoothVelocity, TurnSmoothTime);
            }

            _targetMoveSpeed = DefaultMoveSpeed * inputDir.magnitude;
            _currentMoveSpeed = Mathf.SmoothDamp(_currentMoveSpeed, _targetMoveSpeed, ref _speedSmoothVelocity, SpeedSmoothTime);

            transform.Translate(transform.forward * _currentMoveSpeed * Time.deltaTime, Space.World);
        }

        _animator.SetBool("IsMoving", PlayersInput.Horizontal != 0f || PlayersInput.Vertical != 0f);

        CheckScreenshot();
    }

    private void FixedUpdate()
    {
        if (_firstCamera.enabled)
        {
            _rigidbody.MovePosition(_rigidbody.position + transform.TransformDirection(_moveAmount) * Time.fixedDeltaTime);
        }
    }

    public void SetFirstCamera(bool Enable)
    {
        if (Enable)
        {
            _firstCamera.enabled = true;
            _thirdCamera.enabled = false;
        }
        else if (!Enable)
        {
            _thirdCamera.enabled = true;
            _firstCamera.enabled = false;
        }
    }

    private void CheckScreenshot()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ScreenCapture.CaptureScreenshot("D:/Repos/CheeseChompers/Screenshots/FirstPerson.png");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ScreenCapture.CaptureScreenshot("D:/Repos/CheeseChompers/Screenshots/ThirdPerson.png");
        }
    }
}
