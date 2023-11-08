using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUnitVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;
    private MeshRenderer meshRenderer;

    private void Awake(){
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }
    private void Start() {
        ActionSelectedUnit.instance.SelectedUnit +=  OnSelectedUnit;
    }
    private void OnSelectedUnit(object sender, EventArgs empty){
        Unit currentUnit = ActionSelectedUnit.instance.GetUnit();
        if(currentUnit == unit){
            meshRenderer.enabled = true;
        }
        else{
            meshRenderer.enabled = false;
        }
    }
}
