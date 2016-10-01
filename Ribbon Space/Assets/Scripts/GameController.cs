using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //use to reload scene

public class GameController : MonoBehaviour {
	
	public GameObject[] hazards;			//array of hazards including crates and enemies
	public GameObject[] blocks;				//array of blocks of different sizes

	public GUIText scoreText;				//display of score
	public GUIText restartText;				//display of restart message
	public GUIText gameOverText;			//display of game over message
	public GUIText winText;					//display of victory message

	public Vector3 spawnValues;				//3D vector of where to spawn hazards
	public Vector3 blockValues;				//3D vector of where to spawn blocks

	private int score;						//score of player
	public int scoreNeededToWin;			//score required to win game

	public float spawnWait;					//time between spawning of hazards
	public float blockSpawnWait;			//time between spawning of blocks
	public float startWait;					//delay spawning at beginning to let player get ready

	private bool win;						//did the player win
	private bool gameOver;					//did the player lose
	private bool restart;					//does the player want to restart

	//add to the score by adding new values
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
		//call win function if the player's score matches the score needed to win
		if (score == scoreNeededToWin) {
			Win ();
		}
	}

	//update score GUI for player to see
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score + "/" + scoreNeededToWin;
	}

	//displays game over text and sets game over to true
	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}

	//displays win text and sets win to true
	public void Win (){
		winText.text = "Victory!! The Galaxy is safe. \n For now...";
		win = true;
	}

	IEnumerator SpawnWaves(){
		//delay spawning only at start
		yield return new WaitForSeconds (startWait);

		//while the player didn't lose or win, spawn hazards
		while(gameOver == false && win == false)
		{
			//choose random game object to instantiate
			GameObject toInstantiate = hazards[Random.Range (0, hazards.Length)];
			//choose random location along y axis to instantiate it
			Vector3 spawnPosition = new Vector3 (spawnValues.x, Random.Range (-spawnValues.y, spawnValues.y), 0f);
			//instantiate object
			Instantiate (toInstantiate, spawnPosition, Quaternion.identity);

			//wait for spawnWait seconds before spawning anything else
			yield return new WaitForSeconds (spawnWait);

			//display restart text if player won or lost and set restart to true
			if (gameOver || win)
			{
				restartText.text = "Press 'R' to play again";
				restart = true;
			}
		}
	}

	IEnumerator SpawnBlocks(){
		//delay spawning only at start
		yield return new WaitForSeconds (startWait);
		//while the player didn't lose or win, spawn blocks
		while(gameOver == false && win == false)
		{
			//get a random integer
			int whereChoice = Random.Range(1,3);
			//choose random object to instantiate from array of blocks
			GameObject toInstantiate = blocks[Random.Range (0, blocks.Length)];
			//pick a position either at the top or bottom of scene
			Vector3 blockPosition01 = new Vector3 (blockValues.x, blockValues.y, 0f);
			//if at bottom, stagger the blocks for variety
			Vector3 blockPosition02 = new Vector3 (blockValues.x + Random.Range(5,6), -blockValues.y, 0f);
			//randomly spawn blocks
			if (whereChoice == 2) {
				Instantiate (toInstantiate, blockPosition01, Quaternion.identity);
			} else {
				Instantiate (toInstantiate, blockPosition02, Quaternion.identity);
			}
			//wait for blockSpawnWait seconds before spawning anything else
			yield return new WaitForSeconds (blockSpawnWait);
			//display restart text if player won or lost and set restart to true
			if (gameOver || win )
			{
				restartText.text = "Press 'R' to play again";
				restart = true;
			}
		}
	}


	// Use this for initialization
	void Start () {
		
		win = false;
		gameOver = false;
		restart = false;

		winText.text = "";
		gameOverText.text = "";
		restartText.text = "";
		score = 0;
		UpdateScore ();


		//spawn obstacles
		StartCoroutine (SpawnWaves ());

		StartCoroutine (SpawnBlocks ());
	}

	void Update ()
	{
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				SceneManager.LoadScene("Level01");
			}
		}
	}
}
