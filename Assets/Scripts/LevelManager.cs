using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {
	public AudioSource source;
	public void LoadLevel(string name){
		Debug.Log("level load requested for: " + name);
		//source.Play ();
		SceneManager.LoadScene (name);

		Cursor.visible = true;
	}

}
