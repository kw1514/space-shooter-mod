using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;
    [SerializeField] Vector2 fastMoveSpeed;

    [Header("Background")]
    [SerializeField] float duration = 45f;

    private GameObject planets;
    private GameObject planets1;

    Vector2 offset;
    Material material;
    TimeKeeper timeKeeper;
    UIDisplay uiDisplay;

    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        timeKeeper = FindObjectOfType<TimeKeeper>();
        uiDisplay = FindObjectOfType<UIDisplay>();
    }

    private void Start() 
    {
        planets = GameObject.FindGameObjectWithTag("Planets");
        planets1 = GameObject.FindGameObjectWithTag("Planets1");

        if (planets != null && planets1 != null)
        {
            planets.SetActive(true);
            planets1.SetActive(false); 
        }     
    }

    void Update()
    {
        offset = moveSpeed * Time.deltaTime;

        if (timeKeeper.GetTimeInSeconds() > duration - 3 && timeKeeper.GetTimeInSeconds() < duration + 2)
        {
            offset = fastMoveSpeed * Time.deltaTime;
        }

        if( timeKeeper.GetTimeInSeconds() > duration && duration != 0f && planets != null && planets1 != null)
        {
            planets.SetActive(false);
            planets1.SetActive(true);
        }

        material.mainTextureOffset += offset;
    }
}
