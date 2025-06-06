using System;
using UnityEngine;

[Serializable]
public class Stat_OffenseGroup
{
    // Physical damage
    public Stat damage; // Base physical damage
    public Stat critDamage; // Critical hit damage multiplier
    public Stat critChance; // Critical hit chance
    public Stat armorReduction; // Armor reduction percentage

    // Elemental damage
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;
}
