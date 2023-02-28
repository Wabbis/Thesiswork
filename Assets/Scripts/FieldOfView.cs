using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldOfView : MonoBehaviour
{
	private LayerMask layermask;
	private Animal animal;

	private float fov;
	private float viewDistance;

	public bool vision; 

	public void Start()
    {
		if (gameObject.tag == "Rabbit") layermask = LayerMask.GetMask("Fox");
		if (gameObject.tag == "Fox") layermask = LayerMask.GetMask("Rabbit");
		animal = GetComponent<Animal>();
		viewDistance = animal.visionRange;
		fov = animal.fieldOfViewAngle;

		StartCoroutine("VisionRoutine", 1f);
	}


	private IEnumerator VisionRoutine()
	{
		while (true)
		{
			CollisionCheck();
			yield return new WaitForSeconds(1f);
		}
	}

	public void CollisionCheck()
	{
		// Currently can only notice one collider

		Collider[] colliders = Physics.OverlapSphere(transform.position, viewDistance, layermask);
		
		Debug.Log("collider lenght: " + colliders.Length);

		if (colliders.Length != 0)
		{
			Transform target = colliders[0].transform;
			Vector3 direction = (target.position - transform.position).normalized;
			float angle = Vector3.Angle(transform.forward, direction);
			if (angle < fov / 2)
			{
				if (Vector3.Distance(transform.position, target.position) < viewDistance)
				{
					Debug.Log("collider in sight");
					if (gameObject.tag == "Rabbit") GetComponent<Rabbit>().SetPredator(target);
					if (gameObject.tag == "Fox") GetComponent<Fox>().hasPrey = true;

				}
			}
		}
	}
}

