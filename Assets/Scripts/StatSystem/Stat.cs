using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private float baseValue;
    [SerializeField] private List<StatModifier> modifiers = new List<StatModifier>();

    private bool wasModified = true;
    private float finalValue;

    public float GetValue()
    {
        if (wasModified)
        {
            finalValue = GetFinalValue();
            wasModified = false;
        }

        return finalValue;
    }

    public void AddModifier(float value, string source, bool isPercentage = false)
    {
        StatModifier modToAdd = new StatModifier(value, source, isPercentage);
        modifiers.Add(modToAdd);
        wasModified = true;
    }

    public void RemoveModifier(string source)
    {
        modifiers.RemoveAll(modifier => modifier.source == source);
        wasModified = true;
    }

    private float GetFinalValue()
    {
        float finalValue = baseValue;
        float totalPercentageIncrease = 1f; // Start with 100% (no increase)

        foreach (StatModifier modifier in modifiers)
        {
            if (modifier.isPercentage)
                totalPercentageIncrease *= 1 + modifier.value / 100f;
            else
                finalValue += modifier.value;
        }

        finalValue *= totalPercentageIncrease;

        return finalValue;
    }

    public void SetBaseValue(float value) => baseValue = value;

}
[Serializable]
public class StatModifier
{
    public float value;
    public string source;
    public bool isPercentage;

    public StatModifier(float value, string source, bool isPercentage = false)
    {
        this.value = value;
        this.source = source;
        this.isPercentage = isPercentage;
    }
}