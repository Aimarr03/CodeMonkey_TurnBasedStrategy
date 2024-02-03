using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionSelectedUnit : MonoBehaviour
{
    private bool isBusy;
    public static ActionSelectedUnit instance;

    public event EventHandler SelectedUnit;
    public event EventHandler SelectedAction;
    public event EventHandler<bool> BusyAction;
    public event EventHandler ActionExecuted;
    
    [SerializeField] private LayerMask targetLayer;
    
    private Unit currentUnit;
    private BaseAction baseAction;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("There is another instance");
            Destroy(gameObject);
            return;
        }
        instance = this;
        isBusy = false;
    }
    private void Start()
    {
        SetSelectedAction(baseAction);
    }
    // Update is called once per frame
    void Update()
    {
        if (isBusy)
        {
            return;
        }
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if(HandleSelection())
        {
            return;
        }
        ActionExecute();
        
    }
    public void ActionExecute()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GridPosition gridPosition = LevelGrid.instance.GetGridPosition(MouseInput.GetMousePosition());
            if (baseAction == null)
            {
                return;
            }
            if (baseAction.ValidateMove(gridPosition))
            {
                if (!currentUnit.TryAction(baseAction))
                {
                    return;
                }
                baseAction.TakeAction(gridPosition, ClearBusy);
                ActionExecuted?.Invoke(this, EventArgs.Empty);
                SetBusy();
            }
            /*switch(baseAction)
            {
                case MoveAction moveAction:
                    GridPosition gridPosition = LevelGrid.instance.GetGridPosition(MouseInput.GetMousePosition());
                    if (moveAction.ValidateMove(gridPosition))
                    {
                        moveAction.Move(gridPosition, ClearBusy);
                    }
                    break;
                case SpinAction spinAction:
                    spinAction.Spin(ClearBusy);
                    break;
            }
            SetBusy();*/
        }
    }
    private void ClearBusy()
    {
        isBusy = false;
        BusyAction?.Invoke(this, isBusy);
    }

    private void SetBusy()
    {
        isBusy = true;
        BusyAction?.Invoke(this, isBusy);
    }
    private bool HandleSelection(){
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out RaycastHit raycastHit,float.MaxValue,targetLayer)){
                    raycastHit.transform.TryGetComponent<Unit>(out Unit unit);
                    if(unit == currentUnit)
                    {
                        return false;
                    }
                    if(unit.IsEnemy())
                    {
                        return false;
                    }
                    SetUnit(unit);
                    return true;
                }
        }
        return false;
    }

    private void SetUnit(Unit unit){
        currentUnit = unit;
        SetSelectedAction(currentUnit.GetMoveAction());
        SelectedUnit?.Invoke(this, EventArgs.Empty);
    }
    public void SetSelectedAction(BaseAction action)
    {
        baseAction = action;
        SelectedAction?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetUnit(){
        return currentUnit;
    }
    public BaseAction GetBaseAction()
    {
        return baseAction;
    }
}
