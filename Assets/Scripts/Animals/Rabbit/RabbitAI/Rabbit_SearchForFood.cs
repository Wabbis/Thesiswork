using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Rabbit_SearchForFood : Node
{
    private Transform _transform;
    private LayerMask _foodLayerMask = LayerMask.GetMask("Plant");


    public Rabbit_SearchForFood(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
		if(RabbitBT.rabbit.hunger < .2f)
		{
			state = NodeState.FAILURE;
			return state;
		}
        object target = GetData("Food");

        if (target == null)
        {
            
            Collider[] collider = Physics.OverlapSphere(_transform.position, 
				RabbitBT.rabbit.visionRange, 
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
