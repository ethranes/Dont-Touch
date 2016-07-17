using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelManagerNew : MonoBehaviour {

	[System.Serializable]
	public class Level
	{
		public string LevelText;
		public int UnLocked;
		public bool IsInteractable;
	}

	public GameObject levelButton;
	public Transform Spacer;
	public List<Level> LevelList;


	// Use this for initialization
	void Start () 
	{
		//DeleteAll ();
		FillList ();
	}
	
	void FillList()
	{
		foreach (var level in LevelList) 
		{
			GameObject newbutton = Instantiate (levelButton) as GameObject;
			LevelButtonNew button = newbutton.GetComponent<LevelButtonNew> ();
			button.LevelText.text = level.LevelText;
			//Level1, Level2,

			if (PlayerPrefs.GetInt ("Level" + button.LevelText.text) == 1) //this if statement unlocked the first level when a player opens the game for the first time
			{
				level.UnLocked = 1;
				level.IsInteractable = true;
			}

			button.unlocked = level.UnLocked;
			button.GetComponent<Button> ().interactable = level.IsInteractable; 
			button.GetComponent<Button> ().onClick.AddListener (() => loadLevels ("Level" + button.LevelText.text)); //loads a level based on the name that the scene has been saved as

			if (PlayerPrefs.GetInt ("Level" + button.LevelText.text + "_score") > 0) 
			{
				button.star1.SetActive (true);
			}	

			if (PlayerPrefs.GetInt ("Level" + button.LevelText.text + "_score") > 5000) 
			{
				button.star2.SetActive (true);
			}	

			if (PlayerPrefs.GetInt ("Level" + button.LevelText.text + "_score") > 9999) 
			{
				button.star3.SetActive (true);
			}


			newbutton.transform.SetParent (Spacer, false);
		}

		SaveAll ();

	}

	void SaveAll ()
	{
//		if (PlayerPrefs.HasKey ("Level1")) 
//		{
//			return;
//		} 
//		else 
		{
			GameObject[] allbuttons = GameObject.FindGameObjectsWithTag ("LevelButton");
			foreach (GameObject buttons in allbuttons) 
			{
				LevelButtonNew button = buttons.GetComponent<LevelButtonNew> ();
				PlayerPrefs.SetInt ("Level" + button.LevelText.text, button.unlocked);
			}
		}
	}

	void DeleteAll()
	{
		PlayerPrefs.DeleteAll ();
	}

	void loadLevels(string value)
	{
		SceneManager.LoadScene (value);
	}
}
