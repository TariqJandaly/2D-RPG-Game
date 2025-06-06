using System;
using UnityEngine;

[Serializable]
public class Stat_DefenseGroup
{
    // Physical defense
    public Stat armor; // Base physical defense
    public Stat evasion; // Evasion chance against physical attacks

    // Elemental defense
    public Stat fireResistance;
    public Stat iceResistance;
    public Stat lightningResistance;
}