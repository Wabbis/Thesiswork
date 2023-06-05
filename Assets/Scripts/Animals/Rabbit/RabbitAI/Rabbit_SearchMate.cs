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
		// Skip search if rabbit already has a partner
		if(rabbit.Mate != null)
		{
			state = NodeState.SUCCESS;
			return state;
		}
		// Female rabbits dont search for mates, hence pass on if female has a mate already, otherwise skip this step
		if (rabbit.genes.gender == Genes.Gender.FEMALE)
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
		
		if (rabbit.Mate == null && rabbit.reproductiveUrge > rabbit.matingThreshold && rabbit.genes.gender == Genes.Gender.MALE)
		{
			
			int oldLayer = rabbit.gameObject.layer;
			rabbit.gameObject.layer = 2;
			Collider[] collider = Physics.OverlapSphere(_transform.position,
				rabbit.visionRange,
				_mateLayerMask);
			rabbit.gameObject.layer = oldLayer;
			
			if(collider.Length > 0)
			{
				foreach(Collider col in collider)
				{
					if (col.GetComponent<Genes>().gender == Genes.Gender.FEMALE )
					{
						if (rabbit.PotentialMate(col.GetComponent<Rabbit>()))
						{
							
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

		state = NodeState.FAILURE;
		return state;
		
	}

}

// TODO:
// Mate info is saved twice