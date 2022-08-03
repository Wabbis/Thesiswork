using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class Rabbit_SearchForWater : Node
{
	private Transform _transform;
	private LayerMask _waterLayerMask = LayerMask.GetMask("Water");
	private List<Transform> memory;

	public Rabbit_SearchForWater(Transform transform)
	{
		_transform = transform;
	}

	public override NodeState Evaluate()
	{
		if (RabbitBT.rabbit.thirst < 0.2f) 
		{
			state = NodeState.FAILURE;
			return state;
		}

		object target = GetData("Water");

		if (target == null)
		{
			Collider[] collider = Physics.OverlapSphere(_transform.position,
				RabbitBT.rabbit.visionRange,
				_waterLayerMask);
			if(collider.Length == 0 && RabbitBT.rabbit.thirst > 0.8f)
			{
				if(memory.Count == 0)
				{
					state = NodeState.FAILURE;
					return state;
				}
				Transform closest = null;
				float distance = float.MaxValue;
				foreach (Transform transform in memory)
				{
					if (Vector3.Distance(transform.position, _transform.position) < distance)
					{
						closest = transform;
						distance = Vector3.Distance(transform.position, _transform.position);
					}
				}

				Debug.Log("water found from memory");
				parent.parent.SetData("Water", closest);
				state = NodeState.SUCCESS;
				return state;
			}


			if(collider.Length > 0)
			{
				Debug.Log("Water found");
				parent.parent.SetData("Water", collider[0].transform);
				if (!memory.Contains(collider[0].transform))
				{
					memory.Add(collider[0].transform);
				}
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
