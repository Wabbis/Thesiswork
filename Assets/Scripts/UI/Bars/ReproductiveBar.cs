using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReproductiveBar : MonoBehaviour
{
	public Animal unit;
	public Slider slider;

	void Start()
	{
		if (unit == null)
		{
			unit = GetComponentInParent<Animal>();
		}

		if (slider == null)
		{
			slider = GetComponent<Slider>();
		}
	}

	// Update is called once per frame
	void Update()
	{
		slider.value = Mathf.Lerp(0, 1, unit.reproductiveUrge);
	}
}
