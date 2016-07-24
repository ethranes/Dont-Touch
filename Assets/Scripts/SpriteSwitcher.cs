using UnityEngine;
using System.Collections;

public class SpriteSwitcher : MonoBehaviour {

	public Sprite sprite1; // Drag your first sprite here
	public Sprite sprite2; // Drag your second sprite here

	private SpriteRenderer spriteRenderer; 

	void Start ()
	{
		//this.gameObject.GetComponent<SpriteRenderer> ().sprite = sprite1;
		spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
		if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
			spriteRenderer.sprite = sprite1; // set the sprite to sprite1
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) // If the space bar is pushed down
		{
			ChangeTheDarnSprite (); // call method to change sprite
		}
	}

	void ChangeTheDarnSprite ()
	{
		if (spriteRenderer.sprite == sprite1) // if the spriteRenderer sprite = sprite1 then change to sprite2
		{
			spriteRenderer.sprite = sprite2;
		}
		else
		{
			spriteRenderer.sprite = sprite1; // otherwise change it back to sprite1
		}
	}
}