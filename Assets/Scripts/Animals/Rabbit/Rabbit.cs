using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rabbit : Animal
{
	private Rabbit mate;

	public Genes genes;

	public Rabbit Mate
	{
		get => mate;
	}

	public void SetMate(Rabbit _mate)
	{
		mate = _mate;
	}

	private float timeToForget = 5;
	private List<Rabbit> rejections;


	// Male only
	public bool PotentialMate(Rabbit mate) 
	{
		bool accepted = mate.RequestMate(this);
		if (accepted)
		{

			SetMate(mate);
			return true;
		}
		else
		{
			AddRejection(mate);
			RemoveRejection(timeToForget, mate);
			return false;
		}
	}

	private void AddRejection(Rabbit mate)
	{
		rejections.Add(mate);
		
	}

	private IEnumerator RemoveRejection(float time, Rabbit mate)
	{
		yield return new WaitForSeconds(time);
		rejections.Remove(mate);
	}

	public void MatingEnded()
	{
		SetMate(null);
	}

	// FEMALE ONLY 

	public bool RequestMate(Rabbit mate)
	{
		if (mate.Desirability < Random.Range(0, 100))
		{
			return false;
		}
		if (this.mate == null)
			this.mate = mate;

		return true;
	}

	public void GetPregnant(Rabbit father)
	{
		isPregnant = true; 

	}

	private IEnumerator Gestation(float time)
	{
		
		yield return new WaitForSeconds(time);
		isPregnant = false;
		var tempSpeed = speed;
		speed = 0;
		GameObject child = Instantiate(prefab);
		child.transform.position = Utility.FindRandomPointOnNavMesh(transform, 5.0f);
		child.transform.Rotate(new Vector3(child.transform.rotation.x, 
			Random.rotation.eulerAngles.y,
			child.transform.rotation.x));

		speed = tempSpeed;
	}
}
