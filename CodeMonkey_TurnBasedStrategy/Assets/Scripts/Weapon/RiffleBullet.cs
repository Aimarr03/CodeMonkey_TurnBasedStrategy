using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiffleBullet : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Transform BulletVFX;
    [SerializeField] private int damage;
    private Vector3 TargetPosition;
    private const float BULLET_SPEED = 50f;
    private Unit targetShooter;
    public void SetUp(Unit targetShooter, Vector3 targetPosition)
    {
        this.targetShooter = targetShooter;
        TargetPosition = targetPosition;
        Debug.Log(TargetPosition);
    }

    public void Update()
    {
        Vector3 Dir = (TargetPosition - transform.position).normalized;

        float BeforeDistance = Vector3.Distance(transform.position, TargetPosition);

        transform.position += BULLET_SPEED * Time.deltaTime * Dir;

        float AfterDistance = Vector3.Distance(transform.position, TargetPosition);

        if(BeforeDistance < AfterDistance)
        {
            transform.position = TargetPosition;
            trailRenderer.transform.parent = null;
            Destroy(gameObject);
            targetShooter.TakeDamage(damage);
            Instantiate(BulletVFX, TargetPosition, Quaternion.identity);
        }
    }
}
