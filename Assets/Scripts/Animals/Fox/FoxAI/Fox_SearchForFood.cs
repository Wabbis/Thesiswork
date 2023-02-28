using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class Fox_SearchForFood : Node
{
	private Transform _transform;
	private LayerMask _foodLayerMask = LayerMask.GetMask("Rabbit");
	private Fox fox;

	public Fox_SearchForFood(Transform transform)
	{
		_transform = transform;
		fox = transform.GetComponent<Fox>();
	}

	public override NodeState Evaluate()
	{
		if (fox.hunger < .2f || fox.action == Animal.Action.MATING || fox.action == Animal.Action.DRINKING)
		{
			state = NodeState.FAILURE;
			return state;
		}

		Transform target = (Transform)GetData("Food");
		if(target == null)
		{
			Collider[] collider = Physics.OverlapSphere(_transform.position,
				fox.visionRange,
				_foodLayerMask);

			if(collider.Length > 0)
			{
				Debug.Log("Prey found");
				parent.parent.SetData("Food", collider[0].transform);
				state = NodeState.SUCCESS;
				return state;
			}

			state = NodeState.RUNNING;
			return state;
		}

		state = NodeState.SUCCESS;
		return state;
	}
}