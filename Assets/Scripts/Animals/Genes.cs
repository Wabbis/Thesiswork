using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;
using System.Linq;


public class Genes : MonoBehaviour
{
	[Serializable]
	public struct Attribute
	{
		public string name;
		public float value;
	}

	const float mutationChance = 50f;
	const float maxMutation = 5f;
	static readonly System.Random rand = new System.Random();
	
	/// <summary>
	///  0 = MALE, 1 = FEMALE
	/// </summary>
	public enum Gender
	{
		MALE,
		FEMALE
	}

	public Gender gender;

	[NonReorderable]
	public Attribute[] attributes;

	private void InitAttributeArray()
	{
		attributes = new Attribute[] {
			new Attribute() { name = "Size", value = 1 },
			new Attribute() { name = "Walk Speed", value = 1 },
			new Attribute() { name = "Run Speed", value = 1 },
			new Attribute() { name = "Vision Radius", value = 1 },
			new Attribute() { name = "Field of View Angle", value = 1 },
			new Attribute() { name = "Eating Threshold", value = 1 },
			new Attribute() { name = "Drinking Threshold", value = 1},
			new Attribute() { name = "Mating Threshold", value = 1 },
			new Attribute() { name = "Gestation Time", value = 1 }
			};
	}

	public void RandomInit()
	{
		if (rand.Next(1, 101) > 50)
		{
			this.gender = Gender.MALE;
		}
		else
		{
			this.gender = Gender.FEMALE;
		}

		InitAttributeArray();
	}

	public void InheritedGenes(Genes mother, Genes father)
	{
		if(rand.Next(1,101) > 50)
		{
			this.gender = Gender.MALE;
		}
		else
		{
			this.gender = Gender.FEMALE;
		}
		InitAttributeArray();
		RandomizeGenes(mother, father);
	}

	public void RandomizeGenes(Genes mother, Genes father)
	{
		int fromMother = 0;

		for (int i = 0; i < mother.attributes.Length; i++)
		{
			float chance = (((float)mother.attributes.Length / 2) - fromMother) / ((float)mother.attributes.Length - i);
			if (rand.NextDouble() <= chance && chance != 0)
			{
				attributes[i].value = mother.attributes[i].value;;

				if (rand.Next(1, 101) < mutationChance)
				{
					attributes[i].value += UnityEngine.Random.Range(-maxMutation, maxMutation);
				}
				fromMother++;
			}
			else
			{
				attributes[i].value = father.attributes[i].value;
				if (rand.Next(1, 101) < mutationChance)
				{
					attributes[i].value += UnityEngine.Random.Range(-maxMutation, maxMutation);
				}
			}
		}

	}
//	public float[] GeneralGenes(Genes mother, Genes father)
//	{
		
//		float[] values = new float[mother.values.Length];
//		// Brake two genes to components and connect them 50-50
//		// select half of the mother genes and fill in rest from the father
//		int fromMother = 0;
//		int current = 0;
//		for (int i = 0; i < values.Length; i++)
//		{
//			float chance = (((float)values.Length / 2) - fromMother) / ((float)values.Length - i);
//			if (rand.NextDouble() <= chance && chance != 0)
//			{
//				values[current] = mother.values[current];
//				var roll = rand.Next(1, 101);

//				if (roll < mutationChance)
//				{
//					values[current] += UnityEngine.Random.Range(-maxMutation, maxMutation);

//				}
//				fromMother++;
//			}
//			else
//			{
//				values[current] = father.values[current];
//				var roll = rand.Next(1, 101);

//				if (roll < mutationChance)
//				{
//					values[current] += UnityEngine.Random.Range(-maxMutation, maxMutation);

//				}
//			}
//			current++;
//		}
//		return values;
//	}
//	public float[] MaleGenes(Genes mother, Genes father)
//	{

//		float[] values = new float[mother.maleValues.Length];
//		int fromMother = 0;
//		int current = 0;
//		for (int i = 0; i < values.Length; i++)
//		{
//			float chance = (((float)maleValues.Length / 2) - fromMother) / ((float)maleValues.Length - i);
//			if (rand.NextDouble() <= chance && chance != 0)
//			{
//				values[current] = mother.maleValues[current];
//				if (rand.Next(1, 101) < mutationChance)
//				{
//					values[current] += UnityEngine.Random.Range(-maxMutation, maxMutation);
//				}
//				fromMother++;
//			}
//			else
//			{
//				values[current] = father.maleValues[current];
//				if (rand.Next(1, 101) < mutationChance)
//				{
//					values[current] = UnityEngine.Random.Range(-maxMutation, maxMutation);
//				}
//			}
//			current++;
//		}
//		return values;
//	}
//	public float[] FemaleGenes(Genes mother, Genes father)
//	{

//		float[] values = new float[mother.femaleValues.Length];
//		int fromMother = 0;
//		int current = 0;
//		for (int i = 0; i < values.Length; i++)
//		{
//			float chance = (((float)femaleValues.Length / 2) - fromMother) / ((float)femaleValues.Length - i);
//			if (rand.NextDouble() <= chance && chance != 0)
//			{
//				values[current] = mother.femaleValues[current];
//				if (rand.Next(1, 101) < mutationChance)
//				{
//					values[current] += UnityEngine.Random.Range(-maxMutation, maxMutation);

//				}
//				fromMother++;
//			}
//			else
//			{
//				values[current] = father.femaleValues[current];
//				if (rand.Next(1, 101) < mutationChance)
//				{
//					values[current] = UnityEngine.Random.Range(-maxMutation, maxMutation);

//				}
//			}
//			current++;
//		}
//		return values;
//	}
}
