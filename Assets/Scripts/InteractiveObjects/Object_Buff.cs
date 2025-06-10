using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Buff
{
    public StatType type;
    public float value;
    public bool isPercentage;
}

public class Object_Buff : MonoBehaviour
{

    private SpriteRenderer sr;
    private Entity_Stats statsToModify;

    [Header("Buff details")]
    [SerializeField] private Buff[] buffs;
    [SerializeField] private string buffName;
    [SerializeField] private float buffDuration = 4;
    [SerializeField] private bool canBeUsed = true;

    [Header("Floaty movement")]
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float floatRange = 0.1f;
    private Vector3 startPosition;

    void Awake()
    {
        startPosition = transform.position;
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {

        float Yoffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
        transform.position = startPosition + new Vector3(0, Yoffset, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeUsed)
            return;

        statsToModify = collision.GetComponent<Entity_Stats>();

        StartCoroutine(BuffCo(buffDuration));
    }

    private IEnumerator BuffCo(float duration)
    {
        canBeUsed = false;
        Debug.Log("Buff uesed");

        sr.color = Color.clear;
        ApplyBuffs(true);

        yield return new WaitForSeconds(duration);

        ApplyBuffs(false);
            

        Debug.Log("Buff is removed");

        Destroy(gameObject);
    }

    private void ApplyBuffs(bool apply)
    {
        foreach (Buff buff in buffs)
        {
            if (apply)
                statsToModify.GetStatByType(buff.type).AddModifier(buff.value, buffName, buff.isPercentage);
            else
                statsToModify.GetStatByType(buff.type).RemoveModifier(buffName);
        }
    }
}