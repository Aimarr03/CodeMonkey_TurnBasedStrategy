using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float timer;

    public void Start()
    {
        TurnSystem.instance.UpdateTurn += Instance_UpdateTurn;
    }

    private void Instance_UpdateTurn(object sender, System.EventArgs e)
    {
        timer = 2f;
    }

    public void Update()
    {
        if(TurnSystem.instance.IsPlayerTurn())
        {
            return;
        }
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            TurnSystem.instance.EndTurn();
        }
    }
}