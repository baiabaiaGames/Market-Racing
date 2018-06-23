using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	private float score = 0.0f;
	private float delta;

	public PlayerControllerData playerControllerData;

	public TMPro.TextMeshProUGUI scoreText;
	public TMPro.TextMeshProUGUI multiplierText;

	private void Start () {
		if (!scoreText)
			Debug.Log ("Please set the score text variable");
		if (!multiplierText)
			Debug.Log ("Please set the multiplier text variable");
	}

	void Update () {

		delta = Time.deltaTime;

		if (!GameManager.instance.isPlaying)
			return;

		IncreaseScore ();
		IncreaseMultiplier ();
	}

	private void IncreaseScore () {
		score += delta * playerControllerData.multiplier;
		scoreText.text = ((int) score).ToString ();
	}

	private void IncreaseMultiplier () {
		multiplierText.text = "X" + playerControllerData.multiplier.ToString ("f1");
	}
}