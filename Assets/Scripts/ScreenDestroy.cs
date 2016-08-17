using UnityEngine;
using System.Collections;

public class ScreenDestroy : MonoBehaviour {

//	void Awake() {
//		Destroy (GameObject.Find("Level2screen"));
//		Destroy (GameObject.Find("Level3screen"));
//		Destroy (GameObject.Find("Level4screen"));
//		Destroy (GameObject.Find("Level5screen"));
//	}

//	void Start () {
//
//		Destroy (GameObject.Find("Level2screen"));
//		Destroy (GameObject.Find("Level3screen"));
//		Destroy (GameObject.Find("Level4screen"));
//		Destroy (GameObject.Find("Level5screen"));
//	}
		
	void Update () {

		Destroy (GameObject.Find("Level2screen"));
		Destroy (GameObject.Find("Level3screen"));
		Destroy (GameObject.Find("Level4screen"));
		Destroy (GameObject.Find("Level5screen"));
	}
}
