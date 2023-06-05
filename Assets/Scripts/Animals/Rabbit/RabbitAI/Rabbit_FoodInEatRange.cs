using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class Rabbit_FoodInEatRange : Node
{
    private Transform _transform;
	private Rabbit rabbit;

    public Rabbit_FoodInEatRange(Transform transform)
    {
        _transform = transform;
		rabbit = _transform.GetComponent<Rabbit>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("Food");
        if (target == null || rabbit.action == Animal.Action.DRINKING || rabbit.action == Animal.Action.MATING)
        {
            state = NodeState.FAILURE;
            return state;
        }
        
        if (Vector3.Distance(_transform.position, target.position) < rabbit.eatRange)
        {
            
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;

    }
}
