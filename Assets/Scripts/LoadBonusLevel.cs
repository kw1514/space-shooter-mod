using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadBonusLevel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bonusText; 
    [SerializeField] float durationTillBonus = 120f;
    [SerializeField] float delay = 2f;

    [SerializeField] GameObject enemySpawner;

    TimeKeeper timeKeeper;
    LevelManager levelManager;

void Awake()
    {
        timeKeeper = FindObjectOfType<TimeKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Start()
    {
        bonusText.text = "";
    }

    void Update()
    {
        if (timeKeeper.GetTimeInSeconds() > durationTillBonus)
        {
            enemySpawner.SetActive(false);
            bonusText.text = "Bonus Level Unlocked!";

            if (timeKeeper.GetTimeInSeconds() > durationTillBonus + delay)
            {
                levelManager.LoadBonusLevel();
            }
        }
    }
}
