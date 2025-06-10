using Unity.Cinemachine;
using UnityEngine;

public class UI_ToolTip : MonoBehaviour
{
    private RectTransform rect;
    [SerializeField] private Vector2 offset = new Vector2(300, 20);

    public virtual void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public virtual void ShowToolTip(bool show, RectTransform targetRect)
    {
        if (!show)
        {
            rect.position = new Vector2(9999, 9999);
            return;
        }

        UpdatePosition(targetRect);
    }

    public void UpdatePosition(RectTransform targetRect)
    {

        float screenCenterX = Screen.width / 2;
        float screenTop = Screen.height;
        float screenBottom = 0f;


        Vector2 targetPosition = targetRect.position;

        float toolTipHalf = rect.sizeDelta.y / 2;
        float toolTipTop = targetPosition.y + toolTipHalf;
        float toolTipBottom = targetPosition.y - toolTipHalf;

        targetPosition.x += targetPosition.x > screenCenterX ? -offset.x : offset.x;

        if (toolTipTop > screenTop)
            targetPosition.y = screenTop - toolTipHalf - offset.y;
        else if (toolTipBottom < screenBottom)
            targetPosition.y = screenBottom + toolTipHalf + offset.y;

        rect.position = targetPosition;

    }
    
}