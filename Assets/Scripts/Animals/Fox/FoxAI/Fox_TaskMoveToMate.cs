using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using Pathfinding;

public class Fox_TaskMoveToMate : Node
{
	private Transform _transform;
	private Fox fox;
	private AIPath ai;

	public Fox_TaskMoveToMate(Transform transform)
	{
		_transform = transform;
		ai = transform.GetComponent<AIPath>();
		fox = transform.GetComponent<Fox>();
	}

	public override NodeState Evaluate()
	{
		
		Transform mate = fox.Mate.transform;

		if(mate == null)
		{
			state = NodeState.FAILURE;
			return state;
		}
		fox.action = Animal.Action.MATING;
		ai.destination = mate.position; 
		if (Vector3.Distance(_transform.position, mate.position) < 2.5f)
		{
			Debug.Log("??");
			state = NodeState.SUCCESS;
			return state;
		}

		state = NodeState.RUNNING;
		return state;
	}
}
