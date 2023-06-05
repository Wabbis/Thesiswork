using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class Rabbit_TaskEat : Node
{
    private Transform _transform;
    private float timeCounter = 0f;
	private Rabbit rabbit;
    
    public Rabbit_TaskEat(Transform transform)
    {
        _transform = transform;
		rabbit = _transform.GetComponent<Rabbit>();
	}

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("Food");

        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

		if (rabbit.action != Animal.Action.MATING) 
		{
			rabbit.action = Animal.Action.EATING;

			if (rabbit.hunger > 0)
			{
				rabbit.hunger -= 100 * Time.deltaTime / rabbit.timeToEat;
				rabbit.energy += 100* Time.deltaTime / rabbit.timeToEat; // test value
			}
			else
			{
				rabbit.hunger = 0;
			}

			timeCounter += Time.deltaTime;
			if (timeCounter > rabbit.timeToEat)
			{
				
				Object.Destroy(target.gameObject);
				timeCounter = 0f;
				state = NodeState.SUCCESS;
				rabbit.action = Animal.Action.NONE;
				ClearData("Food");
				return state;
			}
		}

        state = NodeState.RUNNING;
        return state;
    }

}