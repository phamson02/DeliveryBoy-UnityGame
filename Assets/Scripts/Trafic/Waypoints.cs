using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [Range(0f, 2f)]
    [SerializeField] private float waypointSize = 1f;

    [Header("Path Setting")]
    [SerializeField] private bool canloop = true;

    [SerializeField] private bool isMovingForward = true;
    private void OnDrawGizmos()
    {
        foreach(Transform t in transform)
        {   
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(t.position, waypointSize);
        }

        Gizmos.color = Color.red;
        for(int i = 0; i < transform.childCount -1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
        if (canloop)
        {
            Gizmos.DrawLine(transform.GetChild(transform.childCount -1).position, transform.GetChild(0).position);
        }
    }

    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
        if (currentWaypoint == null)
        {
            return transform.GetChild(0);
        }

        int currentIndex = currentWaypoint.GetSiblingIndex();
        int nextIndex = currentIndex;

        if (isMovingForward)
        {
            nextIndex += 1;
            if (nextIndex == transform.childCount)
            {
                if (canloop)
                {
                    nextIndex = 0;
                } else {
                    nextIndex -= 1;
                }
            }
        } else {
            nextIndex -= 1;

            if (nextIndex < 0)
            {
                if (canloop)
                {
                    nextIndex = transform.childCount -1;
                } else {
                    nextIndex += 1;
                }
            }
        }

        return transform.GetChild(nextIndex);
    }
}