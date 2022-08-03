using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using Pathfinding;
public class Rabbit_TaskMoveToFood : Node
{
    private Transform _transform;
	private AIPath ai;
    public Rabbit_TaskMoveToFood(Transform transform)
    {
        _transform = transform;
		ai = transform.GetComponent<AIPath>();
	}

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("Food");

		if (target == null) 
		{
			state = NodeState.FAILURE;
			return state;
		}

		ai.destination = target.position;
		
        if (ai.reachedDestination)
        {
			state = NodeState.SUCCESS;
			return state;
		}

        state = NodeState.RUNNING;
        return state;
    }
}
