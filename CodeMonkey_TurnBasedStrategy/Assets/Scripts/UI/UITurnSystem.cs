using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITurnSystem : MonoBehaviour
{
    [SerializeField] private Button endTurn;
    [SerializeField] private TextMeshProUGUI turnVisualText;
    [SerializeField] private GameObject EnemyTurnUI;
    public void Awake()
    {
        UpdateTurnVisual();
    }
    public void Start()
    {
        endTurn.onClick.AddListener(() =>
        {
            TurnSystem.instance.EndTurn();
        });
        TurnSystem.instance.UpdateTurn += Instance_UpdateTurn;
        EnemyTurnUIVisibility();
        EndTurnVisibility();
    }

    private void Instance_UpdateTurn(object sender, System.EventArgs e)
    {
        UpdateTurnVisual();
        EndTurnVisibility();
        EnemyTurnUIVisibility();
    }

    public void UpdateTurnVisual()
    {
        int currentTurn = TurnSystem.instance.GetCurrentTurnNumber();
        turnVisualText.text = $"Turns: {currentTurn}";
    }
    public void EnemyTurnUIVisibility()
    {
        EnemyTurnUI.SetActive(!TurnSystem.instance.IsPlayerTurn());
    }
    public void EndTurnVisibility()
    {
        endTurn.gameObject.SetActive(TurnSystem.instance.IsPlayerTurn());
    }
}
