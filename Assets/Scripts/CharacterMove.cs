using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField]public float Speed = 6f;
    [SerializeField]public float RunSpeed = 18f;
    [SerializeField]public float RotationSpeed = 500f;

    MoveState moveState = MoveState.Walk;
    enum MoveState{
        Walk,
        Run
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;     
    }
    void Update()
    {
        Move();
        Rotate();
    }

    void Move(){
        CheckMoveState();
        float currentSpeed = GetSpeed();
        Vector3 offset = new Vector3(Input.GetAxis("Horizontal"), 0,
                   Input.GetAxis("Vertical")) * Time.deltaTime * currentSpeed;
        transform.Translate(offset);
    }

    void Rotate(){
         float yRotation = Input.GetAxis("Mouse X") * Time.deltaTime * RotationSpeed;
         transform.Rotate(0, yRotation , 0);
    }

    //Проверка текущего состояния игрока
    void CheckMoveState(){
        if(Input.GetKeyDown(KeyCode.LeftShift) 
            && moveState != MoveState.Run){
            moveState = MoveState.Run;
            return;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)
            && moveState == MoveState.Run){
            moveState = MoveState.Walk;
            return;
        } 
    }
    //В зависимости от состояния получить скорость
    float GetSpeed(){
        return moveState switch{
            MoveState.Walk => Speed,
            MoveState.Run => RunSpeed,
            _ => Speed
        };
    }

}
