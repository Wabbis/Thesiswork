using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{	
	private static UIController _instance;
	public static UIController Instance
	{
		get
		{
			if (_instance == null)
			{
				Debug.Log("No UI controller available");
			}
			return _instance;
		}
	}

	private void Awake()
	{
		_instance = this;
	}

	private GameObject attributePanel;
	private GameObject statusPanel;
	public Transform[] texts;
	public Transform[] statuses;
	public GameObject selectedUnit;
	private bool isUIEnabled = false;

	private void Start()
	{
		attributePanel = transform.Find("AttributePanel").gameObject;
		statusPanel = transform.Find("StatusPanel").gameObject;

		int children = attributePanel.transform.childCount;
		for (int i = 0; i < children; i++)
		{
			texts[i] = attributePanel.transform.GetChild(i);
		}

		children = statusPanel.transform.childCount;
		for (int i = 0; i < children; i++)
		{
			statuses[i] = statusPanel.transform.GetChild(i);
		}
	}

	private void Update()
	{
		if (isUIEnabled)
		{
			SetStatus();
		}


		if(Input.GetKeyDown(KeyCode.Escape))
		{
			DisableUI();
		}
	}

	public void SelectUnit(GameObject go)
	{
		selectedUnit = go;
		if (isUIEnabled)
		{ 
			SetAttributes();
		}
		else
		{
			EnableUI();
			SetAttributes();
		}
	}

	public void EnableUI()
	{
		isUIEnabled = true;
		attributePanel.SetActive(true);
		statusPanel.SetActive(true);

	}

	public void SetAttributes()
	{
		for(int i = 0; i < texts.Length; i++)
		{
			texts[i].Find("Attribute Value").GetComponent<TMP_Text>().text = selectedUnit.GetComponent<Genes>().attributes[i].value.ToString("F2");
			texts[i].Find("Attribute Name").GetComponent<TMP_Text>().text = selectedUnit.GetComponent<Genes>().attributes[i].name;
		}
	}
	
	public void SetStatus()
{
		var slider = statuses[1].Find("StatusSlider").GetComponent<Slider>();
		var animal = selectedUnit.GetComponent<Animal>();
		// Age
		statuses[0].Find("Value").GetComponent<TMP_Text>().text = Mathf.Round(animal.age).ToString();
		// Energy
		slider.maxValue = animal.maxEnergy;
		slider.value = animal.energy;
		slider.transform.Find("Value").GetComponent<TMP_Text>().text = Mathf.Round(slider.value).ToString();
		// Hunger
		slider = statuses[2].Find("StatusSlider").GetComponent<Slider>();
		slider.value = animal.hunger;
		slider.transform.Find("Value").GetComponent<TMP_Text>().text = Mathf.Round(slider.value).ToString();
		// Thirst
		slider = statuses[3].Find("StatusSlider").GetComponent<Slider>();
		slider.value = animal.thirst;
		slider.transform.Find("Value").GetComponent<TMP_Text>().text = Mathf.Round(slider.value).ToString();
		// Reproduction
		slider = statuses[4].Find("StatusSlider").GetComponent<Slider>();
		slider.value = animal.reproductiveUrge;
		slider.transform.Find("Value").GetComponent<TMP_Text>().text = Mathf.Round(slider.value).ToString();
		// Current Action
		statuses[5].Find("ActionName").GetComponent<TMP_Text>().text = animal.action.ToString();

	}

	public void DisableUI()
	{
		selectedUnit = null;
		isUIEnabled = false;
		attributePanel.SetActive(false);
		statusPanel.SetActive(false);
	}



}
