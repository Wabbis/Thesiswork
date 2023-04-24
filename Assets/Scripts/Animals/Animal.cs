using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Pathfinding;

public class Animal : MonoBehaviour
{
	public enum Action
	{
		NONE,
		EATING,
		DRINKING,
		MATING,
		MOVING,
		RUNNING
	}

	public CauseOfDeath causeOfDeath;
	public bool DEBUG;
	public GameObject prefab;
	protected bool dead = false;
	public Action action = Action.NONE;
	public AIPath ai;
	
	// Eat and drink settings:
	[Header("Eat and Drink")]
	public float drinkRange;
    public float eatRange;
    public float timeToEat; 
    public float timeToDrink;

	// Reproduction Settings	
	[Header("Reproduction")]
	public float timeToMate;
	public float reproductiveUrge;  
	// Female only
	public bool isPregnant = false;
	public float gestationTime = 10f;
	// Male only
	// NYI : public float Desirability = 100f; // Values from 0.1 to 100 male only, 0 for female

	// General settings:
	[Header("General")]
	public float age = 0;
	public float maxEnergy; // affected by size?
	public float energy; 
	public float mass; // is affected by genes
    public float walkSpeed; // is affected by genes
	public float runSpeed; // is affected by genes
	public float size; // is affected by genes
	public void ChangeSize(float newSize)
	{
		size = newSize;
		transform.localScale = new Vector3(size, size, size);
	}
	public float searchRadius; // is affected by genes
	public float visionRange; // is affected by genes
	public float fieldOfViewAngle; // is affected by genes

	[Header("Thresholds")]
	public float drinkingThreshold; // is affected by genes
	public float eatingThreshold; // is affected by genes
	public float matingThreshold; // is affected by genes



	[Header("Death settings")]
	public AnimationCurve oddToDieByAge;
	public float timeToDieByStarving;
	public float timeToDieByThirst; 
	[Header("State")]
    public float hunger = 0;
    public float thirst = 0;

	public void Awake()
	{
		ai = GetComponent<AIPath>();
		
	}

	public void Update()  
    {
		walkSpeed = ai.velocity.magnitude;
		
		reproductiveUrge += Time.deltaTime;
		if(reproductiveUrge > 100)
		{
			reproductiveUrge = 100;
		}
		// some fucking algorith
		
		// float energyConsuption = Mathf.Pow(size, 0.75f) + (0.5f * speed);
		energy -= Mathf.Pow(size, 0.75f) * Time.deltaTime;

		//energy -= mass;
		age += Time.deltaTime;
		hunger += Time.deltaTime * 1f;
		thirst += Time.deltaTime * 1f;
		
		
		if(energy > maxEnergy) energy = maxEnergy;

		if (energy <= 0) Die(CauseOfDeath.EXHAUSTION);
		if (hunger >= 100) Die(CauseOfDeath.STARVING);
		if (thirst >= 100) Die(CauseOfDeath.THIRST);
		if (age > 120f) Die(CauseOfDeath.AGE);
	}

	protected void Die(CauseOfDeath cod)
	{
		if (!dead)
		{
			causeOfDeath = cod;
			dead = true;
			GameManager.Instance.Remove(this);
			ai.enabled = false;
		}
   }

	private void OnDrawGizmosSelected()
	{
		if (DEBUG)
		{
			// Show search radius
			Handles.DrawWireDisc(transform.position, Vector3.up, searchRadius);

			// Show FoV
			Vector3 fromVector = Quaternion.AngleAxis(-0.5f * fieldOfViewAngle, Vector3.up) * transform.forward;
			Handles.DrawSolidArc(transform.position, Vector3.up, fromVector, fieldOfViewAngle, visionRange);
		}
	}
}



