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
		MATING
	}

	public GameObject prefab; 

	protected bool dead = false;
	public Action action = Action.NONE;
    
    public Color maleColor;
    public Color femaleColor;
	


	// Sexual traits:

		// Male only
	public float Desirability = 100f; // Values from 0.1 to 100 male only, 0 for female

		// Female only
	public bool isPregnant = false;
	public float pregnancyDuration = 10f;

	// Eat and drink settings:
    public float eatRange;
    public float timeToEat; 
    public float timeToDrink;
	public float timeToReproduce;

    // General settings:
    public float speed;
    public float size;
    public float searchRadius;
    public float visionRange;
    public float fieldOfViewAngle;

	// Death settings:
    public float timeToDieByStarving;
    public float timeToDieByThirst;

    [Header("State")]
    public float hunger;
    public float thirst;

    public void Update()
    {

		hunger += Time.deltaTime / timeToDieByStarving;

		thirst += Time.deltaTime / timeToDieByThirst;

        if (hunger >= 1) Die();
        else if (thirst >= 1) Die();
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



