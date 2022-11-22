using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using Pathfinding;

public class Rabbit_TaskMoveToMate : Node
{
	private Transform _transform;
	private Rabbit rabbit;
	private AIPath ai;
	
	public Rabbit_TaskMoveToMate(Transform transform)
	{
		_transform = transform;
		ai = _transform.GetComponent<AIPath>();
		rabbit = _transform.GetComponent<Rabbit>();
	}

	public override NodeState Evaluate()
	{
		Transform mate = rabbit.Mate.transform;
		if (mate == null)
		{
			state = NodeState.FAILURE;
			return state;
		}
		rabbit.action = Animal.Action.MATING;
		ai.destination = mate.position;
		if (ai.reachedDestination)
		{
			state = NodeState.SUCCESS;
			return state;
		}

		state = NodeState.RUNNING;
		return state;		
	}
}