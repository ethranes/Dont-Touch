using UnityEngine;
using System.Collections;

public class clockDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Destroy (GameObject.Find("Clock"));
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
