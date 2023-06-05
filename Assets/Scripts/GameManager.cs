using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
			{
				Debug.Log("No game manager available");
			}
			return _instance;
		}
	}

	private void Awake()
	{
		_instance = this;
	}

	public bool createInitialPopulation = false;
	public float initialPopulationAmount = 0f;


	[SerializeField]
	private List<Rabbit> rabbits = new List<Rabbit>();
	[SerializeField]
	private List<Fox> foxes = new List<Fox>();

	private Dictionary<CauseOfDeath, int> listOfRabbitDeaths = new Dictionary<CauseOfDeath, int>()
	{
		{CauseOfDeath.UNKNOWN, 0 },
		{CauseOfDeath.EATEN, 0 },
		{CauseOfDeath.STARVING, 0 },
		{CauseOfDeath.THIRST, 0 },
		{CauseOfDeath.AGE, 0 },
		{CauseOfDeath.EXHAUSTION, 0}
	};

	private Dictionary<CauseOfDeath, int> listOfFoxDeaths = new Dictionary<CauseOfDeath, int>()
	{
		{CauseOfDeath.UNKNOWN, 0 },
		{CauseOfDeath.EATEN, 0 },
		{CauseOfDeath.STARVING, 0 },
		{CauseOfDeath.THIRST, 0 },
		{CauseOfDeath.AGE, 0 },
		{CauseOfDeath.EXHAUSTION, 0}
	};


	// Start is called before the first frame update
	void Start()
    {
		if (listOfRabbitDeaths == null) listOfRabbitDeaths = new Dictionary<CauseOfDeath, int>();
		if (listOfFoxDeaths == null) listOfFoxDeaths = new Dictionary<CauseOfDeath, int>();

		if (createInitialPopulation)
		{
			GameObject animal = Resources.Load("Animals/Rabbit") as GameObject;
			for (int i = 0; i < initialPopulationAmount; i++)
			{
				GameObject go = Instantiate(animal, Vector3.zero, Quaternion.identity);
				do {
					go.transform.position = new Vector3(Random.Range(0, 370), 0, Random.Range(0, 370)); 
				} while(!Physics.Raycast(go.transform.position, Vector3.down, 2, LayerMask.GetMask("Grass")));

				go.GetComponent<Genes>().RandomInit();
			}
		}

		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Fox")) {
			AddFox(obj.GetComponent<Fox>());
			obj.GetComponent<Fox>().InitGenes();
		}

		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Rabbit"))
		{
			AddRabbit(obj.GetComponent<Rabbit>());
			obj.GetComponent<Rabbit>().InitGenes();
		}

	}

	private void OnDestroy()
	{
		foreach(KeyValuePair<CauseOfDeath, int> kvp in listOfRabbitDeaths)
		{
			Debug.LogFormat("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
		}
	}
	/* Add animals */
	public void AddRabbit(Rabbit rabbit)
	{
		if (!rabbits.Contains(rabbit))
		{
			rabbits.Add(rabbit);
			Debug.Log(rabbit + " added to the list");
		}
	}
	public void AddFox(Fox fox)
	{
		if (!foxes.Contains(fox))
		{
			foxes.Add(fox);
			Debug.Log(fox + " added to the list");
		}
	}

	/* Remove animals */

	public void Remove(Animal animal) 
	{
		if (UIController.Instance.selectedUnit == animal.gameObject) UIController.Instance.DisableUI();

		if(animal.GetComponent<Fox>())
		{
			RemoveFox(animal.GetComponent<Fox>(), animal.causeOfDeath);
		}

		if (animal.GetComponent<Rabbit>())
		{
			RemoveRabbit(animal.GetComponent<Rabbit>(), animal.causeOfDeath);
		}
	}


	public void RemoveRabbit(Rabbit rabbit, CauseOfDeath cod)
	{
		if (rabbits.Contains(rabbit))
		{
			rabbits.Remove(rabbit);
			listOfRabbitDeaths[cod]++;
			Destroy(rabbit.gameObject);
			Debug.Log(rabbit + " removed from the list");
		}
	}

	public void RemoveFox(Fox fox, CauseOfDeath cod)
	{
		if (foxes.Contains(fox))
		{
			foxes.Remove(fox);
			listOfFoxDeaths[cod]++;
			Debug.Log(fox + " removed from the list");
		}
	}

}
