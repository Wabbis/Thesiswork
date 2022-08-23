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
		if(!rabbit.genes.isMale)
		{
			if (rabbit.Mate == null)
			{
				state = NodeState.FAILURE;
				return state;
			}
			else
			{
				state = NodeState.SUCCESS;
				return state;
			}
		}
		
		if (rabbit.Mate == null)
		{
			Debug.Log("Searching for mate");
			Collider[] collider = Physics.OverlapSphere(_transform.position,
				rabbit.visionRange,
				_mateLayerMask);
			Debug.Log(collider.Length);
			if(collider.Length > 0)
			{
				foreach(Collider col in collider)
				{
					if (!col.GetComponent<Genes>().isMale)
					{
						if (rabbit.PotentialMate(col.GetComponent<Rabbit>()))
						{
							Debug.Log("Mate found");
							// parent.parent.SetData("Mate", col.transform);
							state = NodeState.SUCCESS;
							return state;
						}
					}
				}
			}

			state = NodeState.FAILURE;
			return state;
		}

		state = NodeState.SUCCESS;
		return state;
		
	}

}

// TODO:
// Mate info is saved twice