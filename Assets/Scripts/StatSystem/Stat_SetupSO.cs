using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Default Stat Setup", fileName = "Default Stat Setup")]
public class Stat_SetupSO : ScriptableObject
{
    [Header("Major Attributes")]
    public float strength;
    public float agility;
    public float intelligence;
    public float vitality;

    [Header("Resources")]
    public float maxHealth = 100;
    public float healthRegen;
    public float maxMana = 100;
    public float manaRegen;
    public float maxStamina = 100;
    public float staminaRegen;

    [Header("Offense Group")]
    public float attackSpeed = 1;
    public float damage = 10;
    public float critDamage = 20;
    public float critChance = 5;
    public float armorReduction;
    public float fireDamage;
    public float iceDamage;
    public float lightningDamage;

    [Header("Defense Group")]
    public float armor;
    public float evasion;
    public float fireResistance;
    public float iceResistance;
    public float lightningResistance;

}