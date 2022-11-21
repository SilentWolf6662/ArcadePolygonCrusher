using System;
using System.Collections;
using PolygonCrosser;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI startText;
    [SerializeField] private float time = 120;

    private void Start()
    {
        startText.enabled = true;
        Time.timeScale = 0;
        StartCoroutine(ETimer());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            startText.enabled = false;
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene((int)Scenes.Gameplay);
        timerText.text = $"Time: {time}";
        if (time <= 0) DashMove.Die();
    }

    private IEnumerator ETimer()
    {
        yield return new WaitForSeconds(1);
        time -= 1;
        StartCoroutine(ETimer());
    }
}
