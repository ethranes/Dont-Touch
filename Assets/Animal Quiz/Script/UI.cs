using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {
	
	public GameObject pauseMenu;
	public GameObject GameOver;
	public GameObject Menu;
	public Image music;
	public Image sound;
	public Sprite imageSoundOn;
	public Sprite imageSoundOff;
	public Sprite imageMusicOn;
	public Sprite imageMusicOff;

	private Source GamePlay;
	private AudioSource audioSource;

	void Start(){

		GamePlay = FindObjectOfType<Source> ();
		audioSource = GetComponent<AudioSource> ();
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (Menu.GetComponent<Animator> ().GetBool ("show")) {
				Debug.Log ("quit");
				Application.Quit ();
			} else {
				if (GameOver.GetComponent<Animator> ().GetBool ("show")) {
					Menu.GetComponent<Animator> ().SetBool ("show",true);
					GameOver.GetComponent<Animator> ().SetBool ("show",false);
					GamePlay.Reset ();
				} else {
					Pause ();
				}
			}
		}
	}

	public void Pause(){
		if (Time.timeScale == 0) {
			pauseMenu.SetActive (false);
			audioSource.UnPause ();
			Time.timeScale = 1;
		} else {
			pauseMenu.SetActive (true);
			audioSource.Pause ();
			Time.timeScale = 0;
		}
	}
	public void Play(bool isKidMode){
		Timer.isKidMode = isKidMode;
		Menu.GetComponent<Animator> ().SetBool ("show",false);
		GamePlay.Reset ();
	}

	public void PlayAgain(){
		GameOver.GetComponent<Animator> ().SetBool ("show",false);
		GamePlay.Reset ();
	}

	public void Sound(){
		if (GamePlay.playSound) {
			GamePlay.playSound = false;
			sound.sprite = imageSoundOff;
		} else {
			GamePlay.playSound = true;
			sound.sprite = imageSoundOn;
		}
	}

	public void Music(){
		if (audioSource.isPlaying) {
			audioSource.Stop ();
			music.sprite = imageMusicOff;
		} else {
			audioSource.Play ();
			music.sprite = imageMusicOn;
		}
	}

	public void Quit(){
		Application.Quit ();
	}

	public void MainMenu(){
		Menu.GetComponent<Animator> ().SetBool ("show",true);
		GameOver.GetComponent<Animator> ().SetBool ("show",false);
		GamePlay.Reset ();
		Pause ();
	}

//	IEnumerator Wait(){
//		yield return new WaitForSeconds (1f);
//		Menu.color = Color.white;
//		Menu.gameObject.SetActive (false);
//	}
}
