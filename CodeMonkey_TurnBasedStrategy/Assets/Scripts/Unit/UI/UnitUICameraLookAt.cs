using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUICameraLookAt : MonoBehaviour
{
    private Transform mainCamera;
    [SerializeField] private bool inverted;

    private void Awake()
    {
        mainCamera = Camera.main.transform;
    }
    private void LateUpdate()
    {
        if (inverted)
        {
            Vector3 normalizedDirection = (mainCamera.position - transform.position).normalized;
            transform.LookAt(transform.position + normalizedDirection * -1);
        }
        else
        {
            transform.LookAt(mainCamera.position);
        }
    }
}
