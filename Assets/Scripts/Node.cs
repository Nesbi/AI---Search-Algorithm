using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {

	public List<Node> neighbours = new List<Node> ();
	public bool isStart, isEnd = false;
	public bool walkable = true;

	public void setNeigbours(List<Node> matrix, float directDistance){
		Vector3 nodePosition = gameObject.transform.position;

		Vector3 diagonalVector = new Vector3 (directDistance*1.1f, directDistance*1.1f, 0);
		float neighbourDistance = Vector3.Distance(nodePosition, nodePosition+diagonalVector);

		foreach (Node node in matrix) {
			float nodeDistance = Vector3.Distance(nodePosition, node.transform.position);

			if (nodeDistance < neighbourDistance && !node.Equals(this)) {
				neighbours.Add(node);
			}
		}
	}

	public void setDefault(){
		isStart = false;
		isEnd = false;
		walkable = true;
		changeColor (new Color (0, 0, 1));
	}

	public void setVisited(){
		if (walkable && !isStart && !isEnd) {
			changeColor (new Color (1, 0, 0));
		}
	}

	public void setInspected(){
		if (isStart) {
			changeColor (new Color (0, 0.7f, 0));
		} else if (isEnd) {
			changeColor (new Color (0, 0.7f, 0.7f));
		} else if (!walkable) {
			changeColor (new Color (0.3f, 0.3f, 0.3f));
		} else {
			changeColor (new Color (0.7f, 0, 0.7f));
		}
	}

	public void setPath(){
		if (!isStart && !isEnd) {
			changeColor (new Color (1, 1, 0));
		}
	}

	public void setToWall(){
		changeColor (new Color (0, 0, 0));
		walkable = false;
	}

	public void setToStart(){
		changeColor (new Color (0,1,0));
		walkable = true;
		isStart = true;
	}
		
	public void setToEnd(){
		changeColor (new Color (0,1,1));
		walkable = true;
		isEnd = true;
	}

	public void changeColor(Color color){
		gameObject.GetComponent<Renderer> ().material.color = color;
	}
}
