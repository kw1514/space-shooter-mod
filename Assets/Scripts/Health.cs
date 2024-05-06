using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public bool isPlayer;
    [SerializeField] public bool isEnemy;
    [SerializeField] bool isPowerUp;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] ParticleSystem powerupEffect;
    [SerializeField] ParticleSystem dieEffect;

    [SerializeField] bool applyCameraShake;

    static int constHealth;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    Player player;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        // saves the total health of the player in a constant variable so 
        // IncreaseHealth does not go over the total player health
        if (isPlayer)
        {
            constHealth = health;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null && !isPowerUp)
        {
            // checks if the shield is active
            if (player.shieldEnabled)
            {
                // shield is active but enemy should still take damage
                if (isEnemy && !isPlayer)
                {
                    TakeDamage(damageDealer.GetDamage());
                }
                // shield is active so player should not take damage
                else if (isPlayer && !isEnemy)
                {
                    TakeDamage(0);
                }
            }
            else if (!player.shieldEnabled)
            {
                TakeDamage(damageDealer.GetDamage());
                ShakeCamera();
            }

            PlayHitEffect();
            audioPlayer.PlayExplosionClip();
            damageDealer.Hit();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public void IncreaseHealth(int heal)
    {
        health += heal;
        health = Mathf.Clamp(health, 0, constHealth);
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isPlayer && !isPowerUp)
        {
            scoreKeeper.ModifyScore(score);
            audioPlayer.PlayEnemyDieClip();
            PlayDieEffect();
            Destroy(gameObject);
        }
        else if (isPlayer && !isPowerUp)
        {
            levelManager.LoadGameOver();
            audioPlayer.PlayPlayerDieClip();
            PlayDieEffect();
            Destroy(gameObject);
        }
        else
        {
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    public void PlayPowerupEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(powerupEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    public void PlayDieEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
}
