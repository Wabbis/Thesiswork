using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class Rabbit_TargetInEatRange : Node
{
    private Transform _transform;

    public Rabbit_TargetInEatRange(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        
        if (Vector3.Distance(_transform.position, target.position) < RabbitBT.rabbit.eatRange)
        {
            Debug.Log("Food in range");
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;

    }
}
