using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using Pathfinding;

// Behaviour Tree requires the GameObject to have Animal component
[UnityEngine.RequireComponent(typeof(Rabbit))]
public class RabbitBT : BehaviourTree.BehaviourTree
{
	public Rabbit rabbit;
	public AIPath ai;
	public Node activeNode = null;

    public void Awake()
    {
        rabbit = transform.GetComponent<Rabbit>();
		ai = transform.GetComponent<AIPath>();
    }
    protected override Node SetupTree()
    {
		Node root = new Selector(new List<Node>
		{
			new Sequence(new List<Node>
			{
				new Rabbit_CheckForPredators(transform),
				new Rabbit_TaskRun(transform)
			}),
			new Selector(new List<Node>
			{
				new Selector(new List<Node>
				{
					new Sequence(new List<Node>
					   {
						   new Rabbit_WaterInRange(transform),
						   new Rabbit_TaskDrink(transform)
					   }),

					   new Sequence(new List<Node>
					   {
						   new Rabbit_SearchForWater(transform),
						   new Rabbit_TaskMoveToWater(transform)
					   })
				   }),

				   new Selector(new List<Node>
				   {
					   new Sequence(new List<Node>
					   {
						   new Rabbit_FoodInEatRange(transform),
						   new Rabbit_TaskEat(transform)
					   }),

					   new Sequence(new List<Node>
					   {
						   new Rabbit_SearchForFood(transform),
						   new Rabbit_TaskMoveToFood(transform)
					   })
				   })

			   }),
			   new Selector(new List<Node>
			   {
				   new Sequence(new List<Node>
				   {
					   new Rabbit_MateInRange(transform),
					   new Rabbit_TaskMate(transform)
				   }),

				   new Sequence(new List<Node>
				   {
					   new Rabbit_SearchMate(transform),
					   new Rabbit_TaskMoveToMate(transform)
				   })
			   }),
			   new Rabbit_RandomMove(transform, rabbit.searchRadius)
			});
         
			UnityEngine.Debug.Log("Rabbit Behaviour Tree has been set up" + GetInstanceID());
			return root;
		}

	}

