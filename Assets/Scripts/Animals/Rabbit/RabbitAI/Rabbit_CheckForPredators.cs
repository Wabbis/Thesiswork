using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Rabbit_CheckForPredators : Node
{
	private Transform _transform;
	private Rabbit rabbit;

	public Rabbit_CheckForPredators(Transform transform)
	{
		_transform = transform;
		rabbit = transform.GetComponent<Rabbit>();
	}

	public override NodeState Evaluate()
	{
		if(!rabbit.isHunted)
		{
			state = NodeState.FAILURE;
			return state;
		}

		Transform predator = (Transform)GetData("predator");
		if (predator == null)
		{
			parent.SetData("predator", rabbit.Predator);
			state = NodeState.SUCCESS;
			return state;
		}

		state = NodeState.SUCCESS;
		return state;
	}
}
