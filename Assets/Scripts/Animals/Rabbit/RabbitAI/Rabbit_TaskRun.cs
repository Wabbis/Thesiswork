using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using Pathfinding;

public class Rabbit_TaskRun : Node
{
	private Transform _transform;
	private AIPath ai;
	private Rabbit rabbit;

	public Rabbit_TaskRun(Transform transform)
	{
		_transform = transform;
		ai = transform.GetComponent<AIPath>();
		rabbit = transform.GetComponent<Rabbit>();
	}


	public override NodeState Evaluate()
	{
		Transform predator = (Transform)GetData("predator");
		if (predator == null)
		{
			state = NodeState.FAILURE;
			return state;
		}
		else
		{
			
			if	(ai.reachedDestination)
			{
				
				Vector3 direction = (_transform.position - predator.position).normalized;
				GraphNode destinationNode;
				do
				{
					
					
					direction = Quaternion.AngleAxis(Random.Range(1, rabbit.searchRadius), Vector3.up) * direction * Random.Range(1, rabbit.searchRadius);
					direction += _transform.position;
					
					destinationNode = AstarPath.active.GetNearest(direction).node;
					if (destinationNode.Walkable)
					{
						
						direction = destinationNode.RandomPointOnSurface();
					}
				} while (!destinationNode.Walkable);
				Debug.Log(direction);

				ai.destination = direction;

				// RUN AWAY 
				// ALSO WHAT IF MULTIPLE PREDATORS? 
			}
		}
		state = NodeState.RUNNING;
		return state;
	}
}
