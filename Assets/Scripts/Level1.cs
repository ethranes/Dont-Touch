using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour {

	public Question[] questions;
	private static List<Question> unansweredQuestions;

	//private int userScore = 0;
	public Text scoreText;

	private int m_timeRemaining = 10;

	private Question currentQuestion;

	[SerializeField]
	private Text factText = null;

	[SerializeField]
	private Text trueAnserText = null;

	[SerializeField]
	private Text falseAnswerText = null;

	[SerializeField]
	private Animator animator = null;

	[SerializeField]
	private float timeBetweenQuestions = 1f;

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
		int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
		currentQuestion = unansweredQuestions[randomQuestionIndex];

		factText.text = currentQuestion.fact;

		if (currentQuestion.isTrue) {
			trueAnserText.text = "CORRECT";
			falseAnswerText.text = "WRONG";
		} else {
			trueAnserText.text = "WRONG";
			falseAnswerText.text = "CORRECT";
		}
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
			var GO = new GameObject("State");
			var ISS = GO.AddComponent<TimeController>();
			ISS.TimeRemaining = m_timeRemaining; // with m_timeRemaining being whatever way you're keeping the remaining time
			DontDestroyOnLoad(GO);
			SceneManager.LoadScene ("Level 2");
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
			Debug.Log ("Correct");

			SceneManager.LoadScene ("Level 2");
		} else {
			Debug.Log ("Wrong!");
			SceneManager.LoadScene ("Lose"); //This makes sure that the scene will switch to the Lose scene if the player gets the question wrong
		}

		StartCoroutine (TransitionToNextQuestion ());
	}
}