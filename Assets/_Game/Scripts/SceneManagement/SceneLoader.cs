using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneLoader : MonoBehaviour {

	[SerializeField] public string loaderScene;
	[SerializeField] public string menuScene;
	[SerializeField] public string gameScene;
	[SerializeField] public float delay;
	private ScreenWipe screenWipe;
	private int nextLevelIndex;

	IEnumerator loadSceneCoroutine;

	public static SceneLoader instance;
	private static SceneLoader _instance;

	private void Awake () {
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad (this.gameObject);

		screenWipe = FindObjectOfType<ScreenWipe> ();
		LoadScene ();
	}

	public void LoadScene () {
		if (loadSceneCoroutine != null)
			StopCoroutine (loadSceneCoroutine);

		loadSceneCoroutine = LoadSceneCoroutine (menuScene, gameScene);
		StartCoroutine (loadSceneCoroutine);
	}

	public IEnumerator LoadSceneCoroutine (string menuSceneName, string gameSceneName) {
		float d = 0;
		while (d < delay) {
			d += Time.deltaTime;
			yield return null;
		}

		screenWipe.ToggleWipe (true);

		while (!screenWipe.isDone)
			yield return null;

		AsyncOperation operation = SceneManager.LoadSceneAsync (menuSceneName);
		AsyncOperation gameOperation = SceneManager.LoadSceneAsync (gameSceneName, LoadSceneMode.Additive);
		while (!operation.isDone && !gameOperation.isDone)
			yield return null;

		screenWipe.ToggleWipe (false);
	}
}