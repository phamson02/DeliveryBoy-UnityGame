using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSpawner : VehicleSpawner
{
    [SerializeField]
    private GameObject[] vehiclesReference;

    [SerializeField]
    private Transform pos;

    private GameObject spawnedVehicles;
    private int randomIndex;
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
                spawnedVehicles.GetComponent<HorizontalVehicle>().speed = carSpeed;
                spawnedVehicles.transform.localScale = new Vector3(-1f, 1f, 1f);

                yield return new WaitForSeconds(2f);
            }

        } 
    }
}
