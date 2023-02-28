using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using Pathfinding;

public class Fox_TaskDrink : Node
{
	private Transform _transform;
	private float timeCounter = 0f;
	private Fox fox;

	public Fox_TaskDrink(Transform transform)
	{
		_transform = transform;
		fox = transform.GetComponent<Fox>();
	}

	public override NodeState Evaluate()
	{
		Transform target = (Transform)GetData("Water");

		if(target == null || (fox.action == Animal.Action.EATING && fox.thirst < 90))
		{ 
			state = NodeState.FAILURE;
			return state;
		}
		if(fox.action != Animal.Action.MATING)
		{
			fox.action = Animal.Action.DRINKING;

			if(fox.thirst > 0)
			{
				fox.thirst -= Time.deltaTime * 1 / fox.timeToDrink;
				fox.thirst = Mathf.Clamp01(fox.thirst);
			}
			else
			{
				fox.thirst = 0f;
			}

			timeCounter += Time.deltaTime;
			if(timeCounter > fox.timeToDrink)
			{
				timeCounter = 0f;
				state = NodeState.SUCCESS;
				fox.action = Animal.Action.NONE;
				ClearData("Water");
				return state;
			}
		}
		state = NodeState.RUNNING;
		return state;
	}
}
