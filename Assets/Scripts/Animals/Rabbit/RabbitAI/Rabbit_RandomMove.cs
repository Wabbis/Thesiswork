using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;


public class Rabbit_RandomMove : Node
{
    private Transform _transform;
    private Vector3 _destination;
    private float _waitTime = 1f;
    private float _waitCounter = 0f;
    private bool _waiting = false;
    private float _searchRadius;
	private float yOffset;

    public Rabbit_RandomMove(Transform transform, float searchRadius)
    {
        _transform = transform;
        _searchRadius = searchRadius;
		yOffset = transform.GetComponent<MeshCollider>().bounds.center.y;
		_destination = Utility.FindRandomPointOnNavMesh(_transform, _searchRadius);
		_destination.y = 2.251461f;
		_transform.GetComponent<NavMeshAgent>().SetDestination(_destination);
	}

    public override NodeState Evaluate()
    {
		Debug.Log(Vector3.Distance(_transform.position, _destination));
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
			if (Vector3.Distance(_transform.position, _destination) < 0.5f)
			{
				_transform.position = _destination;
				_waitCounter = 0f;
				_waiting = true;
				_destination = Utility.FindRandomPointOnNavMesh(_transform, _searchRadius);
				_destination.y = 2.251461f;
				_transform.GetComponent<NavMeshAgent>().SetDestination(_destination);
				
			}
			else
			{
				
				//_transform.position = Vector3.MoveTowards(
				//	_transform.position,
				//	_destination,
				//	RabbitBT.rabbit.speed * Time.deltaTime);
				//_transform.LookAt(_destination);
			}
		}

        state = NodeState.RUNNING;
        return state;
    }

	
}
