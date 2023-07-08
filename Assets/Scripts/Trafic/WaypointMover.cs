using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    [SerializeField] private float distanceThreshold = 0.1f;
    [SerializeField] private Waypoints waypoints;
    [SerializeField] private float moveSpeed = 5f;
    [Range(0f, 15f)]
    [SerializeField] private float rotateSpeed = 4f;

    public Transform currentWaypoint;

    private Quaternion rotationGoal;
    private Vector2 directionToWaypoint;

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
        transform.up = currentWaypoint.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
        {
            currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
            transform.up = currentWaypoint.position - transform.position;
        }

        RotateTowardsWaypoint();
    }

    private void RotateTowardsWaypoint()
    {
        directionToWaypoint = (currentWaypoint.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionToWaypoint.y, directionToWaypoint.x) * Mathf.Rad2Deg;
        rotationGoal = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, rotateSpeed * Time.deltaTime);
    }
}
