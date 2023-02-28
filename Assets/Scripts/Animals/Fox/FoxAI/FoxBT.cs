using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using Pathfinding;

// Behaviour Tree requires the GameObject to have Animal component
[UnityEngine.RequireComponent(typeof(Fox))]
public class FoxBT : BehaviourTree.BehaviourTree
{
	public Fox fox;
	public AIPath ai;
	public Node activeNode = null;

	public void Awake()
    {
        fox = transform.GetComponent<Fox>();
		ai = transform.GetComponent<AIPath>();
    }

	protected override Node SetupTree()
	{
		Node root = new Selector(new List<Node>
		{
			new Selector(new List<Node>
			{
				new Selector(new List<Node>
				{
					new Sequence(new List<Node>
					{
						new Fox_WaterInRange(transform),
						new Fox_TaskDrink(transform)
					}),

					new Sequence(new List<Node>
					{
						new Fox_SearchForWater(transform),
						new Fox_TaskMoveToWater(transform)
					})
				}),

				new Selector(new List<Node>
				{
					new Sequence(new List<Node>
					{
						new Fox_FoodInEatRange(transform),
						new Fox_TaskEat(transform)
					}),

					new Sequence(new List<Node>
					{
						new Fox_SearchForFood(transform),
						new Fox_TaskHunt(transform)
					})
				}),

				new Selector(new List<Node>
				{
					new Sequence(new List<Node>
					{
						new Fox_MateInRange(transform),
						new Fox_TaskMate(transform)
					}),

					new Sequence(new List<Node>
					{
						new Fox_SearchMate(transform),
						new Fox_TaskMoveToMate(transform)
					})
				}),
				new Fox_RandomMove(transform, fox.searchRadius)
			})
		});
	
		UnityEngine.Debug.Log("Fox Behaviour Tree has been set up " + GetInstanceID());
		return root;
	}
}
