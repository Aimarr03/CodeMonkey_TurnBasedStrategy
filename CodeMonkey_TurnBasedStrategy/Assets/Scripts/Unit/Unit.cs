using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Vector3 targetPosition;
    private GridPosition gridPosition;
    const string IsWalking = "IsWalking";
    private float speed;
    private float rotateSpeed;
    private void Awake() {
        targetPosition = transform.position;
        speed = 2f;
        rotateSpeed = 10f;
    }
    private void Start() {
        gridPosition = LevelGrid.instance.GetGridPosition(transform.position);
        LevelGrid.instance.SetUnitAtGridPosition(gridPosition, this);    
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

        GridPosition newGridPoisiton = LevelGrid.instance.GetGridPosition(transform.position);
        if(newGridPoisiton != gridPosition){
            //Unit change into different grid position
            LevelGrid.instance.ChangeUnitGridToAnother(gridPosition, newGridPoisiton, this);
            gridPosition = newGridPoisiton;
        }
    }

    public void Move(Vector3 newPosition){
        targetPosition = newPosition;
    }
}
