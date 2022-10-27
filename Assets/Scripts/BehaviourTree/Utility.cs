using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Pathfinding;

    public class Utility
    { 
        public static Vector3 FindRandomPointOnNavMesh(Transform transform, float radius)
        {
            Vector3 pointOnNavMesh = Vector3.zero;
            Vector3 randomDir = UnityEngine.Random.insideUnitSphere * radius;
            randomDir += transform.position;
            if (NavMesh.SamplePosition(randomDir, out NavMeshHit hit, radius, 1))
            {
                pointOnNavMesh = hit.position;
            }

            return pointOnNavMesh;
        }

	public static Vector3 FindRandomNodeOnAstarGrid(Transform transform, float radius)
	{
		Vector3 randomPoint = Vector3.zero;
		GraphNode randomNode;
		do
		{
			Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
			randomDirection += transform.position;
			randomNode = AstarPath.active.GetNearest(randomDirection).node;
			if (randomNode.Walkable)
			{
				randomPoint = randomNode.RandomPointOnSurface();
			}
		} while (!randomNode.Walkable);
		return randomPoint;
	}
    }
