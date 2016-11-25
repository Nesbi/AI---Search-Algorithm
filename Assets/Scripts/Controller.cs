using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour {
	
	public Vector3 startPositionMatrix;
	public GameObject standartNode;
	public float nodeDistance;
	public float nodeCountX;
	public float nodeCountY;

	public int wallPercent = 10;


	private Pathfinder pathfinder;
	private List<Node> matrix;
	private Node startNode, endNode;


	void Start () {
		matrix = new List<Node> ();
		createMatrix();

		pathfinder = new AStar ();
		pathfinder.setPath(startNode, endNode);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			pathfinder.findPathNextStep();
		}
		if (Input.GetKeyDown ("f")) {
			pathfinder.findPathRestSteps ();
		}
		if (Input.GetKeyDown ("r")) {
			reset ();	
		}
	}

	private void reset(){
		if (matrix == null || matrix.Count != 0) {
			destroyMatrix ();
		}
		matrix = new List<Node> ();
		createMatrix();

		pathfinder = new AStar ();
		pathfinder.setPath(startNode, endNode);
	}

	private void createMatrix(){

		float deltaPositionX = 0;
		float deltaPositionY = 0;

		for (int i = 0; i < nodeCountX; i++) {
			for (int j = 0; j < nodeCountY; j++) {
				Vector3 delta = new Vector3 (deltaPositionX, deltaPositionY, 0);

				GameObject newNode = Instantiate (standartNode);
				newNode.transform.position = startPositionMatrix + delta;

				matrix.Add (newNode.GetComponent<Node> ());

				deltaPositionX += nodeDistance;

				if (Random.Range (0, 100) <= wallPercent) {
					newNode.GetComponent<Node> ().setToWall ();
				} else {
					newNode.GetComponent<Node> ().setDefault ();
				}
			}
			deltaPositionX = 0;
			deltaPositionY += nodeDistance;
		}

		foreach (Node node in matrix) {
			node.setNeigbours (matrix, nodeDistance);
		}

		setStartEnd ();
	}

	private void destroyMatrix(){
		foreach (Node node in matrix) {
			Destroy (node.gameObject);
		}
	}

	private void setStartEnd(){
		int start = Random.Range (0, matrix.Count);
		int end;
		do {
			end = Random.Range (0, matrix.Count);
		} while (end == start);

		startNode = matrix [start];
		startNode.setToStart ();
		endNode = matrix [end]; 
		endNode.setToEnd ();
	}
}
