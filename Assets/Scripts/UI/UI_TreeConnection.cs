using UnityEngine;
using UnityEngine.UI;

public class UI_TreeConnection : MonoBehaviour
{

    [SerializeField] private RectTransform rotationPoint;
    [SerializeField] private RectTransform connectionLength;
    [SerializeField] private RectTransform ChildNodeConnectionPoint;

    private Image connectionImage;
    private Color originalColor;

    void Awake()
    {
        connectionImage = connectionLength.GetComponent<Image>();
        originalColor = connectionImage.color;

        ConnectionImageUnlocked(false);
    }

    public void DirectConnection(NodeDirectionType direction, float length, UI_TreeNode node, float offset, UI_TreeConnectHandler connectHandler)
    {
        bool shouldBeActive = direction != NodeDirectionType.None;

        float finalLength = shouldBeActive ? length : 0;
        float angle = GetDirectionAngle(direction);

        rotationPoint.localRotation = Quaternion.Euler(0, 0, angle + offset);
        connectionLength.sizeDelta = new Vector2(finalLength, connectionLength.sizeDelta.y);

        if (node != null)
            node.transform.position = new Vector2(ChildNodeConnectionPoint.position.x, ChildNodeConnectionPoint.position.y);

        if (connectHandler != null)
            connectHandler.UpdateConnections();
    }

    public void ConnectionImageUnlocked(bool unlocked)
    {
        if (!connectionImage)
            return;

        connectionImage.color = unlocked ? Color.white : Color.gray;
    }

    public Image GetConnectionImage() => connectionLength.GetComponent<Image>();

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
