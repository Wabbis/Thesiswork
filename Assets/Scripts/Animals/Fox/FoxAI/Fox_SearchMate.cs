using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Fox_SearchMate : Node
{
	private Transform _transform;
	private LayerMask _mateLayerMask = LayerMask.GetMask("Fox");
	private Fox fox;

	public Fox_SearchMate(Transform transform)
	{
		_transform = transform;
		fox = transform.GetComponent<Fox>();
	}

	public override NodeState Evaluate()
	{
		// Skip search if already has partner
		if(fox.Mate != null)
		{
			state = NodeState.SUCCESS;
			return state;
		}

		// Females dont search for mates, pass on if already got mate, otherwise skip
		if (fox.genes.gender == Genes.Gender.FEMALE)
		{
			if (fox.Mate == null)
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
		if (fox.Mate == null && fox.reproductiveUrge > 25 && fox.genes.gender == Genes.Gender.MALE)
		{
			Debug.Log("Searching for mate");
			// chance own layer to avoid self-search
			int oldLayer = fox.gameObject.layer;
			// layer 2 = Ignore raycast
			fox.gameObject.layer = 2;

			Collider[] collider = Physics.OverlapSphere(_transform.position,
				fox.visionRange,
				_mateLayerMask);
			// chance layer back
			fox.gameObject.layer = oldLayer;
			Debug.Log(collider.Length);
			if(collider.Length > 0)
			{
				foreach (Collider col in collider)
				{
					if (col.GetComponent<Genes>().gender == Genes.Gender.FEMALE)
					{
						if (fox.PotentialMate(col.GetComponent<Fox>()))
						{
							Debug.Log("Mate found");
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