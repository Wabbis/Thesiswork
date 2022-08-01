using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Fox_RandomMove : Node
{
    private Transform _transform;
    private Vector3 _destination;
    private float _waitTime = 1f;
    private float _waitCounter = 0f;
    private bool _waiting = false;
    private float _searchRadius;

    Fox_RandomMove(Transform transform, float searchRadius)
    {
        _transform = transform;
        _searchRadius = searchRadius;
        _destination = Utility.FindRandomPointOnNavMesh(_transform, _searchRadius);
        _destination.y = 0.5f;
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
            if (Vector3.Distance(_transform.position, _destination) < 0.01f)
            {
                _transform.position = _destination;
                _waitCounter = 0.0f;
                _waiting = true;
                _destination = Utility.FindRandomPointOnNavMesh(_transform, _searchRadius);
                _destination.y = 0.5f;
            }
            else
            {
                _transform.position = Vector3.MoveTowards(
                    _transform.position,
                    _destination,
                    FoxBT.animal.speed * Time.deltaTime);
                _transform.LookAt(_destination);
            }
        }
        Debug.Log("Fox searching for food...");
        state = NodeState.RUNNING;
        return state;
    }
}
