using UnityEngine;
using System.Collections;

public class colourSwitchSprite : MonoBehaviour {

	public Sprite GreenCircle;
	public Sprite WhiteCircle;

	float timer = 1f;
	float delay = 1f;

	void Start()
	{
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = GreenCircle;
	}

	void Update(){
		timer -= Time.deltaTime;
		if (timer <= 0) {
			if (this.gameObject.GetComponent<SpriteRenderer> ().sprite == GreenCircle) 
			{
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = WhiteCircle;
				timer = delay;
				return;
			}
		}
		if (this.gameObject.GetComponent<SpriteRenderer> ().sprite == WhiteCircle) 
		{
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = GreenCircle;
			timer = delay;
			return;
		}
	}

	}




