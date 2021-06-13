using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int health;

    IEnumerator DamageEffect(Color dmgColor, float duration)
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Color originColor = sr.color;

        sr.color = dmgColor;

        for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
        {
            sr.color = Color.Lerp(dmgColor, originColor, t);

            yield return null;
        }

        sr.color = originColor;
    }
    public void DealDamage(int damage)
    {
        health -= damage;
        StartCoroutine(DamageEffect(Color.red, 0.2f));
    }

    void Update()
    {
        if (health <= 0)
        {
            StartCoroutine(DamageEffect(Color.red, 0.5f));
            Destroy(gameObject, 0.5f);
        }
    }
}
