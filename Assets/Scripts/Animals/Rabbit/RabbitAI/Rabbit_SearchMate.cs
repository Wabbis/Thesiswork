using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Rabbit_SearchMate : Node
{
	private Transform _transform;
	private LayerMask _mateLayerMask = LayerMask.GetMask("Rabbit");
	

	public Rabbit_SearchMate(Transform transform)
	{
		_transform = transform;
	}

	public override NodeState Evaluate()
	{

		if (RabbitBT.rabbit.Mate == null)
		{
			Collider[] collider = Physics.OverlapSphere(_transform.position,
				RabbitBT.rabbit.visionRange,
				_mateLayerMask);

			if(collider.Length > 0)
			{
				foreach(Collider col in collider)
				{
					if (col.GetComponent<Genes>().GetGender() 
						!= _transform.GetComponent<Genes>().GetGender())
					{
						if (RabbitBT.rabbit.PotentialMate(col.GetComponent<Rabbit>()))
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