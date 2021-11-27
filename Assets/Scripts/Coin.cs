using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coin : MonoBehaviour
{   
    public AudioClip pickupAudio;
    bool IsPickuped = false;
    [SerializeField] float RotationSpeed = 180f;
    // Update is called once per frame

    void Update()
    {
       Move();
    }

    void Move(){
        //Вращение вокрус y оси
        transform.Rotate(0, 0,  RotationSpeed * Time.deltaTime);
        
        //Перемещение вверх вниз по оси y
        var _newPosition = transform.position;
        _newPosition.y += 0.004f * Mathf.Sin((Mathf.PI) * Time.time + transform.position.x);

        transform.position = _newPosition;
    }

    private void OnTriggerEnter(Collider collider) {
        if(IsPickuped) return;
        if(collider.GetComponent<CharacterMove>() is CharacterMove){
            IsPickuped = true;
            GetComponent<CapsuleCollider>().enabled = false;
            AudioSource.PlayClipAtPoint(pickupAudio, transform.position);
            FindObjectOfType<GameManager>().PickUpCoin();
            Destroy(gameObject);
        }
        
    }
}
