using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static int health = 3;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyheart;

    private void Awake()
    {
        health = 3;
    }
    // Update is called once per frame
    void Update()
    {
        foreach (Image img in hearts)
        {
            img.sprite = emptyheart;
        }
        for (int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }
}
