using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMove : MonoBehaviour
{
    [SerializeField, Range(0.1f, 100f)] private float speed = 10f;
    [SerializeField, Range(0.1f, 100f)] private float speedBooster = 3f;
    [SerializeField, Range(0.1f, 600f)] private float rotationSpeed = 200f;
    [SerializeField, Range(0.1f, 100f)] private float maxAcceleration = 20f;

    Vector3 velocity;
    Vector3 inputX, inputZ;
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

    void Rotate(){
         float yRotation = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
         transform.Rotate(0, yRotation , 0);
    }

    void GetInputs(){
        inputX = transform.right * Input.GetAxis("Horizontal");
        inputZ = transform.forward * Input.GetAxis("Vertical");
        inputRotation = Input.GetAxis("Mouse X");
        currentSpeed = GetSpeed();
    }

    float GetSpeed(){
        if(Input.GetKey(KeyCode.LeftShift)){
            return speed * speedBooster;
        }
        else 
            return speed;
    }

    private void FixedUpdate()
    {
        velocity = rb.velocity;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity = (inputX + inputZ) * currentSpeed * maxSpeedChange;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
        Debug.Log(velocity);
    }
}


