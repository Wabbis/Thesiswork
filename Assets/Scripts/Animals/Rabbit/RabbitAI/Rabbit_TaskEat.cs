using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class Rabbit_TaskEat : Node
{
    private Transform _transform;
    private float timeCounter = 0f;
    
    public Rabbit_TaskEat(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("Food");

        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

		if (RabbitBT.rabbit.action != Animal.Action.MATING) 
		{
			RabbitBT.rabbit.action = Animal.Action.EATING;

			if (RabbitBT.rabbit.hunger > 0)
			{
				float amount = Mathf.Min(RabbitBT.rabbit.hunger, Time.deltaTime * 1 / RabbitBT.rabbit.timeToEat);
				RabbitBT.rabbit.hunger -= amount;
			}
			else
			{
				RabbitBT.rabbit.hunger = 0;
			}

			timeCounter += Time.deltaTime;
			if (timeCounter > RabbitBT.rabbit.timeToEat)
			{
				Debug.Log("Plant eaten");
				Object.Destroy(target.gameObject);
				timeCounter = 0f;
				state = NodeState.SUCCESS;
				RabbitBT.rabbit.action = Animal.Action.NONE;
				ClearData("Food");
				return state;
			}
		}

        state = NodeState.RUNNING;
        return state;
    }

}