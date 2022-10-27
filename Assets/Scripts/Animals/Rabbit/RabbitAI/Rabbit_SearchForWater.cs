using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using Pathfinding;
public class Rabbit_SearchForWater : Node
{
	private Transform _transform;
	private AIPath ai;
	private LayerMask _waterLayerMask = LayerMask.GetMask("Water");
	private List<Transform> memory = new List<Transform>();
	private Rabbit rabbit;
public Rabbit_SearchForWater(Transform transform)
	{
		_transform = transform;
		ai = transform.GetComponent<AIPath>();
		rabbit = _transform.GetComponent<Rabbit>();
	}

	public override NodeState Evaluate()
	{
		if (rabbit.thirst < 0.2f || rabbit.action != Animal.Action.EATING || rabbit.action != Animal.Action.MATING) 
		{
			state = NodeState.FAILURE;
			return state;
		}

		object target = GetData("Water");

		if (target == null)
		{
			Collider[] collider = Physics.OverlapSphere(_transform.position,
				rabbit.visionRange,
				_waterLayerMask);
			if(collider.Length == 0 && rabbit.thirst > 0.8f)
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
				Transform closest = null;
				float closestDistance = float.MaxValue;
				foreach(Collider col in collider)
				{
					float distance = Vector3.Distance(col.transform.position, _transform.position);

					if (distance < closestDistance)
					{
						closest = col.transform;
						closestDistance = distance;
					}
				}
				parent.parent.SetData("Water", closest);
				if (!memory.Contains(closest))
				{
					memory.Add(closest);
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
