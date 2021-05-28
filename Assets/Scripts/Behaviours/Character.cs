using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public int startingHitPoints;
    public int maxHitPoints;

    public abstract IEnumerator DamageCharacter(int damage, float interval);

    public virtual void KillCharacter()
    {
        Destroy(gameObject);
    }

}
