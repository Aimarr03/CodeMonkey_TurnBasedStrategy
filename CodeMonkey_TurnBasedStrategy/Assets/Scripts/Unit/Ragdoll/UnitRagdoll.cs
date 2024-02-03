using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdoll : MonoBehaviour
{
    [SerializeField] private Transform rootBone;
    public void Setup(Transform ragdollTransform)
    {
        MatchAllChildTransform(ragdollTransform, rootBone);
        AddForceRagDoll(rootBone, 1500f, transform.position, 100);
    }

    private void MatchAllChildTransform(Transform root, Transform clone)
    {
        foreach(Transform child in root)
        {
            Transform childClone = clone.Find(child.name);
            if (childClone != null)
            {
                childClone.position = child.position;
                childClone.rotation = child.rotation;

                MatchAllChildTransform (child, childClone);
            }
        }
    }
    private void AddForceRagDoll(Transform root, float explosionForce, Vector3 explosionPosition, float explosionRadius )
    {
        foreach (Transform child in root)
        {
            if(child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
            {
                childRigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
                AddForceRagDoll(child, explosionForce, explosionPosition, explosionRadius);
            }
        }
    }
}
