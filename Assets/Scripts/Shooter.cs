using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject tripleLaserPrefab;

    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float basefiringRate = .2f;

    [Header("AI")]
    [SerializeField] bool useAI;  // tells the script whether to listen to the player or
                                  // automatically fire for the enemy
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;
    Player player;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject instance = null;

            // the enemy should always be shooting normal(single) lasers
            if (useAI)
            {
                instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            }
            // the player has the ability to shoot single or triple lasers
            else if (!useAI)
            {
                // checks to see which lasers should be fired, regular or triple
                if (player.tripleShotEnabled)
                {
                    instance = Instantiate(tripleLaserPrefab, transform.position, Quaternion.identity);
                }
                else if (!player.tripleShotEnabled)
                {
                    instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                }
            }

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            Destroy(instance, projectileLifetime);

            float timeToNextProjectile = Random.Range(basefiringRate - firingRateVariance,
                                                      basefiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
