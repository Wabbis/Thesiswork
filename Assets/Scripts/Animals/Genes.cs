using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genes : MonoBehaviour
{
    const float mutationChance = .5f;
    const float maxMutation = 0.2f;
    static System.Random prng = new System.Random();

	public bool isMale;
	public bool GetGender()
	{
		return isMale;
	}


    public float[] values;

    public Genes(float[] values)
    {
        isMale = prng.NextDouble() < 0.5;
        this.values = values;
    }
    
    public static Genes RandomGenes (int value)
    {
        float[] values = new float[value];
        for(int i = 0; i < value; i++)
        {
            values[i] = (float)prng.NextDouble();
        }

        return new Genes(values);
    } 

    public static Genes InheritedGenes(Genes mother, Genes father)
    {
        float[] values = new float[mother.values.Length];
        if (prng.NextDouble() < mutationChance)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (prng.Next(1, 101) < 50)
                {
                    values[i] = mother.values[i] + Random.Range(-maxMutation, maxMutation);

                }
                else
                {
                    values[i] = father.values[i] + Random.Range(-maxMutation, maxMutation);
                }
            }
        }
        else
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (prng.Next(1, 101) < 50)
                {
                    values[i] = mother.values[i];

                }
                else
                {
                    values[i] = father.values[i];
                }
            }
        }

        Genes genes = new Genes(values);
        return genes;
    }


}
