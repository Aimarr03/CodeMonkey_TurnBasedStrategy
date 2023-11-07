using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 targetPosition;
    private float speed;
    private void Start() {
        targetPosition = transform.position;
        speed = 4f;
    }
    private void Update() {
        if(Vector3.Distance(targetPosition, transform.position) > 0.3f){
            Vector3 MoveDirection = (targetPosition - transform.position);
            transform.position += MoveDirection * speed * Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.T)){
            Move(new Vector3(4,0,4));
        }
    }

    private void Move(Vector3 newPosition){
        targetPosition = newPosition;
    }
}
