using UnityEngine;
using System.Collections;

public static class MazeDirections {
	private static IntVector2[] vectors = {
		new IntVector2 (0, 1),
		new IntVector2 (1, 0),
		new IntVector2 (0, -1),
		new IntVector2 (-1, 0)
	};
	private static MazeDirection[] opposites = {
		MazeDirection.South,
		MazeDirection.West,
		MazeDirection.North,
		MazeDirection.East
	};
	private static Quaternion[] rotations = {
		Quaternion.identity,
		Quaternion.Euler(0f, 90f, 0f),
		Quaternion.Euler(0f, 180f, 0f),
		Quaternion.Euler(0f, 270f, 0f),
	};

	public const int Count = 4;

	/// <summary>
	/// Gets the random value.
	/// </summary>
	/// <value>The random value.</value>
	public static MazeDirection RandomValue {
		get {
			return (MazeDirection)Random.Range(0, Count);
		}
	}

	/// <summary>
	/// Tos the int vector2.
	/// </summary>
	/// <returns>The int vector2.</returns>
	/// <param name="direction">Direction enum.</param>
	public static IntVector2 ToIntVector2 (this MazeDirection direction) {
		return vectors [(int)direction];
	}

	/// <summary>
	/// Gets the opposite of the direction.
	/// </summary>
	/// <returns>The opposite.</returns>
	/// <param name="direction">Direction.</param>
	public static MazeDirection GetOpposite (this MazeDirection direction) {
		return opposites[(int)direction];
	}

	/// <summary>
	/// Tos the rotation.
	/// </summary>
	/// <returns>The rotation.</returns>
	/// <param name="direction">Direction.</param>
	public static Quaternion ToRotation (this MazeDirection direction) {
		return rotations [(int)direction];
	}
}
