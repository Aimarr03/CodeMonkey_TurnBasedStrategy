using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    private List<Unit> unitsList;
    private List<Unit> enemyList;
    private List<Unit> friendlyList;

    public static UnitManager instance;

    public void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Instance is already made!");
            return;
        }
        instance = this;
        unitsList = new List<Unit>();
        enemyList = new List<Unit>();
        friendlyList = new List<Unit>();
    }
    public void Start()
    {
        Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
        Unit.OnAnyUnitDied += Unit_OnAnyUnitDied;
    }

    private void Unit_OnAnyUnitSpawned(object sender, System.EventArgs e)
    {
        Unit unit = sender as Unit;
        unitsList.Add(unit);
        Debug.Log("Unit spawned " + unit);
        if (unit.IsEnemy())
        {
            enemyList.Add(unit);
        }
        else
        {
            friendlyList.Add(unit);
        }
    }

    private void Unit_OnAnyUnitDied(object sender, System.EventArgs e)
    {
        Unit unit = sender as Unit;
        unitsList.Remove(unit);
        Debug.Log("Unit dead " + unit);
        if (unit.IsEnemy())
        {
            enemyList.Remove(unit);
        }
        else
        {
            friendlyList.Remove(unit);
        }
    }
    public List<Unit> GetUnitList()
    {
        return unitsList;
    }
    public List<Unit> GetFriendlyList()
    {
        return friendlyList;
    }
    public List<Unit> GetEnemyList()
    {
        return enemyList;
    }
}
