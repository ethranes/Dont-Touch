using UnityEngine;
using System.Collections;

public class Level2screen : MonoBehaviour {

	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {

		DontDestroyOnLoad (gameObject);
		//SceneManager.LoadScene("sceneSelectBeta");
		GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag ("Level2Block");
		foreach (GameObject go in gameObjectArray) {
			go.SetActive (false);
			Debug.Log ("haha");
		}
		StartCoroutine (waitfordelete());
	}

	IEnumerator waitfordelete(){
		yield return new WaitForSeconds (5);
		Destroy (gameObject);
	}
}
