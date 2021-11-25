using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Coins;
    [SerializeField] Text CoinsCountTextBlock;
    [SerializeField] Image WinImage;
    [SerializeField] AudioSource WinSound;
    int allCoinsCount;
    int coinsCount;
    int CoinsCount{
        get => coinsCount;
        set{
            coinsCount = value;
            UpdateCointCountText();
        }
    }
    string CoinsCountText {
        get => $"{CoinsCount} / {allCoinsCount}";
    }

    void Start()
    {
        WinImage.gameObject.SetActive(false);
        allCoinsCount = Coins.GetComponentsInChildren<Coin>().Count();
        UpdateCointCountText();
        
    }

    public void PickUpCoin(){
        CoinsCount++;
    }

    void UpdateCointCountText(){
        CheckWin();
        CoinsCountTextBlock.text = CoinsCountText;

    }

    void CheckWin(){
        if(CoinsCount >= allCoinsCount){
            StartCoroutine(Win());

        }
    }

    IEnumerator Win(){
        var rb = FindObjectOfType<CharacterMove>().transform.GetComponent<Rigidbody>();
        WinImage.gameObject.SetActive(true);
        WinSound.Play();
        rb.AddForce(Vector3.up * Random.Range(100f, 1000f), ForceMode.Impulse);
        rb.freezeRotation = false;
        rb.AddTorque(new Vector3(Random.Range(0f, 300f),Random.Range(0f, 300f),Random.Range(0f, 300f)), ForceMode.Impulse);
        yield return new WaitForSeconds(5);
        WinImage.gameObject.SetActive(false);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //yield return new WaitForSeconds(1);
        
    }

}
