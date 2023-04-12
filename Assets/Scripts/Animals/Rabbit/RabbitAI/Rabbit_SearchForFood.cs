using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Rabbit_SearchForFood : Node
{
    private Transform _transform;
    private LayerMask _foodLayerMask = LayerMask.GetMask("Plant");
	private Rabbit rabbit;


    public Rabbit_SearchForFood(Transform transform)
    {
        _transform = transform;
		rabbit = _transform.GetComponent<Rabbit>();
	}

    public override NodeState Evaluate()
    {
		if (rabbit.hunger < rabbit.eatingThreshold || rabbit.action == Animal.Action.MATING || rabbit.action == Animal.Action.DRINKING)
		{
			state = NodeState.FAILURE;
			return state;
		}
        object target = GetData("Food");

        if (target == null)
        {
            
            Collider[] collider = Physics.OverlapSphere(_transform.position, 
				rabbit.visionRange, 
				_foodLayerMask);

            if(collider.Length > 0)
            {
                Debug.Log("Food found");
                parent.parent.SetData("Food", collider[0].transform);
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
