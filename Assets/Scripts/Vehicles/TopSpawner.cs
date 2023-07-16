using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] vehiclesReference;

    [SerializeField]
    private Transform pos;

    private GameObject spawnedVehicles;
    private int randomIndex;

    [HideInInspector]
    public int carsPerSpawn = 3;
    [HideInInspector]
    public float carSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnVehicles());
    }

    IEnumerator SpawnVehicles(){
        while (true){

            yield return new WaitForSeconds(Random.Range(7, 10));

            for (int i=0; i<carsPerSpawn; i++){
                randomIndex = Random.Range(0, vehiclesReference.Length);

                spawnedVehicles = Instantiate(vehiclesReference[randomIndex]);

                spawnedVehicles.transform.position = pos.position;
                spawnedVehicles.GetComponent<VerticalVehicle>().speed = -carSpeed;

                yield return new WaitForSeconds(2f);
            }
        
        } 
    }
}
