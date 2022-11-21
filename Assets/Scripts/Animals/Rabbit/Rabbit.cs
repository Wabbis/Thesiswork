using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rabbit : Animal
{
	public Rabbit mate;

	public Color maleColor;
	public Color femaleColor;

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
	public List<Rabbit> rejections;

	public void Start()
	{
	
		Debug.Log("Animal gender: " + genes.gender);
		if (genes.gender == Genes.Gender.MALE)
		{
			transform.Find("Mesh").GetComponent<MeshRenderer>().material.color = maleColor;
		}
		else
		{
			transform.Find("Mesh").GetComponent<MeshRenderer>().material.color = femaleColor;
		}

	}

	public void InitGenes()
	{
		size *= genes.values[0];
		speed *= genes.values[1];



	}

	// Male only
	public bool PotentialMate(Rabbit mate) 
	{
		Debug.Log("Potential mate is: " + mate);

		if (rejections.Contains(mate))
		{
			Debug.Log("Already rejected");
			return false;
			
		}
		bool accepted = mate.RequestMate(this);
		Debug.Log("mate requested...");
		if (accepted)
		{
			Debug.Log("Accepted");
			SetMate(mate);
			return true;
		}
		else
		{
			Debug.Log("rejected");
			AddRejection(mate);
			StartCoroutine(RemoveRejection(timeToForget,mate));
			return false;
		}
	}

	private void AddRejection(Rabbit mate)
	{
		Debug.Log("Rejection added");
		rejections.Add(mate);
		
	}

	private IEnumerator RemoveRejection(float time, Rabbit mate)
	{
		Debug.Log("Rejected removed in " + time);
		yield return new WaitForSeconds(time);
		rejections.Remove(mate);
	}

	public void MatingEnded()
	{
		SetMate(null);
		reproductiveUrge = 0;
	}

	// FEMALE ONLY 

	public bool RequestMate(Rabbit mate)
	{
		if(isPregnant || reproductiveUrge < 25 || action != Action.NONE)
		{
			return false;
		}
		if (reproductiveUrge > thirst && reproductiveUrge > hunger)
		{
			if (this.mate == null)
			{
				this.mate = mate;
				return true;
			}
		}

		return false;
	}

	public void GetPregnant(Rabbit father)
	{
		isPregnant = true;
		StartCoroutine(Gestation(pregnancyDuration, father));

	}

	private IEnumerator Gestation(float time, Rabbit father)
	{
		yield return new WaitForSeconds(time);
		isPregnant = false;
		var tempSpeed = speed;
		speed = 0;
		yield return new WaitForSeconds(2);
		GameObject child = Resources.Load("Animals/Rabbit") as GameObject;
		GameObject newChild = Instantiate(child);
		newChild.GetComponent<Rabbit>().genes.InheritedGenes(genes, father.genes);
		newChild.transform.position = transform.position;
		newChild.transform.Rotate(new Vector3(child.transform.rotation.x, 
			Random.rotation.eulerAngles.y,
			child.transform.rotation.x));

		speed = tempSpeed;
	}
}
