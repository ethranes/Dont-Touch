using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Animal : MonoBehaviour {
	public string Name;
	public List<Sprite> AnimalImages = new List<Sprite> ();
	public AudioClip voice;
}
