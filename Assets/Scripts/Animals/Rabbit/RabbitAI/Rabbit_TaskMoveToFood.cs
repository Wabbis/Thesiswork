using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Rabbit_TaskMoveToFood : Node
{
    private Transform _transform;
	private float yOffset;
    public Rabbit_TaskMoveToFood(Transform transform)
    {
        _transform = transform;
		yOffset = transform.GetComponent<MeshCollider>().bounds.center.y;
	}

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

		if (target == null) 
		{
			state = NodeState.FAILURE;
			return state;
		}

        if (Vector3.Distance(_transform.position, target.position) > 0.3f )
        {
            
            _transform.position = Vector3.MoveTowards(
                _transform.position,
                new Vector3(target.position.x, _transform.position.y, target.position.z),
                RabbitBT.rabbit.speed * Time.deltaTime);
			_transform.LookAt(new Vector3(target.position.x, yOffset, target.position.z));
        } 
		else
		{
			state = NodeState.SUCCESS;
			return state;
		}

        state = NodeState.RUNNING;
        return state;
    }
}
