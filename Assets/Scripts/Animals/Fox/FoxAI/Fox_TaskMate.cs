using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Fox_TaskMate : Node
{
	private Transform _transform;

	private bool mating = false;
	private float timeCounter = 0f;
	private Fox fox;

	public Fox_TaskMate(Transform transform)
	{
		_transform = transform;
		fox = transform.GetComponent<Fox>();
	}

	public override NodeState Evaluate()
	{
		if (fox.Mate == null)
		{
			state = NodeState.FAILURE;
			return state;
		}

		timeCounter += Time.deltaTime;
		_transform.Rotate(Vector3.up, 10); // Lidl-animation
		if (timeCounter > 3f)
		{
			Debug.Log("Mating ended");
			if(fox.genes.gender == Genes.Gender.FEMALE) { fox.GetPregnant(fox.Mate); }
			fox.action = Animal.Action.NONE;
			timeCounter = 0f;
			fox.reproductiveUrge = 0f;
			state = NodeState.SUCCESS;
			ClearData("Mate");
			fox.MatingEnded();
			return state;
		}
		state = NodeState.RUNNING;
		return state;
	
	}
}