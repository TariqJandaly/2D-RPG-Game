using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Skill Data", fileName = "Skill data - ")]
public class Skill_DataSO : ScriptableObject
{
    [Header("Skill details")]
    public int cost;
    public string displayName;
    [TextArea]
    public string descripiton;
    public Sprite icon;
}