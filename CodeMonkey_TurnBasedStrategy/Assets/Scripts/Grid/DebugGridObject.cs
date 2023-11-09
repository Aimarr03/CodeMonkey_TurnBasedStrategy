using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugGridObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
    

    public void SetTextMeshPro(string text){
        textMeshPro.text = text;
    }
}
