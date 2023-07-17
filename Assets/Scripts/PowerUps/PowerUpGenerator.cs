using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] powerUps;
    [SerializeField]
    private Transform[] positions;
    private GameObject spawnedPowerUp;
    private Transform pos;

    private int randomPowerUpIndex, randomPositionIndex;

    void Start()
    {
        StartCoroutine(GeneratePowerUp());
    }

    IEnumerator GeneratePowerUp(){
        while (true){
            yield return new WaitForSeconds(Random.Range(10, 15));

            randomPowerUpIndex = Random.Range(0, powerUps.Length);
            randomPositionIndex = Random.Range(0, positions.Length);

            spawnedPowerUp = Instantiate(powerUps[randomPowerUpIndex]);
            pos = positions[randomPositionIndex];

            spawnedPowerUp.transform.position = pos.position;
        } 
    }
}
