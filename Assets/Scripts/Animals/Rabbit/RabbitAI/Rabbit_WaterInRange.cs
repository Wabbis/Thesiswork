using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Rabbit_WaterInRange : Node
{
	private Transform _transform;

	public Rabbit_WaterInRange(Transform transform)
	{
		_transform = transform;
	}

	
}
