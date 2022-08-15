using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Rabbit_TaskDrink : Node
{
	private Transform _transform;
	private float timeCounter = 0f;
	
	public Rabbit_TaskDrink(Transform transform)
	{
		_transform = transform;
	}

	public override NodeState Evaluate()
	{
		Transform target = (Transform)GetData("Water");

		if (target == null)
		{
			state = NodeState.FAILURE;
			return state;
		}

		if (RabbitBT.rabbit.action != Animal.Action.MATING)
		{
			RabbitBT.rabbit.action = Animal.Action.DRINKING;

			if(RabbitBT.rabbit.thirst > 0)
			{
				float amount = Mathf.Min(
					RabbitBT.rabbit.thirst, 
					Time.deltaTime * 1 / RabbitBT.rabbit.thirst
					);
				RabbitBT.rabbit.thirst -= amount;
			}
			else
			{
				RabbitBT.rabbit.thirst = 0;
			}

			timeCounter += Time.deltaTime;
			if(timeCounter > RabbitBT.rabbit.timeToDrink)
			{
				Debug.Log("Done drinking");
				timeCounter = 0f;
				state = NodeState.SUCCESS;
				RabbitBT.rabbit.action = Animal.Action.NONE;
				ClearData("Water");
				return state;
			}
		}

		state = NodeState.RUNNING;
		return state;
	}
}
