using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
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
        //string time = timeKeeper.GetTime().ToString();
        //string timeStr = String.Format("{0:m mm}", time); 
        string timeStr = timeKeeper.GetTime().ToString(@"mm\:ss");


        scoreText.text = "You Scored:\n" + scoreKeeper.GetScore();
        timeText.text = "You Survived:\n" + timeStr;
    }
}
