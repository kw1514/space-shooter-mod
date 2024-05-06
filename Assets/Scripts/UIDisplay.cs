using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Time")]
    [SerializeField] TextMeshProUGUI timeText;

    ScoreKeeper scoreKeeper;
    TimeKeeper timeKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        timeKeeper = FindObjectOfType<TimeKeeper>();
    }
    
    void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
        scoreText.text = "Start";
        timeText.text = "Start";
    }

    void Update()
    {
        healthSlider.value = playerHealth.GetHealth();
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
        timeText.text = timeKeeper.GetTime().ToString(@"mm\:ss");
        //timeText.text = timeKeeper.GetTimeInSeconds().ToString("000000000");
    }
}
