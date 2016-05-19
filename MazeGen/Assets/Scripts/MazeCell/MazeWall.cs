using UnityEngine;
using System.Collections;

public class MazeWall : MazeCellEdge {


	public Transform wall;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Initialize the specified cell, otherCell and direction.
	/// Make the deges children of their cells
	/// </summary>
	/// <param name="cell">Cell.</param>
	/// <param name="otherCell">Other cell.</param>
	/// <param name="direction">Direction.</param>
	public override void Initialize (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		base.Initialize(cell, otherCell, direction);
		wall.GetComponent<Renderer>().material = cell.mRoom.mSettings.mWallMaterial;
	}
}
