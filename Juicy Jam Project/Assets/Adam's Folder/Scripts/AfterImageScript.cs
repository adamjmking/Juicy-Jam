using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageScript : MonoBehaviour
{
    //Components
    private Transform player;
    private SpriteRenderer SR, playerSR;

    //Colors
    private Color color;

    //Variables
    [SerializeField] private float alphaSet = 0.8f;
    [SerializeField] private float alphaDelta = 0.85f;
    private float alpha;
    [SerializeField] private float activeTime = 0.1f;
    private float timeActivated;

    private void OnEnable()
    {
        //Get components
        SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSR = player.GetComponent<SpriteRenderer>();

        //Initializations
        alpha = alphaSet;
        SR.sprite = playerSR.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActivated = Time.time;
    }

    private void Update()
    {
        alpha -= (1/alphaDelta) * Time.deltaTime; //Updates the alpha
        color = new Color(1f, 1f, 1f, alpha);
        SR.color = color; //Sets the color

        if (Time.time >= (timeActivated + activeTime))
        {
            AfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
