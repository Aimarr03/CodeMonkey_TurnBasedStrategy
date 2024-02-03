using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIActionSystem : MonoBehaviour
{
    [SerializeField] private Transform ActionButtonPrefab;
    [SerializeField] private Transform ActionContainer;
    [SerializeField] private TextMeshProUGUI ActionPointText;

    private List<UIActionButton> UIActionButtons;
    public void Start()
    {
        UIActionButtons = new List<UIActionButton>();
        UpdateAction();
        UpdateVisualSelected();
        UpdateActionPointVisual();

        ActionSelectedUnit.instance.SelectedUnit += Instance_SelectedUnit;
        ActionSelectedUnit.instance.SelectedAction += Instance_SelectedAction;
        ActionSelectedUnit.instance.ActionExecuted += Instance_ActionExecuted;
        Unit.OnAnyResetActionPoints += Unit_OnAnyResetActionPoints;
    }

    private void Unit_OnAnyResetActionPoints(object sender, System.EventArgs e)
    {
        UpdateActionPointVisual();
    }

    private void Instance_ActionExecuted(object sender, System.EventArgs e)
    {
        UpdateActionPointVisual();
    }

    private void Instance_SelectedAction(object sender, System.EventArgs e)
    {
        UpdateVisualSelected();
    }

    private void Instance_SelectedUnit(object sender, System.EventArgs e)
    {
        UpdateAction();
        UpdateVisualSelected();
        UpdateActionPointVisual();
    }

    public void UpdateAction()
    {
        UIActionButtons.Clear();
        foreach (Transform child in ActionContainer)
        {
            Destroy(child.gameObject);
        }
        Unit unit = ActionSelectedUnit.instance.GetUnit();
        if (unit == null) return;
        BaseAction[] actionArray = unit.GetActionArray();

        foreach (BaseAction action in actionArray)
        {
            Transform actionButton = Instantiate(ActionButtonPrefab, ActionContainer);
            UIActionButton UIActionButton = actionButton.GetComponent<UIActionButton>();
            UIActionButton.UpdateUI(action);
            UIActionButtons.Add(UIActionButton);
        }
    }
    private void UpdateVisualSelected()
    {
        foreach(UIActionButton UIaction in UIActionButtons)
        {
            UIaction.UpdateVisualSelected();
        }
    }
    private void UpdateActionPointVisual()
    {
        Unit currentUnit = ActionSelectedUnit.instance.GetUnit();
        if(currentUnit == null)
        {
            ActionPointText.text = "";
            return;
        }
        ActionPointText.text = $"Action Points: {currentUnit.GetActionPoint()}";
    }
}
