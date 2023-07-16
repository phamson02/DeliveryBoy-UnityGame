using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {   
        level = PlayerPrefs.GetInt("SelectedLevel");
        Debug.Log("LEVELDEBUGGER: " + level);

        // Settings for the level

        foreach (VehicleSpawner vehicleSpawner in vehicleSpawners){
                vehicleSpawner.carSpeed = 3 + level;
                vehicleSpawner.carsPerSpawn = (level - 1) / 2 + 1;
            }
        
        if (level == 1){
            
        }
        else if (level == 2){

        }
        else if (level == 3){
            
        }
        else if (level == 4){
            
        }
        else if (level == 5){
            
        }
        else if (level == 6){
            
        }
        else if (level == 7){
            
        }
        else if (level == 8){
            
        }

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

}
