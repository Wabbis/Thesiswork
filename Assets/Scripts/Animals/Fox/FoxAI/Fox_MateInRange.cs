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
		if (fox.Mate == null)
		{
			state = NodeState.FAILURE;
			return state;
		}

		Debug.Log("?=");
		Transform mate = fox.Mate.transform;
		// TODO better value to compare against

		Debug.Log(Vector3.Distance(_transform.position, mate.position));
		if (Vector3.Distance(_transform.position, mate.position) < 2.5f )
		{
			state = NodeState.SUCCESS;
			return state;
		}

		state = NodeState.FAILURE;
		return state;
	}
}
