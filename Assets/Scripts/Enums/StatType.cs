using System;
using UnityEngine;

public enum StatType
{
    // Resource Group
    MaxHealth,
    HealthRegen,
    
    MaxMana,
    ManaRegen,

    MaxStamina,
    StaminaRegen,

    // Offense Group
    AttackSpeed,

    Damage,          // Base physical damage
    CritDamage,      // Critical hit damage multiplier
    CritChance,      // Critical hit chance
    ArmorReduction,  // Armor reduction percentage

    FireDamage,
    IceDamage,
    LightningDamage,

    // Major Attributes
    Strength,
    Agility, 
    Intelligence, 
    Vitality,        // Vitality increases max health by 5 per point

    // Defense Group
    Armor,          // Base physical defense
    Evasion,        // Evasion chance against physical attacks

    FireResistance,
    IceResistance,
    LightningResistance
}
