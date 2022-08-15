using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Rabbit_TaskMoveToMate : Node
{
	private Transform _transform;
	private float yOffset;
	
	public Rabbit_TaskMoveToMate(Transform transform)
	{
		_transform = transform;
	}

	public override NodeState Evaluate()
	{
		Transform mate = (Transform)GetData("Mate");

		if (mate == null)
		{
			state = NodeState.FAILURE;
			return state;
		}
		
		if (Vector3.Distance(_transform.position, mate.position) > 1.5f) 
		{
			_transform.position = Vector3.MoveTowards(
				_transform.position,
				mate.position,
				RabbitBT.rabbit.speed * Time.deltaTime);
			_transform.LookAt(new Vector3(mate.position.x, yOffset, mate.position.z));
		}

		state = NodeState.RUNNING;
		return state;		
	}
}