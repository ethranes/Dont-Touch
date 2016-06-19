using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public float pointsPerHOG = 10;
	private float score;
	public GameObject HOGobj;

	// Use this for initialization
	void Start () {
		HOGobj = GameObject.FindWithTag("correctAnswer");
	}

	// Update is called once per frame
	void Update () {

		//The intent is to have only the HOGobj add to the score when clicked.
		if (Input.GetMouseButtonDown (0)) {

			if (HOGobj) {
				AddPoints (pointsPerHOG);
			}
		}

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			// Casts the ray and get the first game object hit
			Physics.Raycast (ray, out hit);
			if (hit.collider.tag == "correctAnswer") {    
				AddPoints (pointsPerHOG);
			}

		}
	}

	void OnGUI() {
		GUILayout.Label("score: " + score.ToString("0"));
	}


	void AddPoints(float points){
		score += points;
	}

}