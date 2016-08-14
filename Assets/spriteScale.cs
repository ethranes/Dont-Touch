using UnityEngine;
using System.Collections;

public class spriteScale : MonoBehaviour {

	public float width = 1;
	public float height = 1;
	public Vector3 position = new Vector3( 313, 484, 0 );

	void Awake()
	{
		// set the scaling
		Vector3 scale = new Vector3( width, height, 1f );
		transform.localScale = scale;
		// set the position
		transform.position = position;
	}
}