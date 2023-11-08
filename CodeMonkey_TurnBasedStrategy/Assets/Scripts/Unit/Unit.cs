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
    private float rotateSpeed;
    private void Awake() {
        targetPosition = transform.position;
        speed = 2f;
        rotateSpeed = 10f;
    }
    private void Update() {
        if(Vector3.Distance(targetPosition, transform.position) > 0.3f){
            Vector3 MoveDirection = (targetPosition - transform.position).normalized;
            transform.position += MoveDirection * speed * Time.deltaTime;
            animator.SetBool("IsWalking", true);

            
            transform.forward = Vector3.Lerp(transform.forward, MoveDirection, Time.deltaTime * rotateSpeed);
        }
        else{
            animator.SetBool("IsWalking", false);
        }
    }

    public void Move(Vector3 newPosition){
        targetPosition = newPosition;
    }
}
