using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using Pathfinding;

public class Rabbit_WaterInRange : Node
{
	private Transform _transform;
	private AIPath ai;
	private Rabbit rabbit;
	public Rabbit_WaterInRange(Transform transform)
	{
		
		_transform = transform;
		rabbit = _transform.GetComponent<Rabbit>();
		ai = transform.GetComponent<AIPath>();
	}

	public override NodeState Evaluate()
	{
		Transform target = (Transform)GetData("Water");
		if (target == null || rabbit.action == Animal.Action.EATING || rabbit.action == Animal.Action.MATING)
		{
			state = NodeState.FAILURE;
			return state;
		}
		if (Vector3.Distance(_transform.position, target.position) < rabbit.drinkRange)
		{
			ai.destination = _transform.position;
			state = NodeState.SUCCESS;
			return state;
		}

		state = NodeState.FAILURE;
		return state;
	}
}
