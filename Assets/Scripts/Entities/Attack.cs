using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack {
    public int id;
    public int damage;
    public float duration;

    public Attack (int pId, int pDamage, float pDuration)
    {
        id = pId;
        damage = pDamage;
        duration = pDuration;
    }

    public Attack (Attack copy)
    {
        id = copy.id;
        damage = copy.damage;
        duration = copy.duration;
    }
}
