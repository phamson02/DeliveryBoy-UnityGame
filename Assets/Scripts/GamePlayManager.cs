using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    private GameObject[] buildings;
    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject pointer;
    private Vector3 pointerPosition;

    [SerializeField]
    private TargetIndicator questPointer;

    [SerializeField]
    private Button recieveButton, deliverButton;

    private GameObject shop, destination;
    private int destinationIndex, shopIndex;
    private bool inProcess=false, carryingOrder=false;

    // Start is called before the first frame update
    void Start()
    {
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
