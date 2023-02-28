using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using Pathfinding;
public class Fox_WaterInRange : Node
{
	private Transform _transform;
	private AIPath ai;
	private Fox fox;

	public Fox_WaterInRange(Transform transform)
	{
		_transform = transform;
		fox = transform.GetComponent<Fox>();
		ai = transform.GetComponent<AIPath>();
	}

	public override NodeState Evaluate()
	{
		Transform target = (Transform)GetData("Water");
		if(target == null || fox.action == Animal.Action.EATING || fox.action == Animal.Action.MATING)
		{
			state = NodeState.FAILURE;
			return state;
		}
		if(Vector3.Distance(_transform.position, target.position) < fox.drinkRange)
		{
			ai.destination = _transform.position;
			state = NodeState.SUCCESS;
			return state;
		}

		state = NodeState.FAILURE;
		return state;
	}
}
