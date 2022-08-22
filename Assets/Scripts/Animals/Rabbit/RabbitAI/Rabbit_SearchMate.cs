using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Rabbit_SearchMate : Node
{
	private Transform _transform;
	private LayerMask _mateLayerMask = LayerMask.GetMask("Rabbit");
	private Rabbit rabbit;

	public Rabbit_SearchMate(Transform transform)
	{
		_transform = transform;
		rabbit = _transform.GetComponent<Rabbit>();
	}

	public override NodeState Evaluate()
	{

		if (rabbit.Mate == null)
		{
			Collider[] collider = Physics.OverlapSphere(_transform.position,
				rabbit.visionRange,
				_mateLayerMask);

			if(collider.Length > 0)
			{
				foreach(Collider col in collider)
				{
					if (col.GetComponent<Genes>().GetGender() 
						!= _transform.GetComponent<Genes>().GetGender())
					{
						if (rabbit.PotentialMate(col.GetComponent<Rabbit>()))
						{ 
							parent.parent.SetData("Mate", col.transform);
							state = NodeState.SUCCESS;
							return state;
						}
					}
				}
			}

			state = NodeState.FAILURE;
			return state;
		}

		state = NodeState.RUNNING;
		return state;
		
	}

}

// TODO:
// Mate info is saved twice