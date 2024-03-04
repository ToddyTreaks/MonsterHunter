using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] float speed = 30f;
    [SerializeField] private GameObject rotateCamera;
    private int _mouseSensitivity = 1;
    private int _maxLookAngle = 35;
    private int _minLookAngle = -30;

    private CharacterController _characterController;

    private float _gravityValue = 10f;
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        if ( _characterController == null ) Debug.LogError("there is no CharacterController on player");
    }

    // Update is called once per frame
    void Update()
    {

        Rotate();
    }

    void FixedUpdate()
    {
        Move();
        applyGravity();
    }

    private void Move()
    {
        var dirVertical = Input.GetAxisRaw("Vertical")*transform.forward;
        var dirHorizontal = Input.GetAxisRaw("Horizontal")*transform.right;

        _characterController.Move(speed * Time.deltaTime *(dirHorizontal + dirVertical).normalized);
    }

    private void applyGravity()
    {
        _characterController.Move(_gravityValue*Time.deltaTime * Vector3.down);
    }

    private void Rotate()
    {
        var yaw = _mouseSensitivity * Input.GetAxis("Mouse X");

        var pitch = -_mouseSensitivity * Input.GetAxis("Mouse Y");

        // Clamp pitch between lookAngle

        transform.localEulerAngles += new Vector3(0, yaw, 0);

        var rotation = rotateCamera.transform.localEulerAngles + new Vector3(pitch, 0, 0);

        rotation.x = ClampRotation(rotation.x, _minLookAngle, _maxLookAngle);

        rotateCamera.transform.localEulerAngles = rotation;
    }

    private static float ClampRotation(float angle, float min, float max)
    {
        if (angle < 0) angle += 360;
        return angle > 180f ? Mathf.Max(angle, 360 + min) : Mathf.Min(angle, max);
    }
}
