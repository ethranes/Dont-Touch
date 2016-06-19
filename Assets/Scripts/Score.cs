using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {

	private int userScore;
	public Text scoreText;

	void Start ()
	{
		userScore = 0;
		SetScoreText ();
	}

	void FixedUpdate ()
	{
		if (userScore > 2) {
			SceneManager.LoadScene ("Win");
			Cursor.visible = true;
		}
	}

	void OnTriggerEnter(Collider other)
	{		


		if (other.gameObject.CompareTag ("correctAnswer"))
		{

			other.gameObject.SetActive (false);
			userScore = userScore + 1;
			SetScoreText ();
		}

	}

	void SetScoreText ()
	{
		scoreText.text = "Score: " + userScore.ToString ();
	}
}
