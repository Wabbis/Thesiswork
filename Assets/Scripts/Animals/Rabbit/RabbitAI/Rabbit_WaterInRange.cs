using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using Pathfinding;

public class Rabbit_WaterInRange : Node
{
	private Transform _transform;
	private AIPath ai;
	public Rabbit_WaterInRange(Transform transform)
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
		if (Vector3.Distance(_transform.position, target.position) < RabbitBT.rabbit.drinkRange)
		{
			ai.destination = _transform.position;
			state = NodeState.SUCCESS;
			return state;
		}

		state = NodeState.FAILURE;
		return state;
	}
}
