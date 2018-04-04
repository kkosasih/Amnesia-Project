using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect {

    public static StatusEffect empty = new StatusEffect(0, 0, 0); // Represents an empty status effect

    public float duration; // how long the status effect lasts
    public float repeatTime; // how much time passes between each damage tick
    public int damage; // how much damage the status effect inflicts

    public StatusEffect(int dam, float repeat, float dur)
    {
        damage = dam;
        duration = dur;
        repeatTime = repeat;
    }
}
