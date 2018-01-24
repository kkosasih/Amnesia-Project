using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack {
    public int id;          // The team that the attack comes from
    public int damage;      // The damage that the attack does
    public float duration;  // How long the attack lasts before fading

    // Constructor for each of the fields
    public Attack (int pId, int pDamage, float pDuration)
    {
        id = pId;
        damage = pDamage;
        duration = pDuration;
    }

    // A copy constructor
    public Attack (Attack copy)
    {
        id = copy.id;
        damage = copy.damage;
        duration = copy.duration;
    }
}
