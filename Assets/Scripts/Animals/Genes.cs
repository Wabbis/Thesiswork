using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genes : MonoBehaviour
{ 
    const float mutationChance = 50f;
    const float maxMutation = 0.2f;
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

    public float[] values;
	public float[] femaleValues;
	public float[] maleValues;

	public Genes(float[] gValues, float[] fValues, float[] mValues, Gender sex) 
    {
		gender = sex;
        values = gValues;
		femaleValues = fValues;
		maleValues = mValues;
	}

	public void AssignGenes(float[] gValues, float[] fValues, float[] mValues, Gender sex)
	{
		gender = sex;
		values = gValues;
		femaleValues = fValues;
		maleValues = mValues;
	}

	public void InheritedGenes(Genes mother, Genes father)
	{
		Gender gender;
		if(rand.Next(1,101) > 50)
		{
			gender = Gender.MALE;
		}
		else
		{
			gender = Gender.FEMALE;
		}

		float[] gValues = GeneralGenes(mother, father);
		float[] fValues = FemaleGenes(mother, father);
		float[] mValues = MaleGenes(mother, father);

		AssignGenes(gValues, fValues, mValues, gender);
	}

	public float[] GeneralGenes(Genes mother, Genes father)
	{

		float[] values = new float[mother.values.Length];
		// Brake two genes to components and connect them 50-50
		// select half of the mother genes and fill in rest from the father
		int fromMother = 0;
		int current = 0;
		for (int i = 0; i < values.Length; i++)
		{
			float chance = (((float)values.Length / 2) - fromMother) / ((float)values.Length - i);
			if (rand.NextDouble() <= chance && chance != 0)
			{
				values[current] = mother.values[current];
				var roll = rand.Next(1, 101);

				if (roll < mutationChance)
				{
					values[current] += Random.Range(-maxMutation, maxMutation);

				}
				fromMother++;
			}
			else
			{
				values[current] = father.values[current];
				var roll = rand.Next(1, 101);

				if (roll < mutationChance)
				{
					values[current] += Random.Range(-maxMutation, maxMutation);

				}
			}
			current++;
		}
		return values;
	}
	public float[] MaleGenes(Genes mother, Genes father)
	{

		float[] values = new float[mother.maleValues.Length];
		int fromMother = 0;
		int current = 0;
		for (int i = 0; i < values.Length; i++)
		{
			float chance = (((float)maleValues.Length / 2) - fromMother) / ((float)maleValues.Length - i);
			if (rand.NextDouble() <= chance && chance != 0)
			{
				values[current] = mother.maleValues[current];
				if (rand.Next(1, 101) < mutationChance)
				{
					values[current] += Random.Range(-maxMutation, maxMutation);
				}
				fromMother++;
			}
			else
			{
				values[current] = father.maleValues[current];
				if (rand.Next(1, 101) < mutationChance)
				{
					values[current] = Random.Range(-maxMutation, maxMutation);
				}
			}
			current++;
		}
		return values;
	}
	public float[] FemaleGenes(Genes mother, Genes father)
	{

		float[] values = new float[mother.femaleValues.Length];
		int fromMother = 0;
		int current = 0;
		for (int i = 0; i < values.Length; i++)
		{
			float chance = (((float)femaleValues.Length / 2) - fromMother) / ((float)femaleValues.Length - i);
			if (rand.NextDouble() <= chance && chance != 0)
			{
				values[current] = mother.femaleValues[current];
				if (rand.Next(1, 101) < mutationChance)
				{
					values[current] += Random.Range(-maxMutation, maxMutation);

				}
				fromMother++;
			}
			else
			{
				values[current] = father.femaleValues[current];
				if (rand.Next(1, 101) < mutationChance)
				{
					values[current] = Random.Range(-maxMutation, maxMutation);

				}
			}
			current++;
		}
		return values;
	}
}
