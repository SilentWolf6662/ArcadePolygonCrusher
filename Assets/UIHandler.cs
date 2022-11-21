using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText, highscoreText;
    private int score, highscore;
    private void Update()
    {
        if (score > PlayerPrefs.GetInt("highscore")) PlayerPrefs.SetInt("highscore", score);
        highscore = PlayerPrefs.GetInt("highscore");
        highscoreText.text = $"Highscore: {highscore}";
        score = PointHandler.GetPoints();
        scoreText.text = $"Score: {score}";
    }
}
