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
	
	public static Vector3 FindPointAroundTransform(Transform transform, float radius)
	{
		Vector3 randomPoint = Vector3.zero;
		do
		{
			Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
			randomPoint = randomDirection + transform.position;
			randomPoint.y = 1;

		} while (!Physics.Raycast(randomPoint, Vector3.down, 2, LayerMask.GetMask("Grass")));

		randomPoint.y = 0;
		return randomPoint;
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

	public static Vector3 FindRandomNodeOnAstarGrid(float radius)
	{
		Vector3 randomPoint = Vector3.zero;
		GraphNode randomNode;
		do
		{
			Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
			
			randomNode = AstarPath.active.GetNearest(randomDirection).node;
			if (randomNode.Walkable)
			{
				randomPoint = randomNode.RandomPointOnSurface(); 
			}
		} while (!randomNode.Walkable);
		return randomPoint;
	}

	public static Vector3 VectorFromAngle(float AngleInDegrees)
		{
			float angleInRads = AngleInDegrees * (Mathf.PI / 180f);
			return new Vector3(Mathf.Sin(angleInRads), 0, Mathf.Cos(angleInRads));
		
		}

		public static float AngleFromVector3(Vector3 vector)
		{
			vector = vector.normalized;

			float f = Mathf.Atan2(vector.x, vector.z) * Mathf.Rad2Deg;
		
			if (f < 0) f += 360;

			Debug.Log("F: " + f + "Vector z: " + vector.z + "Vector x: " + vector.x);
			return f;
		}

	
}
