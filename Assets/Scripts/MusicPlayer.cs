using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	
	static MusicPlayer instance = null;

	// a static has been set up otherwise everytime that the game is restarted a new music player would be made, with this code, the new music player
	// is destoryed when the game is restarted, preventing a music track from playing twice and overlapping.
	void Start () {
		
		if (instance != null) 
		{
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else 
			{
				instance = this;
				GameObject.DontDestroyOnLoad (gameObject);
			}
	}
	void Update () 
	{	
	}
}
