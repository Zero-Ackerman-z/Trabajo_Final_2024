using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera playerCamera;
    public CinemachineVirtualCamera opponentCamera;
    public CinemachineVirtualCamera duetCamera;

    public void MoveCameraToPlayer()
    {
        playerCamera.Priority = 10;
        opponentCamera.Priority = 5;
        duetCamera.Priority = 5;
    }

    public void MoveCameraToOpponent()
    {
        playerCamera.Priority = 5;
        opponentCamera.Priority = 10;
        duetCamera.Priority = 5;
    }

    public void MoveCameraToDuet()
    {
        playerCamera.Priority = 5;
        opponentCamera.Priority = 5;
        duetCamera.Priority = 10;
    }
}
