using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		if (onGameStart != null)
			onGameStart.Raise ();
	}

	public void IsPlaying (bool playing) {
		isPlaying = playing;
	}
}