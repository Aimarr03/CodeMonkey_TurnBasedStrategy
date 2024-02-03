using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    private int currentTurn;
    public static TurnSystem instance;
    public event EventHandler UpdateTurn;

    private bool playerTurn;
    public void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("There is more than one instance");
            return;
        }
        instance = this;
        playerTurn = true;
        currentTurn = 1;
    }
    public void EndTurn()
    {
        currentTurn++;
        playerTurn = !playerTurn;
        UpdateTurn?.Invoke(this, EventArgs.Empty);
    }
    public int GetCurrentTurnNumber()
    {
        return currentTurn;
    }
    public bool IsPlayerTurn()
    {
        return playerTurn;
    }
}
