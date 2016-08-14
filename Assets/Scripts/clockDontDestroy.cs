using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class clockDontDestroy : MonoBehaviour {


	private static clockDontDestroy _instance = null;
	public static clockDontDestroy Instance
		{
			get { return _instance; }
		}

		void Awake()
		{



			if (_instance != null && _instance != this)
			{
				Destroy(transform.root.gameObject);
				return;
			}

			_instance = this;

			DontDestroyOnLoad(transform.root.gameObject);
		}
	}

