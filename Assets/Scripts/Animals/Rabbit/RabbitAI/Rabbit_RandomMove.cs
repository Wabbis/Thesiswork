using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;
using Pathfinding;

public class Rabbit_RandomMove : Node
{
    private Transform _transform;
    private Vector3 _destination;
    private float _waitTime = 5f;
    private float _waitCounter = 0f;
    private bool _waiting = false;
    private float _searchRadius;
	private static AIPath ai;

    public Rabbit_RandomMove(Transform transform, float searchRadius)
    {
        _transform = transform;
		_searchRadius = searchRadius;
		ai = transform.GetComponent<AIPath>();
		_destination = Utility.FindRandomNodeOnAstarGrid(_transform, _searchRadius);
		ai.destination = _destination;
	}

	

    public override NodeState Evaluate()
    {
		if (_waiting)
		{
			_waitCounter += Time.deltaTime;
			if (_waitCounter >= _waitTime)
			{
				_waiting = false;
			}
		}
		else
		{
			if (ai.reachedDestination)
			{
				_waitCounter = 0f;
				_waiting = true;
				_destination = Utility.FindRandomNodeOnAstarGrid(_transform, _searchRadius);
				ai.destination = _destination;
				
			}

		}

        state = NodeState.RUNNING;
        return state;
    }

	
}
