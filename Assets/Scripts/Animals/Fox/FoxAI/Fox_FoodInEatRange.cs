using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using Pathfinding;
public class Fox_FoodInEatRange : Node
{
	private Transform _transform;
	private Fox fox;

	public Fox_FoodInEatRange(Transform transform)
	{
		_transform = transform;
		fox = transform.GetComponent<Fox>();
	}

	public override NodeState Evaluate()
	{
		Transform target = (Transform)GetData("Food");
		if (target == null || fox.action == Animal.Action.DRINKING || fox.action == Animal.Action.MATING)
		{
			state = NodeState.FAILURE;
			return state;
		}

		if (Vector3.Distance(_transform.position, target.position) < fox.eatRange)
		{
			Debug.Log("Food in range");
			target.GetComponent<AIPath>().enabled = false;
			state = NodeState.SUCCESS;
			return state;
		}

		state = NodeState.FAILURE;
		return state;
	}
}
