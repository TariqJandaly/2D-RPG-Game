using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    private UI ui;
    private RectTransform rect;
    private UI_SkillTree skillTree;

    [Header("Unlock details")]
    public UI_TreeNode[] neededNodes;
    public UI_TreeNode[] conflictNodes;
    public bool isUnlocked;
    public bool isLocked;

    public UI_TreeConnection connectionToSkill;


    [Header("Skill Details")]
    public Skill_DataSO skillData;
    [Space]
    [SerializeField] private string skillName;
    [SerializeField] private int skillCost;
    [SerializeField] private Image skillIcon;
    [SerializeField] private string skillLockedColorHex = "#9F9797";
    // private Color skillLockedColor = new Color(138, 123, 123);
    private Color skillLockedColor;
    private Color lastColor;

    void Awake()
    {
        ui = GetComponentInParent<UI>();
        skillTree = GetComponentInParent<UI_SkillTree>();
        rect = GetComponent<RectTransform>();

        skillLockedColor = hexToColor(skillLockedColorHex);
        UpdateIconColor(skillLockedColor);
    }

    private void Unlock()
    {
        isUnlocked = true;
        UpdateIconColor(Color.white);
        skillTree.RemoveSkillPoints(skillData.cost);
        LockConflictNodes();

        connectionToSkill?.ConnectionImageUnlocked(true);
    }

    private bool CanBeUnlocked()
    {
        if (isLocked || isUnlocked)
            return false;

        if (!skillTree.hasEnoughSkillPoints(skillData.cost))
            return false;

        // Checking needed nodes
            foreach (UI_TreeNode node in neededNodes)
            {
                if (!node.isUnlocked)
                    return false;

                // Checking conflicting nodes

                // UI_TreeConnectHandler connectHandler = node.GetComponent<UI_TreeConnectHandler>();
                // foreach (UI_TreeConnectDetails connectDetail in connectHandler.connectionDetails)
                // {
                //     Debug.Log(!nonConflictNodes.Contains(connectDetail.node));
                //     Debug.Log(node.isUnlocked);
                //     Debug.Log(!nonConflictNodes.Contains(connectDetail.node) && node.isUnlocked);
                //     if (node.isUnlocked)
                //         return false;
                // }
            }

        // Checking conflicting nodes
        // Old system
        // foreach (UI_TreeNode node in conflictNodes)
        //     if (node.isUnlocked)
        //         return false;



        return true;
    }

    private void UpdateIconColor(Color color)
    {
        if (skillIcon == null)
            return;

        lastColor = skillIcon.color;
        skillIcon.color = color;
    }

    private void LockConflictNodes()
    {
        foreach (UI_TreeNode node in conflictNodes)
            node.isLocked = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(true, rect, this);

        if (isUnlocked || isLocked)
            return;
        
        Color color = Color.white * 0.9f; color.a = 1;
        UpdateIconColor(color);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CanBeUnlocked())
            Unlock();
        else if (isLocked)
            ui.skillToolTip.LockedSkillEffect();
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(false, rect);

        if (isUnlocked || isLocked)
            return;
        
        UpdateIconColor(lastColor);
    }

    public Color hexToColor(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out Color color);

        return color;
    }

    void OnValidate()
    {
        if (skillData == null)
            return;

        skillName = skillData.displayName;
        skillCost = skillData.cost;
        skillIcon.sprite = skillData.icon;
        gameObject.name = "UI_TreeNode - " + skillData.displayName;

    }
}
