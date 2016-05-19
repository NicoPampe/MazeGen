using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private Maze mazeInstance;
	private Player playerInstance;

	public Maze mazePrefab;
	public Player playerPrefab;

	// Use this for initialization
	void Start () {
		BeginGame ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			RestartGame();
		}
	}

	void BeginGame ()
	{
		mazeInstance = Instantiate(mazePrefab) as Maze;
		StartCoroutine( mazeInstance.Generate() );
//		mazeInstance.Generate ();
		playerInstance = Instantiate (playerPrefab) as Player;
		playerInstance.SetLocation (mazeInstance.GetCell (mazeInstance.RandomCoordinates));
	}

	void RestartGame ()
	{
		StopAllCoroutines ();
		Destroy (mazeInstance.gameObject);
		if (playerInstance != null) {
			Destroy(playerInstance.gameObject);
		}
		BeginGame ();
	}
}
