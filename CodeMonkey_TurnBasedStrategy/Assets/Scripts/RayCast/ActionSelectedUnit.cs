using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSelectedUnit : MonoBehaviour
{
    public static ActionSelectedUnit instance;
    public event EventHandler SelectedUnit;
    [SerializeField] private LayerMask targetLayer;
    private Unit currentUnit;
    private void Awake() {
        if(instance != null){
            Debug.LogWarning("There is another instance");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && currentUnit != null){
            currentUnit.Move(MouseInput.GetMousePosition());
        }
        if(Input.GetMouseButton(0)){
            HandleSelection();
        }
    }
    private void HandleSelection(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit raycastHit,float.MaxValue,targetLayer)){
                raycastHit.transform.TryGetComponent<Unit>(out Unit unit);
                SetUnit(unit);
            }
    }

    public void SetUnit(Unit unit){
        currentUnit = unit;
        SelectedUnit?.Invoke(this, EventArgs.Empty);
    }
    public Unit GetUnit(){
        return currentUnit;
    }
}
