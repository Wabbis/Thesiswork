using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class Rabbit_TaskMate : Node
{
	private Transform _transform;

	private bool mating = false;
	private float timeCounter = 0f;

	public Rabbit_TaskMate(Transform transform)
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

		RabbitBT.rabbit.action = Animal.Action.MATING;

		timeCounter += Time.deltaTime;
		if (timeCounter > 3f)
		{
			Debug.Log("Mating ended");
			RabbitBT.rabbit.action = Animal.Action.NONE;
			timeCounter = 0f;
			state = NodeState.SUCCESS;
			ClearData("Mate");
			RabbitBT.rabbit.MatingEnded();
			return state;


		}

		state = NodeState.RUNNING;
		return state;
	}
}