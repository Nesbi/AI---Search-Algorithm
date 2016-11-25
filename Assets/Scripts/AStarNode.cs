using UnityEngine;
using System.Collections;

public class AStarNode : Node {

	public float fcost = float.MaxValue;
	public float hcost = 0;
	public float gcost = 0;
	public AStarNode parent;


	public float calculateFCost(AStarNode neighbour, AStarNode end){
		Vector3 position = gameObject.transform.position;

		gcost = neighbour.gcost + Vector3.Distance (position,
			neighbour.gameObject.transform.position);
		hcost = Vector3.Distance (position, end.gameObject.transform.position);

		return gcost + hcost;
	}
}
