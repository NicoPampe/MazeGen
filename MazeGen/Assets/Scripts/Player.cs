using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private MazeCell mCurrentCell;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown(KeyCode.UpArrow)) {
//			Move(MazeDirection.North);
//		}
//		else if (Input.GetKeyDown(KeyCode.DownArrow)) {
//			Move(MazeDirection.South);
//		}
//		else if (Input.GetKeyDown(KeyCode.RightArrow)) {
//			Move(MazeDirection.West);
//		}
//		else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
//			Move(MazeDirection.East);
//		}
	}

	public void SetLocation(MazeCell cell) {
		mCurrentCell = cell;
		transform.localPosition = cell.transform.localPosition;
	}

	private void Move (MazeDirection direction) {
		MazeCellEdge edge = mCurrentCell.getEdge (direction);
		if (edge is MazePassage) {
			SetLocation(edge.mOtherCell);
		}
	}
}
