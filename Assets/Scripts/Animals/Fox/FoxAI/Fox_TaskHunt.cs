using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using Pathfinding;
public class Fox_TaskHunt : Node
{
	private Transform _transform;
	private AIPath ai;
	private Fox fox;

	public Fox_TaskHunt(Transform transform)
	{
		_transform = transform;
		fox = transform.GetComponent<Fox>();
		ai = transform.GetComponent<AIPath>();
	}

	public override NodeState Evaluate()
	{
		Transform target = (Transform)GetData("Food");
		if (target == null)
		{
			state = NodeState.FAILURE;
			return state;
		}

		ai.destination = target.position;
		ai.maxSpeed = fox.huntSpeed;
		

		if(ai.reachedDestination)
		{
			state = NodeState.SUCCESS;
			return state;
		}

		if(fox.energy < 5 || ai.remainingDistance > 50)
		{
			ClearData("Food");
			ai.maxSpeed = fox.normalSpeed;
		}

		state = NodeState.RUNNING;
		return state;
	}
}