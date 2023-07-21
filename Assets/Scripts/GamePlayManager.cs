using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayManager : MonoBehaviour
{
    private GameObject[] buildings;
    [SerializeField]
    private Player player;


    // For target pointer
    [SerializeField]
    private GameObject pointer;
    private Vector3 pointerPosition;

    [SerializeField]
    private TargetIndicator questPointer;

    // For delivery feature
    [SerializeField]
    private Button recieveButton, deliverButton;

    private GameObject shop, destination;
    private int destinationIndex, shopIndex;
    private bool inProcess=false, carryingOrder=false;

    // For level settings
    private int level;
    public VehicleSpawner[] vehicleSpawners;
    public CountdownTimer timer;
    private int numTotalOrders;
    private bool useTime;

    // For calculating score when game over
    private int numDeliveredOrders=0;
    private float remainingTime;
    private int totalMinutes, totalSeconds;
    public GameObject deathScreen, gameCompletedSceen, itemDeliveredCanvas,
     scoreDisplay, taskOrdersDisplay, taskTimeDisplay, deliveredOredersDisplay;
    private TextMeshProUGUI tmp, tmp1, tmp2, tmp3;

    // Start is called before the first frame update
    void Start()
    {   
        level = PlayerPrefs.GetInt("SelectedLevel");
        Debug.Log("LEVELDEBUGGER: " + level);

        // Settings for the level

        if (level <= 4){
            useTime = true;
        }
        else{
            useTime = false;
        }

        foreach (VehicleSpawner vehicleSpawner in vehicleSpawners){
                vehicleSpawner.carSpeed = 3 + level;
                vehicleSpawner.carsPerSpawn = (level - 1) / 2 + 1;
            }
        
        if (level == 1){
            numTotalOrders = 1;
            totalMinutes = 1;
            totalSeconds = 0;
        }
        else if (level == 2){
            numTotalOrders = 2;
            totalMinutes = 1;
            totalSeconds = 15;
        }
        else if (level == 3){
            numTotalOrders = 3;
            totalMinutes = 1;
            totalSeconds = 30;
        }
        else if (level == 4){
            numTotalOrders = 4;
            totalMinutes = 1;
            totalSeconds = 45;
        }
        else if (level == 5){
            numTotalOrders = 4;
            totalMinutes = 2;
            totalSeconds = 30;
        }
        else if (level == 6){
            numTotalOrders = 5;
            totalMinutes = 2;
            totalSeconds = 45;
        }
        else if (level == 7){
            numTotalOrders = 6;
            totalMinutes = 3;
            totalSeconds = 15;
        }
        else if (level == 8){
            numTotalOrders = 7;
            totalMinutes = 3;
            totalSeconds = 45;
        }

        tmp1 = taskOrdersDisplay.GetComponent<TextMeshProUGUI>();
        tmp1.text = "deliver " + numTotalOrders + " item(s)";

        tmp2 = taskTimeDisplay.GetComponent<TextMeshProUGUI>();
        tmp2.text = "finish within " + totalMinutes.ToString("00") + ":" + totalSeconds.ToString("00");

        timer.minutesLeft = totalMinutes;
        timer.secondsLeft = totalSeconds;

        recieveButton.gameObject.SetActive(false);
        deliverButton.gameObject.SetActive(false);

        buildings = GameObject.FindGameObjectsWithTag("Buildings");
        shopIndex = Random.Range(0, buildings.Length);

        shop = buildings[shopIndex];

        shop.AddComponent<Shop>();
        shop.tag = "Shop";
        for (int i = 0; i < buildings.Length; i++){
            if (i != shopIndex){
                buildings[i].AddComponent<House>();
                buildings[i].tag = "House";
            }
        }
        updatePointer(shop);
        changeTarget(shop);
        
        StartCoroutine(GenerateOrder());

    }

    void updatePointer(GameObject building){
        pointerPosition = building.transform.position;
        pointerPosition.y += 2;
        pointer.transform.position = pointerPosition;
    }

    void changeTarget(GameObject building){
        questPointer.Target = building;
    }

    public void receiveButtonClick(){
        carryingOrder = true;
        player.GetComponent<Player>().carryingOrder = true;
        recieveButton.gameObject.SetActive(false);
        shop.GetComponent<Shop>().havingOrder = false;
        updatePointer(destination);
        changeTarget(destination);
    }

    public void deliverButtonClick(){
        if (carryingOrder){
            destination.GetComponent<House>().isDesination = false;
            deliverButton.gameObject.SetActive(false);  
            carryingOrder = false;
            player.GetComponent<Player>().carryingOrder = false;
            inProcess = false;
            updatePointer(shop);
            changeTarget(shop);

            numDeliveredOrders += 1;
            tmp3 = deliveredOredersDisplay.GetComponent<TextMeshProUGUI>();
            tmp3.text = numDeliveredOrders.ToString();
            if (useTime && (numDeliveredOrders == numTotalOrders)){
                gameCompleted();
            }
        }       
    }

    IEnumerator GenerateOrder(){
        while (true){
            if (! inProcess){
                inProcess = true;
                shop.GetComponent<Shop>().havingOrder = true;

                destinationIndex = Random.Range(0, buildings.Length);
                while (destinationIndex == shopIndex){
                    destinationIndex = Random.Range(0, buildings.Length);
                }

                destination = buildings[destinationIndex];
                destination.GetComponent<House>().isDesination = true;

            }

            yield return new WaitForSeconds(Random.Range(3, 5));
        } 
    }

    public void endGame(){
        if (calculateScore() == 0){
            gameOver();
        }
        else{
            gameCompleted();
        }
    }
    public void gameOver(){

        itemDeliveredCanvas.SetActive(false);
        deathScreen.SetActive(true);
    }

    public void gameCompleted(){
        tmp = scoreDisplay.GetComponent<TextMeshProUGUI>();
        tmp.text = "YOU EARNED " + calculateScore().ToString();
        itemDeliveredCanvas.SetActive(false);

        gameCompletedSceen.SetActive(true);
    }

    public int calculateScore(){
        // Calculate number of stars achieved
        if (player.currentLives <= 0){
            // If the game is over because the player died, you get 0 star
            return 0;
        }

        if (useTime){
            // The game ends when the time is up or the player has delivered all orders
            if (numDeliveredOrders < numTotalOrders){
                // The game is over because time us up, but the player hasn't delivered all orders, you get 0 star
                return 0;
            }
            else{
                // The game ends when the player has delivered all orders, you get 1 star plus 1 more star for each 15seconds remaining
                remainingTime = timer.secondsLeft + timer.minutesLeft * 60;
                return 1 + (int)Mathf.Min((remainingTime / 15), 2);
            }
        }
        else{
            // The game ends when the time is up
            if (numDeliveredOrders < numTotalOrders){
                // The game is over because time is up but the player hasn't delivered the minimum number of orders, you get 0 star
                return 0;
            }
            else{
                // The game ends when time is up, you get 1 star if the player delivered the minimum number of orders, plus 1 star 
                // for each additional 2 orders
                return 1 + (int)Mathf.Min(2, numDeliveredOrders/2); 
            }
        }
    }

}
