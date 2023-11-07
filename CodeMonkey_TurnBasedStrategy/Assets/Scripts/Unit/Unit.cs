using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Vector3 targetPosition;
    const string IsWalking = "IsWalking";
    private float speed;
    private void Start() {
        targetPosition = transform.position;
        speed = 2f;
    }
    private void Update() {
        if(Vector3.Distance(targetPosition, transform.position) > 0.3f){
            Vector3 MoveDirection = targetPosition - transform.position;
            transform.position += MoveDirection * speed * Time.deltaTime;
            animator.SetBool("IsWalking", true);
        }
        else{
            animator.SetBool("IsWalking", false);
        }
        if(Input.GetMouseButton(0)){
            Move(MouseInput.GetMousePosition());
            
        }
    }

    private void Move(Vector3 newPosition){
        targetPosition = newPosition;
    }
}
