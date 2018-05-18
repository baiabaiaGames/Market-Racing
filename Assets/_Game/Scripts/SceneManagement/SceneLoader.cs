using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneLoader : MonoBehaviour {

	[SerializeField] public string menuScene;
	[SerializeField] public string gameScene;
	[SerializeField] public float delay;
	private ScreenWipe screenWipe;
	private int nextLevelIndex;

	IEnumerator loadSceneCoroutine;

	private void Awake () {
		screenWipe = FindObjectOfType<ScreenWipe> ();
		DontDestroyOnLoad (gameObject);

		LoadScene ();
	}

	public void LoadScene () {
		if (loadSceneCoroutine != null)
			StopCoroutine (loadSceneCoroutine);

		loadSceneCoroutine = LoadSceneCoroutine (menuScene, gameScene);
		StartCoroutine (loadSceneCoroutine);
	}

	public IEnumerator LoadSceneCoroutine (string menu, string game) {
		float d = 0;
		while (d < delay) {
			d += Time.deltaTime;
			yield return null;
		}

		screenWipe.ToggleWipe (true);

		while (!screenWipe.isDone)
			yield return null;

		AsyncOperation operation = SceneManager.LoadSceneAsync (menu);
		AsyncOperation gameOperation = SceneManager.LoadSceneAsync (game, LoadSceneMode.Additive);
		while (!operation.isDone && !gameOperation.isDone)
			yield return null;

		screenWipe.ToggleWipe (false);
	}
}