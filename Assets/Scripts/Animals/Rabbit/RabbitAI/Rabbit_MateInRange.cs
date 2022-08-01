using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class Rabbit_MateInRange : Node
{
	private Transform _transform;

	public Rabbit_MateInRange(Transform transform)
	{
		_transform = transform; 
	}

	public override NodeState Evaluate()
	{

		Transform mate = (Transform)GetData("Mate");
		if (mate == null)
		{
			state = NodeState.FAILURE;
			return state;
		}

		if(Vector3.Distance(_transform.position, mate.position) < 1.5f)
		{
			Debug.Log("mate in range");
			state = NodeState.SUCCESS;
			return state;
		}

		state = NodeState.FAILURE;
		return state;
		
	}
}
