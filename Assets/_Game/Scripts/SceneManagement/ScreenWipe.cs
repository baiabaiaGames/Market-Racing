using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ScreenWipe : MonoBehaviour {

	[SerializeField][Range (0.1f, 3f)] private float wipeSpeed = 1f;
	private enum WipeMode { NotBlocked, WipingNotBlocked, Blocked, WipingToBlocked }
	private WipeMode wipeMode = WipeMode.NotBlocked;
	public bool isDone { get; private set; }

	Animator transitionAnim;

	private void Awake () {
		transitionAnim = GetComponentInChildren<Animator> ();
		DontDestroyOnLoad (gameObject);

		transitionAnim.SetFloat ("Time", wipeSpeed);
	}

	public void ToggleWipe (bool blockScreen) {
		isDone = false;
		if (blockScreen)
			wipeMode = WipeMode.WipingToBlocked;
		else
			wipeMode = WipeMode.WipingNotBlocked;
	}

	private void Update () {
		switch (wipeMode) {
			case WipeMode.WipingToBlocked:
				StartCoroutine (WipeToBlocked ());
				break;
			case WipeMode.WipingNotBlocked:
				StartCoroutine (WipeToNotBlocked ());
				break;
		}
	}

	private IEnumerator WipeToBlocked () {
		transitionAnim.SetBool ("Start", true);
		yield return new WaitForSeconds (transitionAnim.GetCurrentAnimatorStateInfo (0).length + transitionAnim.GetCurrentAnimatorStateInfo (0).length / 2f);
		isDone = true;
		wipeMode = WipeMode.Blocked;
		yield return new WaitForSeconds (transitionAnim.GetCurrentAnimatorStateInfo (0).length + transitionAnim.GetCurrentAnimatorStateInfo (0).length);
		transitionAnim.SetBool ("Start", false);
	}

	private IEnumerator WipeToNotBlocked () {

		yield return new WaitForSeconds (transitionAnim.GetCurrentAnimatorStateInfo (0).length + transitionAnim.GetCurrentAnimatorStateInfo (0).length);
		isDone = true;
		wipeMode = WipeMode.NotBlocked;
	}

	[ContextMenu ("Block")] private void Block () { ToggleWipe (true); }

	[ContextMenu ("Clear")] private void Clear () { ToggleWipe (false); }
}