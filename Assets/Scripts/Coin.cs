using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coin : MonoBehaviour
{   
    public AudioClip pickupAudio;
    bool isActive = true;
    [SerializeField] float RotationSpeed = 180f;
    // Update is called once per frame
    void Update()
    {
       Move();
    }
    void Move(){
        transform.Rotate(0, 0,  RotationSpeed * Time.deltaTime);
        //transform.Translate(0, Mathf.Sin(Time.time), 0);
        var _newPosition = transform.position;
        _newPosition.y += 0.004f * Mathf.Sin((Mathf.PI) * Time.time + transform.position.x);
        transform.position = _newPosition;
    }

    private void OnCollisionEnter(Collision other) {
        if(isActive){
            isActive = false;
            if(other.transform.CompareTag("Player")){
               var audio = GetComponent<AudioSource>();
                AudioSource.PlayClipAtPoint(pickupAudio, transform.position);
                FindObjectOfType<GameManager>().PickUpCoin();
                Destroy(gameObject);
            }
        }
    }
}
