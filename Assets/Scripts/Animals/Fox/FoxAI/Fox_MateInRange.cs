using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Fox_MateInRange : Node 
{ 
	private Transform _transform;
	private Fox fox;

	public Fox_MateInRange(Transform transform)
	{
		_transform = transform;
		fox = transform.GetComponent<Fox>();
	}

	public override NodeState Evaluate()
	{
		// Two mates in range somehow???
		if (fox.action == Animal.Action.MATING)
		{
			state = NodeState.FAILURE;
			return state;
		}


		if (fox.Mate == null)
		{
			state = NodeState.FAILURE;
			return state;
		}


		Transform mate = fox.Mate.transform;

		if (Vector3.Distance(_transform.position, mate.position) < 1.5f)
		{
			state = NodeState.SUCCESS;
			return state;
		}

		state = NodeState.FAILURE;
		return state;
	}
}
