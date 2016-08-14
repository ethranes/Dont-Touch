using UnityEngine;
using System.Collections;

public class clockDestroy : MonoBehaviour {

	void Awake() {

		Destroy (GameObject.Find("Clock"));

	}

	void Start () {

		Destroy (GameObject.Find("Clock"));
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Destroy (GameObject.Find("Clock"));

	}
}
