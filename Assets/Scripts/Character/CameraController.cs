
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private GameObject _target;

    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private PlayerData playerData;

    [SerializeField] private LayerMask _layerMask;

    private int maxLookAngle = 35;
    private int minLookAngle = -30;
    private int mouseSensitivity = 1;

    private float smoothTime = 10f;
    private Vector3 _camera_offset;

    void Start()
    {
        mouseSensitivity = playerData.MouseSensitivity;
        maxLookAngle = playerData.MaxLookAngle;
        minLookAngle = playerData.MinLookAngle;

        _camera_offset = transform.localPosition;

    }

    void Update()
    {
        if (PlayerController.StopPlayer) return;
        RotateCamera();
        /*CameraMovement();*/
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

/*    private void CameraMovement()
    {
*//*        if (CheckObstacleToTarget())
        {
            Debug.Log("il y'a des obstacles");
            Vector3 destination = Vector3.up;
            if ((destination - _cameraTransform.position).magnitude < 1) return;
            *//*_cameraTransform.localPosition = Mathf.Lerp(_cameraTransform.localPosition.z, 1, 0.1f);*//*
            Debug.Log(_cameraTransform.position);
        }
        else
        {
            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, _camera_offset, Time.deltaTime);
            Debug.Log("pas d'obstacle");
        }*//*
    }

    private bool CheckObstacleToTarget()
    {
        bool isObstacle = false;
        Vector3 origin = _cameraTransform.position;
        Vector3 destination = _target.transform.position +Vector3.up;
        Vector3 direction = (destination - origin).normalized;
        float distanceMax = (origin - destination).magnitude;

        Debug.DrawLine(origin, origin+direction * distanceMax, Color.red);
        *//*isObstacle = Physics.SphereCast(origin, 1, direction, out var hitInfo, distanceMax,1<<0);*//*
        isObstacle = Physics.Raycast(origin, direction, out var hitInfo, distanceMax,_layerMask);
        return isObstacle;
    }*/
}
