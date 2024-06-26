using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class Rabbit_TaskMate : Node
{
	private Transform _transform;

	private bool mating = false;
	private float timeCounter = 0f;
	private Rabbit rabbit;

	public Rabbit_TaskMate(Transform transform)
	{
		_transform = transform;
		rabbit = _transform.GetComponent<Rabbit>();
	}

	public override NodeState Evaluate()
	{
		if (rabbit.Mate == null)
		{
			state = NodeState.FAILURE;
			return state;
		}

		timeCounter += Time.deltaTime;
		_transform.Rotate(Vector3.up, 10);
		if (timeCounter > 3f)
		{
			
			if (rabbit.genes.gender == Genes.Gender.FEMALE) { rabbit.GetPregnant(rabbit.Mate); }
			rabbit.action = Animal.Action.NONE;
			timeCounter = 0f;
			rabbit.reproductiveUrge = 0f;
			state = NodeState.SUCCESS;
			ClearData("Mate");
			rabbit.MatingEnded();
			return state;
		}

		state = NodeState.RUNNING;
		return state;
	}
}