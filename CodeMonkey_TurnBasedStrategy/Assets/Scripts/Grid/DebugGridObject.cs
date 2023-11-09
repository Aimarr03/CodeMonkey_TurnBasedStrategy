using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugGridObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
    
    private GridObject gridObject;
    public void SetGridObject(GridObject gridObject){
        this.gridObject = gridObject;
    }
    public void Update(){
        textMeshPro.text = gridObject.ToString();
    }
}
