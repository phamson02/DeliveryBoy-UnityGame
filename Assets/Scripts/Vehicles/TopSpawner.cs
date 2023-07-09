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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnVehicles());
    }

    IEnumerator SpawnVehicles(){
        while (true){

            yield return new WaitForSeconds(Random.Range(3, 5));

            randomIndex = Random.Range(0, vehiclesReference.Length);

            spawnedVehicles = Instantiate(vehiclesReference[randomIndex]);

            spawnedVehicles.transform.position = pos.position;
            spawnedVehicles.GetComponent<VerticalVehicle>().speed = -Random.Range(4, 10);

        } 
    }
}