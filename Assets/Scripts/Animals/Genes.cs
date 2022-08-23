using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genes : MonoBehaviour
{ 
    const float mutationChance = .5f;
    const float maxMutation = 0.2f;
    static System.Random rand = new System.Random();

	public bool isMale;
	public bool GetGender()
	{
		return isMale;
	}

    public float[] values;

    public Genes(float[] values)
    {
		var rnd = rand.NextDouble();
		Debug.Log("rnd: "+rnd);
		isMale = rnd < 0.5f;
        this.values = values;
    }
    
    public Genes RandomGenes (int value)
    {
        float[] values = new float[value];
        for(int i = 0; i < value; i++)
        {
            values[i] = (float)Random.Range(0.9f,1.1f);
        }
        Genes genes = new Genes(values);
		return genes;
    } 

    public Genes InheritedGenes(Genes mother, Genes father)
    {
		float[] newValues = new float[mother.values.Length];

		// Brake two genes to components and connect them 50-50
		// select half of the mother genes and fill in rest from the father

		int fromMother = 0;
		int current = 0;
		for (int i = 0; i < newValues.Length; i++)
		{
			float chance = (((float)values.Length / 2) - fromMother) / ((float)values.Length - i);
			if (rand.NextDouble() <= chance && chance != 0)
			{
				newValues[current] = mother.values[current];
				if(rand.Next(1,101) < 50)
				{
					newValues[current] += Random.Range(-maxMutation, maxMutation);
				}
				fromMother++;
			}
			else
			{
				newValues[current] = father.values[current];
				if (rand.Next(1, 101) < 50)
				{
					newValues[current] += Random.Range(-maxMutation, maxMutation);
				}
			}
			current++;
		}
		Debug.LogError("New values created");
		values = newValues;
        Genes genes = new Genes(values);
        return genes;
    }


}
