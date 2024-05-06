using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [Header("Health Powerup")]
    [SerializeField] int heal = 50;

    [Header("Shield Powerup")]
    [SerializeField] GameObject shieldPrefab;

    [Header("Triple Shot Powerup")]
    [SerializeField] GameObject tripleShotLaser;

    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Health otherHealth = other.GetComponent<Health>();
        Health health = gameObject.GetComponent<Health>();
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (other.gameObject.tag == "Player" && gameObject.tag == "Health")
        {
            otherHealth.PlayPowerupEffect();
            otherHealth.IncreaseHealth(heal); 
            audioPlayer.PlayPowerUpClip();
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Player" && gameObject.tag == "Shield")
        {
            GameObject shield = gameObject;
            otherHealth.PlayPowerupEffect();
            //audioPlayer.PlayPowerUpClip();
            audioPlayer.PlayShieldClip();
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Player" && gameObject.tag == "Triple Shot")
        {
            otherHealth.PlayPowerupEffect();
            audioPlayer.PlayPowerUpClip();
            Destroy(gameObject);
        }
    }
}
