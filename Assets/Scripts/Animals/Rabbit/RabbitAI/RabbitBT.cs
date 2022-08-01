using System.Collections;
using System.Collections.Generic;
using BehaviourTree;

// Behaviour Tree requires the GameObject to have Animal component
[UnityEngine.RequireComponent(typeof(Rabbit))]
public class RabbitBT : BehaviourTree.BehaviourTree
{
    public static Rabbit rabbit;

    public void Awake()
    {
        rabbit = transform.GetComponent<Rabbit>();
    }
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new Rabbit_TargetInEatRange(transform),
                new Rabbit_TaskEat(transform)
            }),
            
            new Sequence(new List<Node>
            {
                new Rabbit_SearchForFood(transform),
                new Rabbit_TaskMoveToFood(transform),
            }),

            new Rabbit_RandomMove(transform, rabbit.searchRadius)
        }) ;
         
        UnityEngine.Debug.Log("Rabbit Behaviour Tree has been set up");

        return root;
    }

}

