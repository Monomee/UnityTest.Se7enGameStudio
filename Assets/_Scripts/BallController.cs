using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    public Transform nearBall;
    public Transform targetBall;
    public Ball[] balls;

    private void Awake()
    {
        balls = FindObjectsByType<Ball>(FindObjectsSortMode.None);
    }
    public void KickBall()
    {
        if (targetBall == null || goal1 == null || goal2 == null) return;

        UIManager.Instance.SetActiveKickButton(false);
        UIManager.Instance.SetActiveAutoKickButton(false);

        Shoot();
    }
    public void AutoKickBall()
    {
        if (goal1 == null || goal2 == null) return;

        UIManager.Instance.SetActiveKickButton(false);
        UIManager.Instance.SetActiveAutoKickButton(false);

        targetBall = null;
        targetBall = GetFurthestBall().transform;

        CameraController.instance.ChangeCamera();

        //Shoot();
    }
    private void Shoot()
    {
        targetGoal = (CalculateDistance(targetBall, goal1) >= CalculateDistance(targetBall, goal2)) ? goal2 : goal1;
        Vector3 target = new Vector3(targetGoal.position.x, targetGoal.position.y + offset, targetGoal.position.z);

        if (shootStraight)
        {
            targetBall.DOMove(target, speedWhenShootStraight)
            .SetSpeedBased()
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                ReleaseAfterShoot();
            });
        }
        else
        {
            targetBall.DOJump(target, jumpPower: 3f, numJumps: 1, duration: 1f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                ReleaseAfterShoot();
            });
        }
    }
    private void ReleaseAfterShoot()
    {
        player.isKicking = false;
        targetBall = null;
        UIManager.Instance.SetActiveKickButton(true);
        UIManager.Instance.SetActiveAutoKickButton(true);
    }
    private float CalculateDistance(Transform ob1,  Transform ob2)
    {
        return Vector3.Distance(ob1.position, ob2.position);
    }
    private Transform GetFurthestBall()
    {
        if (balls == null || balls.Count() == 0) return null;

        float furthestSqrDistance = 0f;
        Ball furthestBall = null;
        Vector3 playerPos = player.transform.position;

        foreach (var ball in balls)
        {
            if (ball == null) continue;

            Vector3 direction = ball.transform.position - playerPos;
            float sqrDis = direction.sqrMagnitude;

            if (sqrDis >= furthestSqrDistance)
            {
                furthestSqrDistance = sqrDis;
                furthestBall = ball;
            }
        }
        return furthestBall.transform;
    }
    public void SetShootStyle(Toggle toggle) => shootStraight = toggle.isOn;
}
