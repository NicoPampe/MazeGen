using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {
	private MazeCell [,] mCells;
	private List<MazeRoom> mRooms = new List<MazeRoom> ();

	public IntVector2 mSize;
	public MazeCell mCellPrefab;
	public MazeWall mWallPrefab;
	public MazePassage mPassgaePrefab;
	public MazeDoor mDoorPrefab;
	public MazeRoomSettings[] mRoomSettings;

	public float mGenerationStepDelay;

	public int mRoomCreatedTest;

	[Range (0f, 1f)]
	public float mDoorProbability;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Generate this instance of the Maze.
	/// </summary>
	// These are used to delay the generation so it is visible. Good for Debug
	public IEnumerator Generate () {
		WaitForSeconds delay = new WaitForSeconds (mGenerationStepDelay);
	//	public void Generate () {
		mCells = new MazeCell[mSize.x, mSize.z];
		List<MazeCell> activeCells = new List<MazeCell> ();
		DoFirstGenerationStep (activeCells);

		mRoomCreatedTest = 0;

		while (activeCells.Count > 0) {
			yield return delay;
			DoNextGenerationStep(activeCells);
		}
	}

	/// <summary>
	/// Gets the cell.
	/// </summary>
	/// <returns>The cell.</returns>
	/// <param name="coord">Coordinate.</param>
	public MazeCell GetCell (IntVector2 coord) {
		return mCells [coord.x, coord.z];
	}

	public MazeRoom CreateRoom (int indexToExlude) {
		MazeRoom newRoom = ScriptableObject.CreateInstance<MazeRoom>();
		newRoom.mSettingsIndex = Random.Range (0, mRoomSettings.Length);
		if (newRoom.mSettingsIndex == indexToExlude) {
			newRoom.mSettingsIndex = (newRoom.mSettingsIndex + 1) % mRoomSettings.Length;
		}
		newRoom.mSettings = mRoomSettings [newRoom.mSettingsIndex];
		mRooms.Add (newRoom);
		return newRoom;
	}

	/// <summary>
	/// Gets the random coordinates inside the bounds.
	/// </summary>
	/// <value>The random coordinates.</value>
	public IntVector2 RandomCoordinates {
		get {
			return new IntVector2(Random.Range(0, mSize.x), Random.Range(0, mSize.z));
		}
	}

	/// <summary>
	/// Dos the first generation step of the maze.
	/// </summary>
	/// <param name="activeCells">Active cells.</param>
	void DoFirstGenerationStep (List<MazeCell> activeCells)
	{
		MazeCell newCell = CreateCell (RandomCoordinates);
		newCell.Initialized (CreateRoom (-1));
		activeCells.Add (newCell);
	}

	/// <summary>
	/// Dos the next generation step of the maze.
	/// currentIndex is set back one due to first gen.
	/// If ConatinsCoord and GetCell not null
	/// <c>true</c>, add cell, <c>false</c> remove at index.
	/// </summary>
	/// <param name="activeCells">Active cells.</param>
	private void DoNextGenerationStep (List<MazeCell> activeCells)
	{
		// set index back one due to first gen
		int currentIndex = activeCells.Count - 1;
		MazeCell currentCell = activeCells [currentIndex];
		if (currentCell.IsFullyInitialized) {
			activeCells.RemoveAt(currentIndex);
			return;
		}
		MazeDirection direction = currentCell.RandomUninitializedDirection;
		IntVector2 coord = currentCell.coordinates + direction.ToIntVector2 ();
		if (ContainsCoordinates (coord)) {
			MazeCell neighbor = GetCell(coord);
			if (neighbor == null) {
				neighbor = CreateCell(coord);
				CreatePassage(currentCell, neighbor, direction);
				activeCells.Add(neighbor);
			} else {
				CreateWall(currentCell, neighbor, direction);
			}
		} else {
			CreateWall(currentCell, null, direction);
		}
	}


	/// <summary>
	/// Creates the passage.
	/// Passage has a Initialize on either side.
	/// </summary>
	/// <param name="currentCell">Current cell.</param>
	/// <param name="neighbor">Neighbor.</param>
	/// <param name="direction">Direction.</param>
	void CreatePassage (MazeCell currentCell, MazeCell neighbor, MazeDirection direction)
	{
		MazePassage prefab = Random.value < mDoorProbability ? mDoorPrefab : mPassgaePrefab;
		MazePassage passage = Instantiate (prefab) as MazePassage;
		passage.Initialize (currentCell, neighbor, direction);
		passage = Instantiate (prefab) as MazePassage;
		if (passage is MazeDoor) {
			neighbor.Initialized (CreateRoom (currentCell.mRoom.mSettingsIndex));
			mRoomCreatedTest += 1;
		} else {
			neighbor.Initialized (currentCell.mRoom);
		}
		passage.Initialize (neighbor, currentCell, direction.GetOpposite ());
	}

	/// <summary>
	/// Creates the wall.
	/// </summary>
	/// <param name="currentCell">Current cell.</param>
	/// <param name="neighbor">Neighbor.</param>
	/// <param name="direction">Direction.</param>
	void CreateWall (MazeCell currentCell, MazeCell neighbor, MazeDirection direction)
	{
		MazeWall wall = Instantiate (mWallPrefab) as MazeWall;
		wall.Initialize (currentCell, neighbor, direction);
		if (neighbor != null) {
			wall = Instantiate(mWallPrefab) as MazeWall;
			wall.Initialize(neighbor, currentCell, direction.GetOpposite());
		}
	}

	/// <summary>
	/// Check whether some coordinates fall inside the maze
	/// </summary>
	/// <returns><c>true</c>, if coordinates was containsed, <c>false</c> otherwise.</returns>
	/// <param name="coord">Coordinate.</param>
	bool ContainsCoordinates (IntVector2 coord)
	{
		return coord.x >= 0 && coord.x < mSize.x 
			&& coord.z >= 0 && coord.z < mSize.z;
	}

	/// <summary>
	/// Maxs the number of room create.
	/// For testing
	/// </summary>
	/// <returns><c>true</c>, if number of room create was maxed, <c>false</c> otherwise.</returns>
	bool MaxNumberOfRoomCreate () {
		// TODO: remove.
		return mRoomCreatedTest >= 3;
	}

	/// <summary>
	/// Single cell creation
	/// </summary>
	/// <param name="coordinates">Coordinates.</param>
	MazeCell CreateCell (IntVector2 coordinates)
	{
		MazeCell newCell = Instantiate (mCellPrefab) as MazeCell;
		mCells [coordinates.x, coordinates.z] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Maze Cell " + coordinates.x + "," + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3 (coordinates.x - mSize.x * 0.5f + 0.5f, 0f, coordinates.z - mSize.z * 0.5f + 0.5f);
		return newCell;
	}
}
