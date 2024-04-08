using UnityEngine;
using Cinemachine;

public class Cinematics : MonoBehaviour
{
    public Cinematics instance;
    void Start()
    {
        instance = this;
    }


    public void PlayCinematic(CinemachineVirtualCamera virtualCamera)
    {
        virtualCamera.Priority = 20;
    }
    
    public void StopCinematic(CinemachineVirtualCamera virtualCamera)
    {
        virtualCamera.Priority = 0;
    }
}
