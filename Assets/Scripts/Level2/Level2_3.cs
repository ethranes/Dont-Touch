using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class Level2_3 : MonoBehaviour {

	public Question[] questions;
	private static List<Question> unansweredQuestions;

	//private int userScore = 0;
	public Text scoreText;

	private Question currentQuestion;

	[SerializeField]
	private Text factText = null;

	[SerializeField]
	private Text trueAnserText;

	[SerializeField]
	private Text falseAnswerText;

	[SerializeField]
	private Animator animator = null;

	[SerializeField]
	private float timeBetweenQuestions = 1f;

	[SerializeField] 
	private Text countdownTimer;


	void Start ()
	{

		if (unansweredQuestions == null || unansweredQuestions.Count == 0) 
		{
			unansweredQuestions = questions.ToList<Question>();
		}

		SetCurrentQuestion();
	}

	void SetCurrentQuestion ()
	{
		int randomQuestionIndex = UnityEngine.Random.Range(0, unansweredQuestions.Count);
		currentQuestion = unansweredQuestions[randomQuestionIndex];

		factText.text = currentQuestion.fact;

		//		if (currentQuestion.isTrue) {
		//			trueAnserText.text = "CORRECT";
		//			falseAnswerText.text = "WRONG";
		//		} else {
		//			trueAnserText.text = "WRONG";
		//			falseAnswerText.text = "CORRECT";
		//		}
	}

	IEnumerator TransitionToNextQuestion ()
	{
		unansweredQuestions.Remove(currentQuestion);

		yield return new WaitForSeconds (timeBetweenQuestions);

		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	public void UserSelectTrue ()
	{
		animator.SetTrigger ("True");
		if (currentQuestion.isTrue) {
			Debug.Log ("Correct");
			SceneManager.LoadScene ("Level2.4");
		} else {
			Debug.Log ("Wrong!");
			SceneManager.LoadScene ("Lose"); //This makes sure that the scene will switch to the Lose scene if the player gets the question wrong
		}

		StartCoroutine (TransitionToNextQuestion ());
	}

	public void UserSelectFalse ()
	{
		animator.SetTrigger ("False");
		if (!currentQuestion.isTrue) {
			PlayerPrefs.SetInt("Level3", 0);//This is set on the last scene of each level to ensure that if the player choses the correct answer it unlocks the level level, this is linked with LevelManagerNew.cs
			Debug.Log ("Correct");
			SceneManager.LoadScene ("Level2.4");
		} else {
			Debug.Log ("Wrong!");
			SceneManager.LoadScene ("Lose"); //This makes sure that the scene will switch to the Lose scene if the player gets the question wrong
		}

		StartCoroutine (TransitionToNextQuestion ());
	}

	void Update (){

		countdownTimer.text = GlobalCountDown.TimeLeft.Seconds.ToString();
		{
			if (GlobalCountDown.TimeLeft == TimeSpan.Zero)
				SceneManager.LoadScene("LoseTime");  //if the timer reaches 0 then the Lose scene will load
		}
	}


}