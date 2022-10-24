#region Copyright Notice
// ******************************************************************************************************************
// 
// Assembly-CSharp.DataSave.cs © Shadow Wolf Development (SilentWolf6662 & Bambinidk) - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// 
// This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivs 3.0 Unported License.
// To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/3.0/
// 
// Created & Copyrighted @ 2022-10-24
// 
// ******************************************************************************************************************
#endregion
using TMPro;
using UnityEngine;
namespace PolygonCrosser.Scoreboard
{
	public class DataSave : MonoBehaviour
	{
		[SerializeField] private TMP_InputField playerNameField;
		[SerializeField] private TextMeshProUGUI score;
		[SerializeField] private PlayerScore playerScore;
		private void Update()
		{
			playerScore.score = PointHandler.GetPoints();
			score.text = $"SCORE: {PlayerPrefs.GetInt("highscore")}";
		}
		public void SendScore()
		{
			if (playerScore.score <= PlayerPrefs.GetInt("highscore")) return;
			PlayerPrefs.SetInt("highscore", playerScore.score);
			Highscores.UploadScore(playerNameField.text, playerScore.score);
		}
	}
}
