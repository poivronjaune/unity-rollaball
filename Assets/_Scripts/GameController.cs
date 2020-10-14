using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


//
// Singleton Pattern with a single Instance of this class
// 

public class GameController : MonoBehaviour
{

    public static GameController Instance {get; private set;}

    public GameObject pickUpPrefab;
    public GameObject mainBall;

    public float respawnPickups = 3f;

    public TextMeshProUGUI PickupCountText;
    public TextMeshProUGUI mainText;

    private List<GameObject> pickups = new List<GameObject>();
    private int pickupCount;
    

    // CoRoutine variable to start the game
    WaitForSeconds oneSecond;
    WaitForSeconds delay;
    
    private void Awake()
    {
        
        if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

        Instance = this;
        DontDestroyOnLoad(gameObject);

        oneSecond = new WaitForSeconds(1f);
        delay = new WaitForSeconds(respawnPickups);

        StartGame();
    }

    void Update() 
    {
        if (pickupCount >= 12)
        {
            EndGame();
        }

    }


    public void StartGame()
    {
        pickupCount = 0;
        StartCoroutine( countDownAndStartGame() );
    }

    public void EndGame()
    {
        mainText.gameObject.SetActive(true);
        mainText.text = "You win, R:Restart";

    }

    IEnumerator countDownAndStartGame()
    {
        
        mainText.text = "Get Ready...";
        yield return delay;

		mainText.text = "3";
		yield return oneSecond;

		mainText.text = "2";
		yield return oneSecond;

		mainText.text = "1";
		yield return oneSecond;

        mainText.text = "";
        mainText.gameObject.SetActive(false);

        DropPlayerOnGround();
        SpawnPickups();

    }

    void SpawnPickups()
    {
        // Generate a number of prefab objects store them where?

        // Place objects on the ground use spawn points or random

    }

    void DropPlayerOnGround()
    {
        // Place ball on ground and start
        mainBall.transform.position = new Vector3(0,7,0);
        mainBall.gameObject.SetActive(true);
    }

    public void handle_pickup(GameObject pickup)
    {
        pickup.SetActive(false);
        pickupCount++;
        PickupCountText.text = pickupCount.ToString();

    }



}
