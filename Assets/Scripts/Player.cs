using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    [Header("Triple Shot")]
    [SerializeField] public bool tripleShotEnabled = false;
    [SerializeField] float tripleShotDuration = 8f;
    private static float startTripleShotDuration;

    [Header("Shield")]
    [SerializeField] GameObject shield;
    [SerializeField] public bool shieldEnabled = false;
    [SerializeField] float shieldDuration = 5f;
    private static float startShieldDuration;

    Vector2 minBounds;
    Vector2 maxBounds;
    Vector2 rawInput;

    Shooter shooter;

    //private GameObject shield;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        InitBounds();

        // saves the initial duration so the duration can be reset
        startTripleShotDuration = tripleShotDuration;
        startShieldDuration = shieldDuration;

        // This returns the GameObject named shield,
        // which is a child of Player.
        //shield = GameObject.Find("Player1/Shield");
        shield.SetActive(false);
    }

    void Update()
    {
        Move();

        // controls the duration of the triple shot
        if (tripleShotEnabled)
        {
            tripleShotDuration -= Time.deltaTime;

            if (tripleShotDuration <= 0f)
            {
                tripleShotEnabled = false;

                // resets the duration
                tripleShotDuration = startTripleShotDuration;
            }
        }

        // controls the duration of the shield
        if (shieldEnabled)
        {
            shield.SetActive(true);
            shieldDuration -= Time.deltaTime;

            if (shieldDuration <= 0f)
            {
                shieldEnabled = false;
                shield.SetActive(false);

                // resets the duration
                shieldDuration = startShieldDuration;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag == "Player" && other.gameObject.tag == "Shield")
        {
            shieldEnabled = true;
        }
        else if (gameObject.tag == "Player" && other.gameObject.tag == "Triple Shot")
        {
            tripleShotEnabled = true;
        }
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
