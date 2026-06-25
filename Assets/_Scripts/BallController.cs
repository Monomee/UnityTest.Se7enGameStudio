using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public static BallController instance;
    private void OnEnable()
    {
        instance = this;
    }
    private void OnDisable()
    {
        instance = null;
    }
    [Header("Setting")]
    [SerializeField]private bool shootStraight;
    [SerializeField] private float speedWhenShootStraight; 

    [Header("Binding")]
    public PlayerController player;
    public Transform goal1;
    public Transform goal2;
    public Transform spawnPoint;

    private Transform targetGoal;
    public float offset = 0.5f;

    public Transform ball;
    public Ball[] balls;

    private void Awake()
    {
        balls = FindObjectsByType<Ball>(FindObjectsSortMode.None);
    }
    public void KickBall()
    {
        if (ball == null || goal1 == null || goal2 == null) return;

        Shoot();
    }
    public void AutoKickBall()
    {
        if (goal1 == null || goal2 == null) return;

        ball = GetFurthestBall().transform;

        CameraController.instance.ChangeCamera();

        //Shoot();
    }
    private void Shoot()
    {
        targetGoal = (CalculateDistance(ball, goal1) >= CalculateDistance(ball, goal2)) ? goal2 : goal1;
        Vector3 target = new Vector3(targetGoal.position.x, targetGoal.position.y + offset, targetGoal.position.z);

        if (shootStraight)
        {
            ball.DOMove(target, speedWhenShootStraight)
            .SetSpeedBased()
            .SetEase(Ease.OutExpo)
            .OnComplete(() => {
                player.isKicking = false;
            });
        }
        else
        {
            ball.DOJump(target, jumpPower: 3f, numJumps: 1, duration: 1f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                player.isKicking = false;
            });
        }
    }
    private float CalculateDistance(Transform ob1,  Transform ob2)
    {
        return Vector3.Distance(ob1.position, ob2.position);
    }
    private Transform GetFurthestBall()
    {
        float furthestDistance = 0f;
        Ball furthestBall = null;
        foreach (var ball in balls)
        {
            float dis = CalculateDistance(player.transform, ball.transform);
            if (dis > furthestDistance)
            {
                furthestDistance = dis;
                furthestBall = ball;
            }
        }
        return furthestBall.transform;
    }
}
