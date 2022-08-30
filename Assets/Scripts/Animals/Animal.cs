using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Animal : MonoBehaviour
{
	public enum Action
	{
		NONE,
		EATING,
		DRINKING,
		MATING,
		MOVING
	}

	public GameObject prefab;
	protected bool dead = false;
	public Action action = Action.NONE;

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
	public float pregnancyDuration = 10f;

	// Male only
	public float Desirability = 100f; // Values from 0.1 to 100 male only, 0 for female

	// General settings:
	[Header("General")]
	public float energy;
    public float speed;
    public float size;
    public float searchRadius;
    public float visionRange;
    public float fieldOfViewAngle;

	// Death settings:
    public float timeToDieByStarving;
    public float timeToDieByThirst;

    [Header("State")]
    public float hunger = 0;
    public float thirst = 0;

	public void Update()
    {

		reproductiveUrge += Time.deltaTime;
		// some fucking algorith
		energy -= 0.0001f * (speed + size) / Time.deltaTime;
	

		hunger += Time.deltaTime * 100 / timeToDieByStarving;
		thirst += Time.deltaTime * 100 / timeToDieByThirst;

        if (hunger >= 100) Die();
        if (thirst >= 100) Die();
    }

    protected void Die()
    {
        if (!dead)
        {
            dead = true;
            Destroy(gameObject);
        }
    }

	private void OnDrawGizmosSelected()
	{
		
		// Show search radius
		Handles.DrawWireDisc(transform.position, Vector3.up, searchRadius);

		// Show FoV
		Vector3 fromVector = Quaternion.AngleAxis(-0.5f * fieldOfViewAngle, Vector3.up) * transform.forward;
		Handles.DrawSolidArc(transform.position, Vector3.up, fromVector, fieldOfViewAngle,  visionRange);

		
	}

	


}



