using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Rabbit_TaskDrink : Node
{
	private Transform _transform;
	private float timeCounter = 0f;
	private Rabbit rabbit;
	
	public Rabbit_TaskDrink(Transform transform)
	{
		_transform = transform;
		rabbit = transform.GetComponent<Rabbit>();
	}

	public override NodeState Evaluate()
	{
		Transform target = (Transform)GetData("Water");

		if (target == null)
		{
			state = NodeState.FAILURE;
			return state;
		}

		if (rabbit.action != Animal.Action.MATING)
		{
			rabbit.action = Animal.Action.DRINKING;

			if(rabbit.thirst > 0)
			{
				rabbit.thirst -= 100 * Time.deltaTime / rabbit.timeToDrink;
				
			}
			else
			{
				rabbit.thirst = 0;
				state = Stop();
				return state;

			}

			timeCounter += Time.deltaTime;
			if(timeCounter > rabbit.timeToDrink)
			{
				
				timeCounter = 0f;
				state = NodeState.SUCCESS;
				rabbit.action = Animal.Action.NONE;
				ClearData("Water");
				return state;
			}
		}
		state = NodeState.RUNNING;
		return state;
	}

	private NodeState Stop()
	{
		Debug.Log("Done drinking");
		timeCounter = 0f;
		state = NodeState.SUCCESS;
		rabbit.action = Animal.Action.NONE;
		ClearData("Water");
		return state;
	}
}
