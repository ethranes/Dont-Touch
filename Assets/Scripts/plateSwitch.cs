using UnityEngine;
using System.Collections;

public class plateSwitch : MonoBehaviour {

	public Sprite plate, setting;
	private SpriteRenderer spriteRenderer;

	void Start() {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		SetVolume(PlayerPrefs.GetInt("volume", 50) != 50 ? false : true);
	}

	void Update() {
		if(isTouched()) {
			SetVolume(PlayerPrefs.GetInt("volume", 50) != 50 ? true : false);
		}
	}

	private void SetVolume(bool IsOn) {
		if(IsOn) {
			spriteRenderer.sprite = plate;
			PlayerPrefs.SetInt("volume", 50);
		} else {
			spriteRenderer.sprite = setting;
			PlayerPrefs.SetInt("volume", 0);
		}
	}

	public bool isTouched() {
		bool result = false;
		if(Input.touchCount == 1) {
			if(Input.touches[0].phase == TouchPhase.Ended) {
				Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				Vector2 touchPos = new Vector2(wp.x, wp.y);
				if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos)) {
					result = true;
				}
			}
		}
		if(Input.GetMouseButtonUp(0)) {
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos = new Vector2(wp.x, wp.y);
			if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(mousePos)) {
				result = true;
			}
		}
		return result;
	}

}