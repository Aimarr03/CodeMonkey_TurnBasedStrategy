using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisualSystem : MonoBehaviour
{
    [SerializeField] private Transform gridVisualSinglePrefab;
    [SerializeField] private List<GridVisualTypeMaterial> gridVisualTypeMaterialList;
    private GridVisualSingle[,] GridVisualSingleArray;

    public static GridVisualSystem Instance { get; private set; }

    [Serializable]
    public struct GridVisualTypeMaterial
    {
        public Material material;
        public GridVisualType type;
    }


    public enum GridVisualType
    {
        White,
        Red,
        Blue,
        Yellow,
        RedSoft
    }

    public void Awake()
    {
        if(Instance != null)
        {
            return;
        }
        Instance = this;

    }
    void Start()
    {
        GridVisualSingleArray = new GridVisualSingle[
            LevelGrid.instance.GetWidth(),
            LevelGrid.instance.GetHeight()
            ];
        for(int x = 0; x < LevelGrid.instance.GetWidth(); x++)
        {
            for(int z = 0; z < LevelGrid.instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x,z);

                Transform InstantiatedVisualGrid = 
                    Instantiate(gridVisualSinglePrefab, LevelGrid.instance.GetWorldPosition(gridPosition), Quaternion.identity);
                GridVisualSingle gridVisualSingle = InstantiatedVisualGrid.GetComponent<GridVisualSingle>();
                gridVisualSingle.Hide();
                GridVisualSingleArray[x, z] = gridVisualSingle;
            }
        }
        ActionSelectedUnit.instance.SelectedUnit += Instance_SelectedUnit;
        ActionSelectedUnit.instance.SelectedAction += Instance_SelectedAction;
        LevelGrid.instance.OnAnyUnitMoveAnyUnit += Instance_OnAnyUnitMoveAnyUnit;
    }

    private void Instance_OnAnyUnitMoveAnyUnit(object sender, System.EventArgs e)
    {
        UpdateVisualGrid();
    }

    private void Instance_SelectedAction(object sender, System.EventArgs e)
    {
        UpdateVisualGrid();
    }

    private void Instance_SelectedUnit(object sender, System.EventArgs e)
    {
        UpdateVisualGrid();
    }
    public void HideAllGridVisual()
    {
        for(int x = 0; x < LevelGrid.instance.GetWidth(); x++)
        {
            for(int z = 0; z < LevelGrid.instance.GetHeight(); z++)
            {
                GridVisualSingleArray[x, z].Hide();
            }
        }
    }
    private void ShowVisualGridRange(GridPosition unitGridPosition, int range, GridVisualType gridVisualType)
    {
        List <GridPosition> gridPositionRangeList = new List <GridPosition>();
        for(int x = -range; x < range; x++)
        {
            for(int z = -range; z < range; z++)
            {
                GridPosition newGridPositon = unitGridPosition + new GridPosition(x, z);
                int totalDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (totalDistance > range)
                {
                    continue;
                }
                if (!LevelGrid.instance.CheckGridBoundary(newGridPositon))
                {
                    continue;
                }
                gridPositionRangeList.Add(newGridPositon);
            }
        }
        ShowVisualGrid(gridPositionRangeList, gridVisualType);
        
    }
    public void ShowVisualGrid(List<GridPosition> ListGridPosition, GridVisualType gridVisualType)
    {
        Material gridVisualMaterial = GetGridVisualTypeMaterial(gridVisualType);
        foreach(GridPosition gridPosition in ListGridPosition)
        {
            int x = gridPosition.x;
            int z = gridPosition.z;

            GridVisualSingle gridVisualSingle = GridVisualSingleArray[x, z];
            gridVisualSingle.Show(gridVisualMaterial);
        }
    }

    private void UpdateVisualGrid()
    {
        HideAllGridVisual();
        BaseAction baseAction= ActionSelectedUnit.instance.GetBaseAction();
        Unit unit = ActionSelectedUnit.instance.GetUnit();
        GridVisualType gridVisualType = GridVisualType.Yellow;
        if(baseAction == null)
        {
            return;
        }
        switch (baseAction)
        {
            case ShootAction shootAction:
                gridVisualType = GridVisualType.Red;
                ShowVisualGridRange(unit.GetGridPosition(), shootAction.GetMaxDistanceShooting(), GridVisualType.RedSoft);
                break;
            case MoveAction moveAction:
                gridVisualType = GridVisualType.White;
                break;
            case SpinAction spinAction:
                gridVisualType = GridVisualType.Blue;
                break;
        }
        ShowVisualGrid(baseAction.GetValidateMovePosition(), gridVisualType);
    }    
    private Material GetGridVisualTypeMaterial(GridVisualType gridVisualType)
    {
        foreach(GridVisualTypeMaterial gridVisualTypeMaterial in gridVisualTypeMaterialList)
        {
            if(gridVisualTypeMaterial.type == gridVisualType)
            {
                return gridVisualTypeMaterial.material;
            }
        }

        Debug.LogError("Type " + gridVisualType + " doesn't exist");
        return null;
    }
}
