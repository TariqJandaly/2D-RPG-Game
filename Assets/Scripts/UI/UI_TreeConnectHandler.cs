using System;
using UnityEngine;
using UnityEngine.UI;



[Serializable]
public class UI_TreeConnectDetails
{
    public string displayName;
    public UI_TreeConnection connection;
    public UI_TreeNode node;
    public NodeDirectionType direction;
    [Range(100f, 350f)] public float lenght;
    [Range(-25f, 25f)] public float rotation;

}

public class UI_TreeConnectHandler : MonoBehaviour
{
    public UI_TreeConnectDetails[] connectionDetails;

    private Image connectionImage;
    private Color origionalColor;

    void Awake()
    {
        if (connectionImage != null)
            origionalColor = connectionImage.color;

        UpdateConnections();
    }

    void OnValidate()
    {
        UpdateConnections();
    }

    [ContextMenu("Update Connections")]
    public void UpdateConnections()
    {
        foreach (UI_TreeConnectDetails detail in connectionDetails)
        {
            if (detail.connection != null)
            {
                UI_TreeConnectHandler connectHandler = detail.node.GetComponent<UI_TreeConnectHandler>();
                detail.connection.DirectConnection(detail.direction, detail.lenght, detail.node, detail.rotation, connectHandler != null ? connectHandler : null);

                detail.node.connectionToSkill = detail.connection;

                detail.displayName = detail.node.skillData.displayName;
            }
        }
    }
}