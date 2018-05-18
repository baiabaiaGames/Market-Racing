    using System.Collections.Generic;
    using System;
    using GooglePlayGames.BasicApi.SavedGame;
    using GooglePlayGames.BasicApi;
    using GooglePlayGames;
    using UnityEngine;

    /// <summary>
    /// Manage the Google Play services configuration
    /// Sign in the player google play games account
    /// Unlock Achivements
    /// Increment Achivements progress
    /// Show the player Achivements
    /// Add abd show the player score into the google play games leaderboard
    /// </summary>
    public class PlayGamesManager : MonoBehaviour {

        public void Awake () {
            SignIn ();
        }

        // Sign in the player´s google play games account into the game
        void SignIn () {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ().Build ();
            PlayGamesPlatform.InitializeInstance (config);
            PlayGamesPlatform.Activate ();

            Social.localUser.Authenticate ((bool success) => {
                if (success) {
                    Debug.Log ("SignIn successful");
                } else {
                    Debug.Log ("SignIn failed");
                }
            });
        }

        #region Achivements
        // Unlock an achivement
        public static void UnlockAchivement (string id) {

            // To unlock an achivement we need to pass the achivement id
            // the progress and a callback
            Social.ReportProgress (id, 100, success => { });
        }

        // Increment the progress in an Incremental Achivement
        public static void IncrementAchivement (string id, int stepsToIncrement) {

            // To increment the progress we need to pass the achivement id
            // the amount we want to increment and a callback
            PlayGamesPlatform.Instance.IncrementAchievement (id, stepsToIncrement, success => { });
        }

        // Show´s the Achivements
        public static void ShowAchivementUI () {
            Social.ShowAchievementsUI ();
        }
        #endregion /Achivements

        #region Leaderboards

        public static void AddScoreToLeaderBoard (string leaderboardId, long score) {

            // To add the new score to de leaderboard we need to pass
            // the score, the leaderboard ID and a callback
            Social.ReportScore (score, leaderboardId, success => { });
        }

        // Show´s the Leaderboard
        public static void ShowLeaderboardUI () {
            Social.ShowLeaderboardUI ();
        }
        #endregion /Leaderboards
    }