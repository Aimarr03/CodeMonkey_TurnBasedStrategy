using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    private const float MIN_VALUE_SCROLL = 4f;
    private const float MAX_VALUE_SCROLL = 10f;
    private CinemachineTransposer cinemachineTransposer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private CinemachineVirtualCamera cinemachine;
    // Update is called once per frame
    private void Start() {
         cinemachineTransposer = cinemachine.GetCinemachineComponent<CinemachineTransposer>();
    }
    void Update()
    {
        Movement();
        Rotation();
        Zooming();
    }
    void Movement(){
        Vector3 movement = new Vector3(0,0,0);
        if(Input.GetKey(KeyCode.W)){
            movement.z = 1;
        }        
        if(Input.GetKey(KeyCode.S)){
            movement.z = -1;
        }        
        if(Input.GetKey(KeyCode.D)){
            movement.x = 1;
        }        
        if(Input.GetKey(KeyCode.A)){
            movement.x = -1;
        }        
        Vector3 movementDirection = Vector3.forward * movement.z + Vector3.right * movement.x;
        transform.position += movementDirection * Time.deltaTime * moveSpeed;
    }
    void Rotation(){
        Vector3 rotation = new Vector3(0,0,0);
        
        if(Input.GetKey(KeyCode.E)){
            rotation.y = -1;
        }
        if(Input.GetKey(KeyCode.Q)){
            rotation.y = 1;
        }
        transform.eulerAngles += rotation * Time.deltaTime  * rotationSpeed;
    }
    void Zooming(){
        Vector3 offset = cinemachineTransposer.m_FollowOffset;
        Debug.Log(Input.mouseScrollDelta);
        if(Input.mouseScrollDelta.y > 0){
            offset.y -= scrollSpeed;
        }
        if(Input.mouseScrollDelta.y < 0f){
            offset.y += scrollSpeed;
        }
        offset.y = Mathf.Clamp(offset.y, MIN_VALUE_SCROLL, MAX_VALUE_SCROLL);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, offset, Time.deltaTime * zoomSpeed);
    }
}

