using UnityEngine;
using System.Collections;

public class MazeDoor : MazePassage {
	public Transform hinge;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Gets the other side of door.
	/// </summary>
	/// <value>The other side of door.</value>
	private MazeDoor OtherSideOfDoor {
		get {
			return mOtherCell.getEdge(mDirection.GetOpposite()) as MazeDoor;
		}
	}

	public override void Initialize (MazeCell primary, MazeCell other, MazeDirection direction) {
		base.Initialize (primary, other, direction);
		if (OtherSideOfDoor != null) {
			hinge.localScale = new Vector3(-1f, 1f, 1f);
			Vector3 p = hinge.localPosition;
			p.x = -p.x;
			hinge.localPosition = p;
		}
		for (int i = 0; i < transform.childCount; i++) {
			Transform child = transform.GetChild(i);
			if (child != hinge) {
				child.GetComponent<Renderer>().material = mCell.mRoom.mSettings.mWallMaterial;
			}
		}
	}
}
