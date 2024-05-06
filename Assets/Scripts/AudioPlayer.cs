using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Explosion")]
    [SerializeField] AudioClip explosionClip;
    [SerializeField] [Range(0f, 1f)] float explosionVolume = 1f;

    [Header("PowerUps")]
    [SerializeField] AudioClip powerUpClip;
    [SerializeField] [Range(0f, 1f)] float powerUpVolume = 1f;

    [Header("Shield")]
    [SerializeField] AudioClip shieldClip;
    [SerializeField] [Range(0f, 1f)] float shieldVolume = 1f;

    [Header("Player Die")]
    [SerializeField] AudioClip playerDieClip;
    [SerializeField] [Range(0f, 1f)] float playerDieVolume = 1f;

    [Header("Enemy Die")]
    [SerializeField] AudioClip EnemyDieClip;
    [SerializeField] [Range(0f, 1f)] float EnemyDieVolume = 1f;


    static AudioPlayer instance;

    void Awake() 
    {
        ManageSingleton();
    }

    void ManageSingleton() // makes sure that the audio plays throughout the scenes
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayExplosionClip()
    {
        PlayClip(explosionClip, explosionVolume);
    }

    public void PlayPowerUpClip()
    {
        PlayClip(powerUpClip, powerUpVolume);
    }

    public void PlayShieldClip()
    {
        PlayClip(shieldClip, shieldVolume);
    }


    public void PlayPlayerDieClip()
    {
        PlayClip(playerDieClip, playerDieVolume);
    }

    public void PlayEnemyDieClip()
    {
        PlayClip(EnemyDieClip, EnemyDieVolume);
    }


    void PlayClip(AudioClip clip, float volume)
    {
        if(clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
