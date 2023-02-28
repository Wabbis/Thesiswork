using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[UnityEngine.RequireComponent(typeof(Genes))]
public class Fox : Animal
{
	[Header("Fox")]
	public Fox mate;

	public Color maleColor;
	public Color femaleColor;

	public float huntSpeed;
	public float normalSpeed;

	public bool hasPrey;

	public Genes genes;

	public void Die(CauseOfDeath cod)
	{
		if(!dead)
		{
			dead = true;
			GameManager.Instance.RemoveFox(this, cod);
			ai.enabled = false;
		}
	}
	

	public Fox Mate
	{
		get => mate;
	}

	public void SetMate(Fox _mate)
	{
		mate = _mate;
	}

	private float timeToForget = 5;
	public List<Fox> rejections;

	public void Start()
	{
		hasPrey = false;
		// TODO: Apply gender colors	
	}

	public void InitGenes()
	{
		size *= genes.values[0];
		speed *= genes.values[0];
	}

	// Male only
	public bool PotentialMate(Fox mate)
	{
		if (rejections.Contains(mate))
		{
			return false;

		}

		bool accepted = mate.RequestMate(this);
		if(accepted)
		{
			action = Action.MATING;
			SetMate(mate);
			return true;
		}
		else
		{
			AddRejection(mate);
			StartCoroutine(RemoveRejection(timeToForget, mate));
			return false;
		}
	}

	private void AddRejection(Fox mate)
	{
		rejections.Add(mate);
	}

	private IEnumerator RemoveRejection(float time, Fox mate)
	{ 
		yield return new WaitForSeconds(time);
		rejections.Remove(mate);
	}

	public void MatingEnded()
	{
		SetMate(null);
		reproductiveUrge = 0;
	}

	// Female Only

	public bool RequestMate(Fox mate)
	{
		if(isPregnant || reproductiveUrge < 25 || action != Action.NONE)
		{
			return false;
		}
		if(reproductiveUrge > thirst && reproductiveUrge > hunger)
		{
			if (this.mate == null)
			{
				action = Action.MATING;
				this.mate = mate;
				return true;
			}
		}
		return false;
	}

	public void GetPregnant(Fox father)
	{
		isPregnant = true;
		StartCoroutine(Gestation(pregnancyDuration, father));
	}

	private IEnumerator Gestation(float time, Fox father)
	{
		yield return new WaitForSeconds(time);
		isPregnant = false;
		var tempSpeed = speed;
		speed = 0;
		yield return new WaitForSeconds(2);
		GameObject child = Resources.Load("Animals/Fox") as GameObject;
		GameObject newChild = Instantiate(child);
		GameManager.Instance.AddFox(newChild.GetComponent<Fox>());
		newChild.GetComponent<Fox>().genes.InheritedGenes(genes, father.genes);
		newChild.transform.position = transform.position;
		newChild.transform.Rotate(new Vector3(child.transform.rotation.x,
			Random.rotation.eulerAngles.y,
			child.transform.rotation.z));

		speed = tempSpeed;
	}
}