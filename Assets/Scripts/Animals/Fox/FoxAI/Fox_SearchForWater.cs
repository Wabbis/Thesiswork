using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using Pathfinding;

public class Fox_SearchForWater : Node
{
	private Transform _transform;
	private AIPath ai;
	private LayerMask _waterLayerMask = LayerMask.GetMask("Water");
	private List<Transform> memory = new List<Transform>();
	private Fox fox;

	public Fox_SearchForWater(Transform transform)
	{
		_transform = transform;
		ai = transform.GetComponent<AIPath>();
		fox = transform.GetComponent<Fox>();
	}

	public override NodeState Evaluate()
	{
		if (fox.thirst < 0.2f || (fox.action == Animal.Action.EATING && fox.thirst > 0.8f) || fox.action == Animal.Action.MATING)
		{
			state = NodeState.FAILURE;
			return state;
		}

		Transform target = (Transform)GetData("Water");

		if (target == null)
		{
			Collider[] collider = Physics.OverlapSphere(_transform.position,
				fox.visionRange,
				_waterLayerMask);
			if (collider.Length == 0 && fox.thirst > 0.8f)
			{
				if (memory.Count == 0)
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
				Debug.Log("Water found from memory");
				parent.parent.SetData("Water", closest);
				state = NodeState.SUCCESS;
				return state;
			}

			if (collider.Length > 0)
			{
				Transform closest = null;
				float closestDistance = float.MaxValue;
				foreach (Collider col in collider)
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


