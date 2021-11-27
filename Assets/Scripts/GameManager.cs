using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject Coins;
    [SerializeField] Text CoinsCountTextBlock;
    [SerializeField] Image WinImage;
    [SerializeField] AudioSource WinSound;

    public GameObject Character;
    private int allCoinsCount;
    private int coinsCount;
    private int CoinsCount{
        get => coinsCount;
        set{
            coinsCount = value;
            CheckWin();
            UpdateCointCountText();
        }
    }

    private string CoinsCountText {
        get => $"{CoinsCount} / {allCoinsCount}";
    }

    private void Start()
    {
        WinImage.gameObject.SetActive(false);
        //Получить максимальное количество монет
        allCoinsCount = Coins.GetComponentsInChildren<Coin>().Length;
        UpdateCointCountText();
        
    }

    private void Update() {
        CheckLose();
    }

    public void PickUpCoin(){
        CoinsCount++;
    }

    private void UpdateCointCountText(){
        CoinsCountTextBlock.text = CoinsCountText;
    }

    private void CheckWin(){
        if(CoinsCount >= allCoinsCount){
            StartCoroutine(Win());
        }
    }

    private void CheckLose(){
        if(Character.transform.position.y <= -30)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    IEnumerator Win(){

        var rb = Character.GetComponent<Rigidbody>();
        WinImage.gameObject.SetActive(true);
        WinSound.Play();

        rb.AddForce(Vector3.up * Random.Range(1000f, 10000f), ForceMode.Impulse);
        rb.freezeRotation = false;
        rb.AddTorque(new Vector3(Random.Range(0f, 300f),Random.Range(0f, 300f),Random.Range(0f, 300f)), ForceMode.Impulse);

        yield return new WaitForSeconds(5);
        WinImage.gameObject.SetActive(false);
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
