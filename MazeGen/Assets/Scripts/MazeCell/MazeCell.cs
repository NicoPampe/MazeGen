using UnityEngine;
using System.Collections;

public class MazeCell : MonoBehaviour {
	private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];
	private int initializedEdgeCount;

	public IntVector2 coordinates;
	public MazeRoom mRoom;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Initialized the specified room.
	/// </summary>
	/// <param name="room">Room.</param>
	public void Initialized (MazeRoom room) {
		mRoom = room;
		transform.GetChild (0).GetComponent<Renderer> ().material = room.mSettings.mFloorMaterial;
	}

	/// <summary>
	/// Sets the edge in the edges array
	/// </summary>
	/// <param name="direction">Direction.</param>
	/// <param name="mazeCellEdge">Maze cell edge.</param>
	public void setEdge (MazeDirection direction, MazeCellEdge mazeCellEdge) {
		edges [(int)direction] = mazeCellEdge;
		initializedEdgeCount += 1;
	}

	/// <summary>
	/// Gets the edge.
	/// </summary>
	/// <returns>The edge.</returns>
	/// <param name="direction">Direction.</param>
	public MazeCellEdge getEdge (MazeDirection direction) {
		return edges[(int)direction];
	}

	/// <summary>
	/// Gets a value indicating whether this instance is fully initialized.
	/// </summary>
	/// <value><c>true</c> if this instance is fully initialized; otherwise, <c>false</c>.</value>
	public bool IsFullyInitialized {
		get {
			return initializedEdgeCount == MazeDirections.Count;
		}
	}

	/// <summary>
	/// Gets the random uninitialized direction.
	/// </summary>
	/// <value>The random uninitialized direction.</value>
	public MazeDirection RandomUninitializedDirection {
		get {
			int skips = Random.Range(0, MazeDirections.Count - initializedEdgeCount);
			for(int i = 0; i < MazeDirections.Count; i++) {
				if (edges[i] == null) {
					if (skips == 0) {
						return (MazeDirection)i;
					}
					skips -= 1;
				}
			}
			throw new System.InvalidOperationException("MazeCell has no uninitialized directions left.");
		}
	}

}
