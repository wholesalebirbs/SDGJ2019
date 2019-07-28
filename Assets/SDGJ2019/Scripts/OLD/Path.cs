using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public enum PathType { Circuit, Linear };
    public enum PathDirection { Clockwise, Counterclockwise };

    public PathType pathType;

    public Transform[] nodes;

    private int currentPathIndex;
    private PathDirection pathDirection = PathDirection.Clockwise;

    // Start is called before the first frame update
    void Start()
    {
        if(nodes.Length < 2)
        {
            Debug.LogError("Path must have 2 or more nodes!");
        }
    }

    public Transform GetNextPathNode()
    {
        if(pathType == PathType.Circuit)
        {
            if(pathDirection == PathDirection.Clockwise)
            {
                if(currentPathIndex == nodes.Length - 1)
                {
                    currentPathIndex = 0;
                }
                else
                {
                    currentPathIndex++;
                }
            }
            else
            {
                if (currentPathIndex == 0)
                {
                    currentPathIndex = nodes.Length - 1;
                }
                else
                {
                    currentPathIndex--;
                }
            }
        } 
        else if (pathType == PathType.Linear)
        {
            if (pathDirection == PathDirection.Clockwise)
            {
                if (currentPathIndex == nodes.Length - 1)
                {
                    pathDirection = PathDirection.Counterclockwise;
                    currentPathIndex--;
                }
                else
                {
                    currentPathIndex++;
                }
            }
            else
            {
                if (currentPathIndex == 0)
                {
                    pathDirection = PathDirection.Clockwise;
                    currentPathIndex++;
                }
                else
                {
                    currentPathIndex--;
                }
            }
        }

        return nodes[currentPathIndex];
    }

    public void SetPathDirection(PathDirection direction)
    {
        pathDirection = direction;
    }

    public Transform GetClosestPathNode(Vector3 position)
    {
        float shortestDistance = float.MaxValue;
        Transform closestNode = nodes[0];
        int index = 0;
        foreach(Transform node in nodes)
        {
            float dist = Vector3.Distance(position, node.position);
            if (dist < shortestDistance)
            {
                closestNode = node;
                shortestDistance = dist;
                currentPathIndex = index;
            }
            index++;
        }

        return closestNode;
    }
}
