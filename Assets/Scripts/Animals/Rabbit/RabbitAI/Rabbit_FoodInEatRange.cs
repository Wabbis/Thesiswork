using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class Rabbit_FoodInEatRange : Node
{
    private Transform _transform;

    public Rabbit_FoodInEatRange(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("Food");
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
