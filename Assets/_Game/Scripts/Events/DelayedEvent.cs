using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedEvent : MonoBehaviour {

	public GameEvent targetEvent;
	bool isWaiting;

	public void Raise (float timer) {
		if (targetEvent == null)
			return;

		if (isWaiting)
			return;

		isWaiting = true;
		StartCoroutine (DelayedRaise (timer));
	}

	public IEnumerator DelayedRaise (float t) {
		yield return new WaitForSeconds (t);
		targetEvent.Raise ();
		isWaiting = false;
	}
}