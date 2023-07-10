using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carprefabs;
    [SerializeField] private Waypoints waypoints ;

    private void Start()
    {
        foreach (Transform waypoint in waypoints.transform)
        {
            GameObject car = Instantiate(SelectACarPrefab(), transform);
            car.GetComponent<WaypointMover>().currentWaypoint = waypoint;
        }
        // Instantiate(SelectACarPrefab(), transform);
    }

    private GameObject SelectACarPrefab()
    {
        var randomIndex = Random.Range(0, carprefabs.Length);
        return carprefabs[randomIndex];
    }
}
