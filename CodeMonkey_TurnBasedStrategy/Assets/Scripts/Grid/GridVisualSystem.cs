using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisualSystem : MonoBehaviour
{
    [SerializeField] private Transform gridVisualSinglePrefab;
    private GridVisualSingle[,] GridVisualSingleArray;

    public static GridVisualSystem Instance { get; private set; }


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
    }

    public void Update()
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
    public void ShowVisualGrid(List<GridPosition> ListGridPosition)
    {
        foreach(GridPosition gridPosition in ListGridPosition)
        {
            int x = gridPosition.x;
            int z = gridPosition.z;

            GridVisualSingle gridVisualSingle = GridVisualSingleArray[x, z];
            gridVisualSingle.Show();
        }
    }

    private void UpdateVisualGrid()
    {
        HideAllGridVisual();
        BaseAction baseAction= ActionSelectedUnit.instance.GetBaseAction();
        if(baseAction == null)
        {
            return;
        }
        ShowVisualGrid(baseAction.GetValidateMovePosition());
    }    
}
