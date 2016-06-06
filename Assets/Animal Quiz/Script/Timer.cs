using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Timer : MonoBehaviour {
	public static bool isKidMode = false;
	[HideInInspector]
	public bool allowTimer = false;

	public float timeMax = 1.5f;
	public float timeKidMode = 5f;
	[HideInInspector]
	public float timeRun;
	[HideInInspector]
	public bool timeUp;
	[HideInInspector]
	public float currentTime;
	private RectTransform rect;

	// Use this for initialization
	void Start () {
		currentTime = 0f;
		rect = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		if (allowTimer) {
			currentTime += Time.deltaTime;
			float timeLerp;
			if (!isKidMode) {
				 timeLerp = currentTime / timeMax;
			} else {
				 timeLerp = currentTime / timeKidMode;
			}

			//transform.localScale.x = 1 - timeLerp;

			float x = rect.sizeDelta.x;
			x = 1 - timeLerp;
			this.rect.localScale = new Vector3 (x, 1f, 1f);
			if (timeLerp > 1) {
				allowTimer = false;
				timeUp = true;
			}
		}
	}

	public void Reset(){
		allowTimer = false;
		timeUp = false;
		currentTime = 0f;
		this.rect.localScale = new Vector3 (1f, 1f, 1f);
	}
}
