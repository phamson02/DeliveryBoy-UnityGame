using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{   
    [SerializeField] private float distanceThreshold = 0.1f;
    [SerializeField] private Waypoints waypoints ;
    [SerializeField] private float moveSpeed = 5f;

    [Range(0f, 15f)]
    [SerializeField] private float rotateSpeed = 4f;

    public Transform currentWaypoint;

    private Quaternion rorationGoal;
    private Vector3 directionToWaypoint;

    public Transform MyCurrentWaypoint
    {
        get { return currentWaypoint; }
        set { currentWaypoint = value; }
    }

    public Waypoints MyWaypoint
    {
        get { return waypoints; }
        set { waypoints = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.LookAt(currentWaypoint);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
        {
            currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
            // transform.LookAt(currentWaypoint);
        }
        RotateTowardsWaypoint();
    }

    private void RotateTowardsWaypoint()
    {
        directionToWaypoint = (currentWaypoint.position - transform.position).normalized;
        rorationGoal = Quaternion.LookRotation(directionToWaypoint);
        transform.rotation = Quaternion.Slerp(transform.rotation, rorationGoal, rotateSpeed * Time.deltaTime);
    }

}
