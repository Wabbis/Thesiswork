using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    public bool DisableSpawning;
    public Slider slider;
    public GameObject prefab;

    private float reproductionRate = 5f; // Seconds to reprodcue
    private float reprodcutionAmount = 3; // Number of plants created
    private float reproductionRange = 10f; 

    private float reproductionTimer = 0f; 

    void Update()
    {
        reproductionTimer += Time.deltaTime;
        slider.value = reproductionTimer / reproductionRate;
        if(reproductionTimer > reproductionRate)
        {
            if (!DisableSpawning)
            {
                for (int i = 0; i < reprodcutionAmount; i++)
                {
                    GameObject plant = Instantiate(prefab);
                    plant.transform.position = Utility.FindRandomPointOnNavMesh(transform, reproductionRange);
                    plant.transform.name = "Plant";
                }
            }
            reproductionTimer = 0f;
        }
    }


}
