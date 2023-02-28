using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using Pathfinding;

public class Fox_TaskMoveToWater : Node
{
	private Transform _transform;
	private AIPath ai;

	public Fox_TaskMoveToWater(Transform transform)
	{
		_transform = transform;
		ai = transform.GetComponent<AIPath>();
	}

	public override NodeState Evaluate()
	{
		Transform target = (Transform)GetData("Water");

		if (target == null)
		{
			state = NodeState.FAILURE;
			return state;
		}

		GraphNode closestNode = AstarPath.active.GetNearest(target.position).node;
		ai.destination = closestNode.RandomPointOnSurface();
		if (ai.reachedEndOfPath)
		{
			ai.destination = _transform.position;
			state = NodeState.SUCCESS;
			return state;
		}

		state = NodeState.RUNNING;
		return state;
	}


}
