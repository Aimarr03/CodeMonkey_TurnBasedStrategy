using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public static MouseInput instance;
    private Vector3 currentPosition;
    [SerializeField] private LayerMask targetRay;
    
    private void Awake() {
        if(instance != null) return;
        instance = this;    
        currentPosition = transform.position;
    }
    private void Update()
    {
        transform.position = GetMousePosition();   
        currentPosition = transform.position;
    }

    public static Vector3 GetMousePosition(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.targetRay)){
            return raycastHit.point;
        }
        return instance.currentPosition;
    }
}
