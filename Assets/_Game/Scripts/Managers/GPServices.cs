	using UnityEngine.UI;
	using UnityEngine;

	public class GPServices : MonoBehaviour {

		public static GPServices Instance { get; private set; }

		private void Start () {
			Instance = this;
		}

		// Increment the the steps of the achivement to Unlock
		public void Increment (string achivementName, int amount) {
			PlayGamesManager.IncrementAchivement (achivementName, amount);
		}

		// Unlock an Achivement
		public void Unlock (string achivementName) {
			//Pass the Achivement you want to Unlock from the GPSIds Script
			PlayGamesManager.UnlockAchivement (achivementName);
		}

		// Show Google Play Games Achivements in the UI
		public void ShowAchivements () {
			PlayGamesManager.ShowAchivementUI ();
		}

		// Show Google Play Games Leaderboard in the UI
		public void ShowLeaderboards () {
			PlayGamesManager.ShowLeaderboardUI ();
	}
}