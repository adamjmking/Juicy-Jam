using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenBehavior : MonoBehaviour
{
    public Sprite[] sprites;
    public Image image;

    // Update is called once per frame
    void Update()
    {
        image.sprite = sprites[Mathf.FloorToInt((sprites.Length * Time.timeSinceLevelLoad) % sprites.Length)];
    }
}
