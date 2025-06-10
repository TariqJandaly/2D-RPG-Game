using System;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI_SkillToolTip : UI_ToolTip
{
    private UI_SkillTree skillTree;

    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private TextMeshProUGUI skillRequirements;
    [SerializeField] private Color metConditionColor;
    [SerializeField] private Color notMetConditionColor;
    [SerializeField] private Color importantInfoColor;
    [SerializeField] private string lockedSkillText = "You've taken a diffrent path - this skill is now locked.";
    private string metConditionHex;
    private string notMetConditionHex;
    private string importantInfoHex;

    public override void Awake()
    {
        base.Awake();

        skillTree = GetComponentInParent<UI_SkillTree>();

        metConditionHex = ColorToHex(metConditionColor);
        notMetConditionHex = ColorToHex(notMetConditionColor);
        importantInfoHex = ColorToHex(importantInfoColor);
    }

    public string ColorToHex(Color color)
    {
        // Convert each component of the color to an integer and then to a hex string
        int r = Mathf.RoundToInt(color.r * 255);
        int g = Mathf.RoundToInt(color.g * 255);
        int b = Mathf.RoundToInt(color.b * 255);

        return $"#{r:X2}{g:X2}{b:X2}";
    }

    public override void ShowToolTip(bool show, RectTransform targetRect)
    {
        base.ShowToolTip(show, targetRect);


    }

    public void ShowToolTip(bool show, RectTransform targetRect, UI_TreeNode node)
    {
        base.ShowToolTip(show, targetRect);

        if (!show)
            return;

        skillName.text = node.skillData.displayName;
        skillDescription.text = node.skillData.descripiton;

        string stringLockedText = $"<color={importantInfoHex}>{lockedSkillText}</color>";


        skillRequirements.text = node.isLocked ?
            stringLockedText : GetRequirements(node.skillData.cost, node.neededNodes, node.conflictNodes);
    }

    private string GetRequirements(int skillCost, UI_TreeNode[] neededNodes, UI_TreeNode[] conflictNodes)
    {

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Requirements:");

        string costColor = skillTree.hasEnoughSkillPoints(skillCost) ? metConditionHex : notMetConditionHex;
        sb.AppendLine($"<color={costColor}>- {skillCost} skill point(s)</color>");

        foreach (UI_TreeNode node in neededNodes)
        {
            string nodeColor = node.isUnlocked ? metConditionHex : notMetConditionHex;
            sb.AppendLine($"<color={nodeColor}>- {node.skillData.displayName}</color>");
        }

        if (conflictNodes.Length <= 0)
            return sb.ToString();

        sb.AppendLine(); // Empty Space

        sb.AppendLine($"<color={importantInfoHex}>Locks out:");
        foreach (UI_TreeNode node in conflictNodes)
        {
            sb.AppendLine($"{node.skillData.displayName}");
        }

        sb.Append("</color>");

        return sb.ToString();
    }
}