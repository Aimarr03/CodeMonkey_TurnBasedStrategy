using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Transform DebugPrefab;
    GridSystem gridSystem;
    public void Start(){
        gridSystem = new GridSystem(10,10,2);
        gridSystem.CreateDebugPrefab(DebugPrefab);
    }
    private void Update() {
        Debug.Log(gridSystem.GetGridPosition(MouseInput.GetMousePosition()));   
        
    }
}
