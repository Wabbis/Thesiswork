using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[UnityEngine.RequireComponent(typeof(Genes))]
public class Rabbit : Animal
{
	private Species species = Species.RABBIT;
	public Species Get()
	{
		return species;
	}
	public Rabbit mate;
	private MeshRenderer mesh;

	public Color maleColor;
	public Color femaleColor;

	public bool isHunted;
	public Transform Predator;

	public void SetPredator(Transform predator)
	{
		isHunted = true;
		Predator = predator;
	}

	public void ClearPredator()
	{
		isHunted = false;
		Predator = null;
	}

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
		isHunted = false;
		mesh = transform.Find("Mesh").GetComponent<MeshRenderer>();
		Debug.Log("Animal gender: " + genes.gender);
		if (genes.gender == Genes.Gender.MALE)
		{
			mesh.material.color = maleColor;
		}
		else
		{
			mesh.material.color = femaleColor;
		}

	}

	public void InitGenes()
	{
		ChangeSize(genes.attributes[0].value);
		walkSpeed = genes.attributes[1].value;
		runSpeed = genes.attributes[2].value;
		visionRange = genes.attributes[3].value;
		fieldOfViewAngle = genes.attributes[4].value;
		eatingThreshold = genes.attributes[5].value;
		drinkingThreshold = genes.attributes[6].value;
		matingThreshold = genes.attributes[7].value;
		gestationTime = genes.attributes[8].value;
	}

	// Male only
	public bool PotentialMate(Rabbit mate) 
	{
		

		if (rejections.Contains(mate))
		{
			
			return false;
			
		}
		bool accepted = mate.RequestMate(this);
		
		if (accepted)
		{
			
			action = Action.MATING;
			SetMate(mate);
			return true;
		}
		else
		{
			
			AddRejection(mate);
			StartCoroutine(RemoveRejection(timeToForget,mate));
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
				action = Action.MATING;
				this.mate = mate;
				return true;
			}
		}

		return false;
	}

	public void GetPregnant(Rabbit father)
	{
		isPregnant = true;
		StartCoroutine(Gestation(gestationTime, father));

	}

	private IEnumerator Gestation(float time, Rabbit father)
	{
		yield return new WaitForSeconds(time);
		isPregnant = false;
		var tempSpeed = walkSpeed;
		walkSpeed = 0;
		yield return new WaitForSeconds(2);
		GameObject child = Resources.Load("Animals/Rabbit") as GameObject;
		GameObject newChild = Instantiate(child);
		GameManager.Instance.AddRabbit(newChild.GetComponent<Rabbit>());
		newChild.GetComponent<Rabbit>().genes.InheritedGenes(genes, father.genes);
		newChild.transform.position = transform.position;
		newChild.transform.Rotate(new Vector3(child.transform.rotation.x, 
			Random.rotation.eulerAngles.y,
			child.transform.rotation.z));

		walkSpeed = tempSpeed;
	}
}
