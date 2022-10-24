using System.Collections;
using UnityEngine;
#pragma warning disable CS0618
namespace PolygonCrosser.Scoreboard
{
    public class Highscores : MonoBehaviour
    {
        private const string privateCode = "g03DQT9cXEG-15WKhA02Nw-_AvrBwC7kOG-VrLT0onJQ";  //Key to Upload New Info
        private const string publicCode = "6356759d8f40bb11d8c6c741";   //Key to download
        private const string webURL = "http://dreamlo.com/lb/"; //  Website the keys are for

        public PlayerScore[] scoreList;
        private DisplayHighscores myDisplay;

        private static Highscores instance; //Required for STATIC usability
        private void Awake()
        {
            instance = this; //Sets Static Instance
            myDisplay = GetComponent<DisplayHighscores>();
        }
    
        public static void UploadScore(string username, int score)  //CALLED when Uploading new Score to WEBSITE
            //STATIC to call from other scripts easily
            => instance.StartCoroutine(instance.DatabaseUpload(username,score)); //Calls Instance

        private IEnumerator DatabaseUpload(string userame, int score) //Called when sending new score to Website
        {
            WWW www = new WWW($"{webURL}{privateCode}/add/{WWW.EscapeURL(userame)}/{score}");
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                print("Upload Successful");
                DownloadScores();
            }
            else print($"Error uploading{www.error}");
        }

        public void DownloadScores() => StartCoroutine(nameof(DatabaseDownload));
        private IEnumerator DatabaseDownload()
        {
            //WWW www = new WWW(webURL + publicCode + "/pipe/"); //Gets the whole list
            WWW www = new WWW($"{webURL}{publicCode}/pipe/0/10"); //Gets top 10
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                OrganizeInfo(www.text);
                myDisplay.SetScoresToMenu(scoreList);
            }
            else print($"Error uploading{www.error}");
        }

        private void OrganizeInfo(string rawData) //Divides Scoreboard info by new lines
        {
            string[] entries = rawData.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
            scoreList = new PlayerScore[entries.Length];
            for (int i = 0; i < entries.Length; i ++) //For each entry in the string array
            {
                string[] entryInfo = entries[i].Split(new char[] {'|'});
                string username = entryInfo[0];
                int score = int.Parse(entryInfo[1]);
                scoreList[i] = new PlayerScore(username,score);
                print($"{scoreList[i].username}: {scoreList[i].score}");
            }
        }
    }

}