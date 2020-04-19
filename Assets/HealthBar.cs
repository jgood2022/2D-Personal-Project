using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float health;
    public Image green;
    public SwordsmanController player;
    [HideInInspector] public float maxHealth, greenScale;

    private void Start()
    {
        maxHealth = health;
        greenScale = green.GetComponent<RectTransform>().rect.width;
    }

    public void ChangeHealthBar(float damage)
    {
        health -= damage;

        if (health > maxHealth) health = maxHealth;

        if (health <= 0)
        {
            health = 0;
            player.Die();
        }
        green.GetComponent<RectTransform>().sizeDelta = new Vector2(health / maxHealth * greenScale, green.GetComponent<RectTransform>().sizeDelta.y);


    }
}
