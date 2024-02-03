using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit unit;
    

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            List<GridPosition> validMoveGridArray = unit.GetMoveAction().GetValidateMovePosition();
            GridVisualSystem.Instance.HideAllGridVisual();
            GridVisualSystem.Instance.ShowVisualGrid(validMoveGridArray);
        }
    }
}
