using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll_Spawner : MonoBehaviour
{
    [SerializeField] private Transform Ragdoll;
    [SerializeField] private Transform originalRootBone;
    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
    }
    private void Start()
    {
        healthSystem.UnitDie += HealthSystem_UnitDie;
    }

    private void HealthSystem_UnitDie(object sender, System.EventArgs e)
    {
        Transform ragdollUnit = Instantiate(Ragdoll, transform.position, transform.rotation);
        UnitRagdoll unitRagdoll = ragdollUnit.GetComponent<UnitRagdoll>();
        unitRagdoll.Setup(originalRootBone);
    }
}
