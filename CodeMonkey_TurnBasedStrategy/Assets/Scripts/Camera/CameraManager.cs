using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject ActionCamera;


    private void Start()
    {
        DisableCamera();
        BaseAction.ActionStarted += BaseAction_ActionStarted;
        BaseAction.ActionFinished += BaseAction_ActionFinished;
    }
    private void BaseAction_ActionStarted(object sender, System.EventArgs e)
    {

        switch (sender)
        {
            case ShootAction shootAction:
                Vector3 UnitHeight = Vector3.up * 1.7f;

                
                Unit shooterUnit = shootAction.GetUnit();
                Unit targetUnit = shootAction.GetTargetUnit();
                Vector3 direction = (targetUnit.transform.position - shooterUnit.transform.position).normalized;


                float shoulderOffsetAmmount = 0.5f;
                Vector3 shoulderOffset = Quaternion.Euler(0,90,0) *  direction * shoulderOffsetAmmount;
                Vector3 actionCameraPosition =
                    shooterUnit.transform.position +
                    UnitHeight +
                    shoulderOffset +
                    (direction * -1);
                ActionCamera.transform.position = actionCameraPosition;
                ActionCamera.transform.LookAt(targetUnit.transform.position + UnitHeight);
                EnableCamera();
                break;
        }
    }

    private void BaseAction_ActionFinished(object sender, System.EventArgs e)
    {
        switch (sender)
        {
            case ShootAction:
                DisableCamera();
                break;
        }
    }


    private void EnableCamera()
    {
        ActionCamera.SetActive(true);
    }
    private void DisableCamera()
    {
        ActionCamera.SetActive(false);
    }
}
