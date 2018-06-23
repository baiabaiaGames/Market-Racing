using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameSettings gameSettings;
	public GameEvent onGameStart;
	public GameEvent onGamePlaying;
	public GameEvent onGameReset;

	public bool isPlaying { private set; get; }

	#region  Singleton
	public static GameManager instance;
	private static GameManager _instance;

	private void Awake () {
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad (this.gameObject);
	}
	#endregion /Singleton

	void Start () {
		GameStartRaiseEvent ();
	}

	public void IsPlaying (bool playing) {
		isPlaying = playing;
	}

	public void GameStartRaiseEvent () {
		if (onGameStart)
			onGameStart.Raise ();
		else
			Debug.Log ("You haven´t set  the On Game Start Variable");
	}

	public void GamePlayingRaiseEvent () {
		if (onGamePlaying)
			onGamePlaying.Raise ();
		else
			Debug.Log ("You haven´t set  the On Game Playing Variable");
	}

	public void GameResetRaiseEvent () {
		if (onGameReset)
			onGameStart.Raise ();
		else
			Debug.Log ("You haven´t set  the On Game Reset Variable");
	}
}