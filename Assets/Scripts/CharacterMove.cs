using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMove : MonoBehaviour
{
    [SerializeField, Range(0.1f, 100f)] private float speed = 10f;
    [SerializeField, Range(0.1f, 100f)] private float speedBooster = 3f;
    [SerializeField, Range(0.1f, 600f)] private float rotationSpeed = 200f;
    [SerializeField, Range(0.1f, 100f)] private float maxAcceleration = 20f;

    enum MoveState
    {
        Walk,
        Run
    }
    private MoveState moveState = MoveState.Walk;
    private Vector3 velocity;
    private Vector3 inputX, inputZ;
    private Rigidbody rb;

    private float inputRotation, currentSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = speed;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
       GetInputs();
       Rotate();
       
    }

    private void CheckMoveState()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)
            && moveState == MoveState.Walk){
                moveState = MoveState.Run;
                return;
            }
        if(Input.GetKeyUp(KeyCode.LeftShift)
            && moveState == MoveState.Run){
                moveState = MoveState.Walk;
                return;
            }
    }
    
    private void Rotate(){
         float yRotation = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
         transform.Rotate(0, yRotation , 0);
    }

    private void GetInputs(){
        inputX = transform.right * Input.GetAxis("Horizontal");
        inputZ = transform.forward * Input.GetAxis("Vertical");
        inputRotation = Input.GetAxis("Mouse X");
        CheckMoveState();
        currentSpeed = GetSpeed();
    }

    private float GetSpeed(){
        return moveState switch{
            MoveState.Run => speed * speedBooster,
            MoveState.Walk => speed,
            _ => speed
        };
    }

    private void FixedUpdate()
    {
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        //------------- Оси X и Z-----------Ускорение------------------------Ось Y--------------------
        rb.velocity = ((inputX + inputZ) * currentSpeed * maxSpeedChange) + (Vector3.up * rb.velocity.y);
    }
}


