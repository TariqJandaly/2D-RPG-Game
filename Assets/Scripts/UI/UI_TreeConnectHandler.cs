using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;



[Serializable]
public class UI_TreeConnectDetails
{
    public UI_TreeConnection connection;
    public UI_TreeNode node;
    public NodeDirectionType direction;
    [Range(100f, 350f)] public float lenght;

}

public class UI_TreeConnectHandler : MonoBehaviour
{
    public UI_TreeConnectDetails[] connectionDetails;

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
                detail.connection.DirectConnection(detail.direction, detail.lenght, detail.node, connectHandler ?? null);
            }
        }
    }
}
