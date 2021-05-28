using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private int damageStrength;

    private int hitPoints;
    private Coroutine damageCoroutine;

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            if (hitPoints <= 0)
            {
                KillCharacter();
                break;
            }
            else
            {
                hitPoints -= damage;
            }

            if (interval > 0)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(K.player))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 1.0f));
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(K.player))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

}
