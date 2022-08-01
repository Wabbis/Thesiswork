using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

// Behaviour Tree requires the GameObject to have Animal component
[UnityEngine.RequireComponent(typeof(Animal))]
public class FoxBT : BehaviourTree.BehaviourTree
{
    public static Animal animal;

    public void Awake()
    {
        animal = transform.GetComponent<Animal>();
    }

    protected override Node SetupTree()
    {
        // TODO: Fox Logic
        UnityEngine.Debug.Log("Rabbit Behaviour Tree has been set up");
        return null;
    }
}
