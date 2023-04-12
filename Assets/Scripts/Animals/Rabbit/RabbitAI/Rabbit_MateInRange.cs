using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class Rabbit_MateInRange : Node
{
	private Transform _transform;
	private Rabbit rabbit;

	public Rabbit_MateInRange(Transform transform)
	{
		_transform = transform;
		rabbit = transform.GetComponent<Rabbit>();
	}

	public override NodeState Evaluate()
	{

		if (rabbit.Mate == null)
		{
			state = NodeState.FAILURE;
			return state;
		}

		Transform mate = rabbit.Mate.transform;
		if (Vector3.Distance(_transform.position, mate.position) < 1.5f)
		{
			state = NodeState.SUCCESS;
			return state;
		}

		state = NodeState.FAILURE;
		return state;
		
	}
}
