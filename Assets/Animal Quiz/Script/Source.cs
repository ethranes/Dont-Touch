using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
/*
 * Handle Animals
*/
public class Source : MonoBehaviour {
	public AudioClip audioCorrect;
	public AudioClip audioFail;
	public Text Score;
	public Text Best;
	public Text Score_GameOver;
	public Text Best_GameOver;
	public Timer timer;
	public GameObject GameOver;
	public Text AnimalTargetName;
	public Image AnimalTargetImage;
	public List<Image> AnimalUIImage = new List<Image> ();
	public List<Animal> listAnimals = new List<Animal>();

	List<Image> copyAnimalUIImage;
	List<Animal> copyListAnimal;

	private int ID_correct;
	private int ID;
	[HideInInspector]
	public int score;
	private AudioSource audioSource;
	private AudioClip animalVoice;
	[HideInInspector]
	public bool playSound = true;
	[HideInInspector]
	public bool gameOver = false;
	[HideInInspector]
	public int best;

	private int rand;

	// Use this for initialization
	void Start () {
		//Set target frame rate 30 to save battery life
		Application.targetFrameRate = 30;
		score = 0;
		Score.text = "0";
	
		audioSource = GetComponent<AudioSource> ();

		New ();
	}
	
	// Update is called once per frame
	void Update () {
		if (timer.timeUp && !gameOver) {
			gameOver = true;
			GameFinish ();
		}
	}

	//Create new 4 random animals

	public void New(){
		copyAnimalUIImage = new List<Image>(AnimalUIImage);
		copyListAnimal = new List<Animal>(listAnimals);

		// #1
		rand = Random.Range (0, copyListAnimal.Count);
		Animal ani_1 = (Animal) copyListAnimal[rand];
		copyListAnimal.RemoveAt (rand);

		ID = Random.Range (0, copyAnimalUIImage.Count);
		Image image_1 = (Image)copyAnimalUIImage [ID];
		copyAnimalUIImage.RemoveAt (ID);

		SetAnimal (ani_1, image_1, true);

		// #2
		rand = Random.Range (0, copyListAnimal.Count);
		Animal ani_2 = (Animal) copyListAnimal[rand];
		copyListAnimal.RemoveAt (rand);

		rand = Random.Range (0, copyAnimalUIImage.Count);
		Image image_2 = (Image)copyAnimalUIImage [rand];
		copyAnimalUIImage.RemoveAt (rand);

		SetAnimal (ani_2, image_2, false);

		// #3
		rand = Random.Range (0, copyListAnimal.Count);
		Animal ani_3 = (Animal) copyListAnimal[rand];
		copyListAnimal.RemoveAt (rand);

		rand = Random.Range (0, copyAnimalUIImage.Count);
		Image image_3 = (Image)copyAnimalUIImage [rand];
		copyAnimalUIImage.RemoveAt (rand);

		SetAnimal (ani_3, image_3, false);

		// #4
		rand = Random.Range (0, copyListAnimal.Count);
		Animal ani_4 = (Animal) copyListAnimal[rand];
		copyListAnimal.RemoveAt (rand);

		rand = Random.Range (0, copyAnimalUIImage.Count);
		Image image_4 = (Image)copyAnimalUIImage [rand];
		copyAnimalUIImage.RemoveAt (rand);

		SetAnimal (ani_4, image_4, false);

	}

	//Get and set value of the animal in list
	void SetAnimal(Animal animal, Image image, bool target){
		List<Sprite> copyListSprite = new List<Sprite>(animal.AnimalImages);
		rand = Random.Range (0, copyListSprite.Count);
		Sprite sprite = (Sprite) copyListSprite[rand];
		image.sprite = sprite;

		if (target) {
			ID_correct = ID;
			animalVoice = animal.voice;
			copyListSprite.RemoveAt (rand);
			rand = Random.Range (0, copyListSprite.Count);
			sprite = (Sprite) copyListSprite[rand];

			AnimalTargetName.text = animal.Name;
			AnimalTargetImage.sprite = sprite;
		}

	}

	//Send by 4 image button in UI
	public void Pick(int id){
		if (!gameOver) {
			if (id == ID_correct) {
				//allow timer run
				timer.allowTimer = true;
				//reset timer
				timer.currentTime = 0;
				//play sound
				if (playSound) {
					audioSource.PlayOneShot (audioCorrect,0.75f);
					if (animalVoice) {
						audioSource.PlayOneShot (animalVoice, 0.2f);
					}
				}
				//add score
				score++;
				Score.text = score + "";
				//setup new animal
				New ();
			} else {
				GameFinish ();
			}
		}
	}

	void GameFinish(){
		AdsCotroller.ShowAds ();	//show ads

		timer.allowTimer = false;
		gameOver = true;
		if (playSound) {
			audioSource.PlayOneShot (audioFail,0.75f);
		}
		CheckBest ();
		GameOver.GetComponent<Animator> ().SetBool ("show",true);
	}

	//Check and save best score
	void CheckBest(){
		if (Timer.isKidMode) {
			if (PlayerPrefs.HasKey ("bestkid")) {
				best = PlayerPrefs.GetInt ("bestkid");
				if (score > best) {
					PlayerPrefs.SetInt ("bestkid", score);
					Best.text = score + "";
					Best_GameOver.text = score + "";
				}
			} else {
				PlayerPrefs.SetInt ("bestkid", score);
				Best.text = score + "";
				Best_GameOver.text = score + "";
			}
		} else {
			if (PlayerPrefs.HasKey ("best")) {
				best = PlayerPrefs.GetInt ("best");
				if (score > best) {
					PlayerPrefs.SetInt ("best", score);
					Best.text = score + "";
					Best_GameOver.text = score + "";
				}
			} else {
				PlayerPrefs.SetInt ("best", score);
				Best.text = score + "";
				Best_GameOver.text = score + "";
			}
		}
		Score_GameOver.text = score + "";
	}

	//Reset value when hit restart level button
	public void Reset(){
		AdsCotroller.HideAds (); //hide ads when restart game
		score = 0;
		Score.text = score + "";
		timer.Reset ();
		gameOver = false;
		New ();
		SetBest ();
	}

	//Set Best score to UI when playing
	void SetBest(){
		if (Timer.isKidMode) {
			if (PlayerPrefs.HasKey ("bestkid")) {
				Best.text = PlayerPrefs.GetInt ("bestkid") + "";
				Best_GameOver.text = PlayerPrefs.GetInt ("bestkid") + "";
			} else {
				Best.text = "0";
				Best_GameOver.text = "0";
			}
		} else {
			if (PlayerPrefs.HasKey ("best")) {
				Best.text = PlayerPrefs.GetInt ("best") + "";
				Best_GameOver.text = PlayerPrefs.GetInt ("best") + "";
			} else {
				Best.text = "0";
				Best_GameOver.text = "0";
			}
		}
	}

}
