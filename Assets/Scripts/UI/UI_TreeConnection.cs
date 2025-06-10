using UnityEngine;

public class UI_TreeConnection : MonoBehaviour
{

    [SerializeField] private RectTransform rotationPoint;
    [SerializeField] private RectTransform connectionLength;
    [SerializeField] private RectTransform ChildNodeConnectionPoint;

    public void DirectConnection(NodeDirectionType direction, float length, UI_TreeNode node, UI_TreeConnectHandler connectHandler)
    {
        bool shouldBeActive = direction != NodeDirectionType.None;

        float finalLength = shouldBeActive ? length : 0;
        float angle = GetDirectionAngle(direction);

        rotationPoint.localRotation = Quaternion.Euler(0, 0, angle);
        connectionLength.sizeDelta = new Vector2(finalLength, connectionLength.sizeDelta.y);

        if (node != null)
            node.transform.position = new Vector2(ChildNodeConnectionPoint.position.x, ChildNodeConnectionPoint.position.y);

        if (connectHandler != null)
            connectHandler.UpdateConnections();
    }

    private float GetDirectionAngle(NodeDirectionType type)
    {
        switch (type)
        {
            case NodeDirectionType.Left: return -135f;
            case NodeDirectionType.UpLeft: return 180f;
            case NodeDirectionType.Up: return 135;
            case NodeDirectionType.UpRight: return 90f;
            case NodeDirectionType.Right: return 45f;
            case NodeDirectionType.DownRight: return 0f;
            case NodeDirectionType.Down: return -45f;
            case NodeDirectionType.DownLeft: return -90f;
            default: return 0f;
        }
    }
}

public enum NodeDirectionType
{
    None,
    Left,
    UpLeft,
    Up,
    UpRight,
    Right,
    DownRight,
    Down,
    DownLeft,
}
