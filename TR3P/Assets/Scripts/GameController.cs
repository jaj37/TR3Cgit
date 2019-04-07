using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

    public Text gameOverText;
    public Text restartText;
    public Text scoreText;
	public Text winText;

	private bool win;
    private bool gameOver;
	private bool restart;
	private int score;
	
	void Start ()
	{
		win = false;
		gameOver = false;
		restart = false;
		winText.text = "";
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}
	
	void Update ()
	{
	
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (restart)
		{
			if (Input.GetKeyDown (KeyCode.F))
			{
                SceneManager.LoadScene("TR3P");
            }
		}
		
			
	}
	
	
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (win)
			{
				restartText.text = "Press 'F' for Restart";
				restart = true;
				break;
			}

			if (gameOver)
			{
				restartText.text = "Press 'F' for Restart";
				restart = true;
				break;
			}
		}
	}
	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();

	}
	
	void UpdateScore ()
	{
		scoreText.text = "Points: " + score;
		if (score >= 100) {
		winText.text = "You Win! Game Created by Jason Johnson";
		win = true;
		}
	}

	public void GameOver ()
	{
		gameOverText.text = "Game Over :(";
		gameOver = true;
	}
}