using UnityEngine;
using System.Collections;

public abstract class MazeCellEdge : MonoBehaviour {
	public MazeCell mCell, mOtherCell;
	public MazeDirection mDirection;

	/// <summary>
	/// Initialize the specified cell, otherCell and direction.
	/// Make the deges children of their cells
	/// </summary>
	/// <param name="cell">Cell.</param>
	/// <param name="otherCell">Other cell.</param>
	/// <param name="direction">Direction.</param>
	public virtual void Initialize (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		mCell = cell;
		mOtherCell = otherCell;
		mDirection = direction;
		cell.setEdge (direction, this);
		transform.parent = cell.transform;
		transform.localPosition = Vector3.zero;
		transform.localRotation = direction.ToRotation ();
	}
}
