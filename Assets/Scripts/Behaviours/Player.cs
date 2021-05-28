using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    [HideInInspector] public Action<int> onPickUp;
    [HideInInspector] public Action<int> onHPChange;

    [SerializeField] private HitPoints hitPoints;
    [SerializeField] private int heartCount;

    private void OnEnable()
    {
        hitPoints.value = startingHitPoints;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(K.TagKey.consumable))
        {
            heartCount++;
            onPickUp?.Invoke(heartCount);
            collision.gameObject.SetActive(false);
        }
    }

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            if (hitPoints.value <= 0)
            {
                KillCharacter();
                break;
            }
            else
            {
                hitPoints.value -= damage;
                onHPChange?.Invoke(hitPoints.value);
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

    public override void KillCharacter()
    {
        base.KillCharacter();
        PlayerPrefs.SetInt(K.score, heartCount);
        SceneManager.LoadScene(2);
    }

}
