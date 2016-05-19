using UnityEngine;
using System.Collections.Generic;

public class MazeRoom : ScriptableObject {
	private List<MazeCell> mCells = new List<MazeCell>();

	public int mSettingsIndex;
	public MazeRoomSettings mSettings;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Add the specified cell.
	/// </summary>
	/// <param name="cell">Cell.</param>
	public void Add(MazeCell cell) {
		cell.mRoom = this;
		mCells.Add (cell);
	}
}
