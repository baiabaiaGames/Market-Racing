using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu : MonoBehaviour {
	[Space (10f)][Header ("Menu GameObjects")]
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject options;
	[SerializeField] private GameObject exitMenu;

	[Space (10f)][Header ("Options GameObjects")]
	[SerializeField] private GameObject audioMenu;
	[SerializeField] private GameObject controlsMenu;
	[SerializeField] private GameObject lenguageMenu;

	[Space (10f)][Header ("Shop GameObjects")]
	[SerializeField] private GameObject shopMenu;

	void Awake () {
		exitMenu.SetActive (false);
	}

	void Update () {
#if UNITY_EDITOR
		if (Input.GetKeyDown ("escape") && exitMenu.activeInHierarchy == false) {
			exitMenu.SetActive (true);
		} else if (Input.GetKeyDown ("escape") && exitMenu.activeInHierarchy == true) {
			exitMenu.SetActive (false);
		}
#endif
	}

	public void Quit () {
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#else
		Application.Quit ();
#endif
	}
}