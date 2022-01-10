using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private Transform _cameraTarget;
    public float DistanceFromTarget = 2f;

    public bool LockCursor;
    public float MouseSensitivity = 5f;
    private float _yaw;
    private float _pitch;
    public Vector2 PitchMinMax = new Vector2(-40f, 85f);

    public float RotationSmoothTime = 0.12f;
    private Vector3 _rotationSmoothVelocity;
    private Vector3 _currentRotation;

    private void Start()
    {
        if (LockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        GameObject Parent = transform.parent.gameObject;
        _cameraTarget = Parent.transform.Find("FirstCamera").gameObject.transform;
    }

    private void LateUpdate()
    {
        _yaw += Input.GetAxis("Mouse X") * MouseSensitivity;
        _pitch -= Input.GetAxis("Mouse Y") * MouseSensitivity;
        _pitch = Mathf.Clamp(_pitch, PitchMinMax.x, PitchMinMax.y);

        _currentRotation = Vector3.SmoothDamp(_currentRotation, new Vector3(_pitch, _yaw), ref _rotationSmoothVelocity, RotationSmoothTime);
        transform.eulerAngles = _currentRotation;

        transform.position = _cameraTarget.position - transform.forward * DistanceFromTarget;
    }
}
