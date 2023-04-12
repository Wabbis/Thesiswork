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
				rabbit.thirst -= Time.deltaTime * 1 / rabbit.timeToDrink;
				
			}
			else
			{
				rabbit.thirst = 0;
			}

			timeCounter += Time.deltaTime;
			if(timeCounter > rabbit.timeToDrink)
			{
				Debug.Log("Done drinking");
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
}
