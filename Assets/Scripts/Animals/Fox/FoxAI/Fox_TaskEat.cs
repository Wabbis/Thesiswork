using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using Pathfinding;
public class Fox_TaskEat : Node
{
	private Transform _transform;
	private float timeCounter = 0f;
	private Fox fox;
	private AIPath ai;

	public Fox_TaskEat(Transform transform)
	{
		_transform = transform;
		fox = transform.GetComponent<Fox>();
		ai = transform.GetComponent<AIPath>();
	}

	public override NodeState Evaluate()
	{
		Transform target = (Transform)GetData("Food");

		if (target == null)
		{
			state = NodeState.FAILURE;
			return state;
		}

		if (fox.action != Animal.Action.MATING)
		{
			fox.action = Animal.Action.EATING;
			
			if (fox.hunger > 0)
			{
				float amount = Mathf.Min(fox.hunger,
					Time.deltaTime * 1 / fox.timeToEat
					);
				fox.hunger -= amount;
				fox.energy += fox.timeToEat / 50; // test value
			}
			else
			{
				fox.hunger = 0;
			}

			timeCounter += Time.deltaTime;
			if (timeCounter > fox.timeToEat)
			{
				Debug.Log("Eating ended");
				
				Object.Destroy(target.gameObject); // Prey is now destroyed by the fox. Change to GameManager later?
				timeCounter = 0f;
				state = NodeState.SUCCESS;
				fox.action = Animal.Action.NONE;
				ClearData("Food");
				ai.maxSpeed = fox.normalSpeed;
				
				return state;

			}
		}

		state = NodeState.RUNNING;
		return state;
	}
}