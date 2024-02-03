using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBusySystem : MonoBehaviour
{
    public void Start()
    {
        ActionSelectedUnit.instance.BusyAction += Instance_BusyAction;
        gameObject.SetActive(false);
    }

    private void Instance_BusyAction(object sender, bool e)
    {
        gameObject.SetActive(e);
    }
}
