
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
        CameraMovement();
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

    private void CameraMovement()
    {
        if (CheckObstacleToTarget())
        {
            Debug.Log("il y'a des obstacles");
        }
        else
        {
            Debug.Log("pas d'obstacle");
        }
    }

    private bool CheckObstacleToTarget()
    {
        bool isObstacle =false;
        Vector3 origin = _cameraTransform.position;
        Vector3 destination = _target.transform.position;
        Vector3 direction = -(origin - destination).normalized;
        float distanceMax = (origin - destination).magnitude - 1f;

        Debug.DrawRay(origin,direction,Color.red);
        isObstacle = Physics.Raycast(origin, direction, out var hitInfo, distanceMax);
        return isObstacle;
    }
}
