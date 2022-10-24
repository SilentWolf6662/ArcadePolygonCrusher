using System.Collections;
using UnityEngine;
// ReSharper disable IteratorNeverReturns
namespace PolygonCrosser.Scoreboard
{
	public class DisplayHighscores : MonoBehaviour 
	{
		public TMPro.TextMeshProUGUI[] rNames;
		public TMPro.TextMeshProUGUI[] rScores;
		private Highscores myScores;

		private void Start() //Fetches the Data at the beginning
		{
			for (int i = 0; i < rNames.Length;i ++) rNames[i].text = $"{i + 1}. Fetching...";
			myScores = GetComponent<Highscores>();
			StartCoroutine(nameof(RefreshHighscores));
		}
		public void SetScoresToMenu(PlayerScore[] highscoreList) //Assigns proper name and score for each text value
		{
			for (int i = 0; i < rNames.Length;i ++)
			{
				rNames[i].text = $"{i + 1}. "; 
				if (highscoreList.Length <= i) continue;
				rScores[i].text = highscoreList[i].score.ToString();
				rNames[i].text = highscoreList[i].username;
			}
		}
		private IEnumerator RefreshHighscores() //Refreshes the scores every 30 seconds
		{
			while(true)
			{
				myScores.DownloadScores();
				yield return new WaitForSeconds(30);
			}
		}
	}
}
