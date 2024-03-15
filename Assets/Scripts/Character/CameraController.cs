using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{

    [SerializeField] private GameObject _target;
    [SerializeField] private PlayerData playerData;

    private int maxLookAngle = 35;
    private int minLookAngle = -30;
    private int mouseSensitivity = 1;



    void Start()
    {
        mouseSensitivity = playerData.MouseSensitivity;
        maxLookAngle = playerData.MaxLookAngle;
        minLookAngle = playerData.MinLookAngle;

    }

    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        var yaw = mouseSensitivity * Input.GetAxis("Mouse X");

        var pitch = -mouseSensitivity * Input.GetAxis("Mouse Y");

        transform.localEulerAngles += new Vector3(0, yaw, 0);

        var rotation = transform.localEulerAngles + new Vector3(pitch, 0, 0);

        rotation.x = PlayerController.ClampRotation(rotation.x, minLookAngle, maxLookAngle);

        transform.localEulerAngles = rotation;

        transform.position = _target.transform.position;
    }
}
