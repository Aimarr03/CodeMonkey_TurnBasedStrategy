using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIActionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;
    [SerializeField] private Image SelectedVisual;
    private BaseAction baseAction;
    public void UpdateUI(BaseAction baseAction)
    {
        textMeshPro.text = baseAction.GetName();
        this.baseAction = baseAction;

        button.onClick.AddListener(() =>
        {
            ActionSelectedUnit.instance.SetSelectedAction(baseAction);
            
        });
    }
    public void UpdateVisualSelected()
    {
        BaseAction currentBaseAction = ActionSelectedUnit.instance.GetBaseAction();
        SelectedVisual.enabled = currentBaseAction == baseAction;
    }
}
