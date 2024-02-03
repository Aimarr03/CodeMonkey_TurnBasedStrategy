using System;
using Unity.VisualScripting;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    private const string ISWALKING = "IsWalking";
    private const string SHOOT = "Shoot";
    [SerializeField] private Animator animator;
    [SerializeField] private Transform PointBlankShoot;
    [SerializeField] private Transform BulletPrefab;

    private void Awake()
    {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction))
        {
            moveAction.StartMoving += MoveAction_StartMoving;
            moveAction.StopMoving += MoveAction_StopMoving;
        }
        if(TryGetComponent<ShootAction>(out ShootAction shootAction))
        {
            shootAction.StartShooting += ShootAction_StartShooting;
        }
    }

    private void ShootAction_StartShooting(object sender, ShootAction.ShootingPositionArgs e)
    {
        animator.SetTrigger(SHOOT);
        Transform BulletShot = Instantiate(BulletPrefab, PointBlankShoot.position, Quaternion.identity);
        RiffleBullet riffleBullet = BulletShot.GetComponent<RiffleBullet>();

        Vector3 targetPosition = e.targetShoot.transform.position;
        Vector3 shooterPosition = e.shooter.transform.position;
        targetPosition.y = PointBlankShoot.position.y;

        riffleBullet.SetUp(e.targetShoot, targetPosition);
    }

    private void MoveAction_StopMoving(object sender, EventArgs e)
    {
        animator.SetBool(ISWALKING, false);
    }

    private void MoveAction_StartMoving(object sender, EventArgs e)
    {
        animator.SetBool(ISWALKING, true);
        
        
    }
}
