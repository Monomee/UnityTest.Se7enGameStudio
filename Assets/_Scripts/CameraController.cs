using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    private void OnEnable()
    {
        instance = this;
    }
    private void OnDisable()
    {
        instance = null;
    }

    public CinemachineVirtualCamera followBallCamera;
    public PlayerController player;

    [Header("Follow Ball Setting")]
    [SerializeField] private Vector3 followBallOffset;
    [SerializeField] private float followBallFOV;

    // method to use
    public void ChangeCameraToTargetBall()
    {
        followBallCamera.Follow = BallController.instance.ball;
        followBallCamera.LookAt = BallController.instance.ball;
        followBallCamera.Priority = 11;
    }
    public void ChangeCameraBackToPlayer()
    {
        followBallCamera.Priority = 0;
    }
    public void DetachBallFromCamera()
    {
        followBallCamera.Follow = null;
        followBallCamera.LookAt = null;
    }
    public void ChangeCamera()
    {
        StartCoroutine(ChangeCameraCoroutine());
    }
    private IEnumerator ChangeCameraCoroutine()
    {
        ChangeCameraToTargetBall();

        yield return new WaitForSeconds(1f);

        player.isKicking = true;
    }
}
