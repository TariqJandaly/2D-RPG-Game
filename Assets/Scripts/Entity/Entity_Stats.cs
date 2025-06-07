using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defense;

    public float GetElementalDamage(out ElementType element, float scaleFactor = 1)
    {
        float fireDamage = offense.fireDamage.GetValue();
        float iceDamage = offense.iceDamage.GetValue();
        float lightningDamage = offense.lightningDamage.GetValue();
        float bonusDamage = major.intelligence.GetValue() * 1f;

        float highestElementalDamage = fireDamage;
        element = ElementType.Fire;

        if (iceDamage > highestElementalDamage)
        {
            highestElementalDamage = iceDamage;
            element = ElementType.Ice;
        }

        if (lightningDamage > highestElementalDamage)
        {
            highestElementalDamage = lightningDamage;
            element = ElementType.Lightning;
        }

        if (highestElementalDamage <= 0f)
        {
            highestElementalDamage = 0f;
            element = ElementType.None;
        }

        float bonusElementalDamageMultiplier = 0.5f;
        
        float bonusFire = (fireDamage == highestElementalDamage) ? 0f : fireDamage * bonusElementalDamageMultiplier;
        float bonusIce = (iceDamage == highestElementalDamage) ? 0f : iceDamage * bonusElementalDamageMultiplier;
        float bonusLightning = (lightningDamage == highestElementalDamage) ? 0f : lightningDamage * bonusElementalDamageMultiplier;

        float weakerElementsDamage = bonusFire + bonusIce + bonusLightning;

        float finalDamage = highestElementalDamage + weakerElementsDamage + bonusDamage;
        
        return finalDamage * scaleFactor;
    }

    public float GetElementalResistance(ElementType element)
    {
        float baseResistance = 0;
        float bonusResistance = major.intelligence.GetValue() * 0.5f;

        if (element == ElementType.Fire)
            baseResistance = defense.fireResistance.GetValue();

        if (element == ElementType.Ice)
            baseResistance = defense.iceResistance.GetValue();

        if (element == ElementType.Lightning)
            baseResistance = defense.lightningResistance.GetValue();

        float resistance = baseResistance + bonusResistance;
        float resistanceCap = 75f;
        float finalResistance = Mathf.Clamp(resistance, 0f, resistanceCap) / 100f;

        return finalResistance;
    }

    public float GetPhysicalDamage(out bool isCrit, float scaleFactor = 1)
    {
        float baseDamage = offense.damage.GetValue();
        float bonusDamage = major.strength.GetValue() * 0.5f;
        float totalBaseDamage = baseDamage + bonusDamage;

        float baseCritChance = offense.critChance.GetValue();
        float bonusCritChance = major.agility.GetValue() * 0.3f;
        float critChance = baseCritChance + bonusCritChance;

        float baseCritDamage = offense.critDamage.GetValue();
        float bonusCritDamage = major.strength.GetValue() * 0.5f;
        float critDamage = (baseCritDamage + bonusCritDamage) / 100f; // Turn into a multiplier

        isCrit = Random.Range(0f, 100f) < critChance;
        float finalDamage = isCrit ? totalBaseDamage * (1 + critDamage) : totalBaseDamage;

        return finalDamage;
    }

    public float GetArmorMitigation(float armorReduction)
    {
        float baseArmor = defense.armor.GetValue();
        float bonusArmor = major.vitality.GetValue() * 1f;
        float totalArmor = baseArmor + bonusArmor;

        float reductionMultiplier = Mathf.Clamp(1f - armorReduction, 0f, 1f);
        float effectiveArmor = totalArmor * reductionMultiplier;
        float mitigation = effectiveArmor / (totalArmor + 100f);

        float mitigationCap = 0.95f;
        float finalMitigation = Mathf.Clamp(mitigation, 0f, mitigationCap);

        return finalMitigation;
    }

    public float GetArmorReduction()
    {

        float finalReduction = offense.armorReduction.GetValue() / 100f;

        return finalReduction;
    }
    
    public float GetEvasion()
    {
        float baseEvasion = defense.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * 0.5f;

        float totalEvasion = baseEvasion + bonusEvasion;
        float evasionCap = 85f;

        float finalEvasion = Mathf.Clamp(totalEvasion, 0f, evasionCap);

        return finalEvasion;
    }
    public float GetMaxHealth()
    {
        float baseMaxHealth = maxHealth.GetValue();
        float bonusMaxHealth = major.vitality.GetValue() * 5f;

        float finalMaxHealth = baseMaxHealth + bonusMaxHealth;

        return finalMaxHealth;
    }

}