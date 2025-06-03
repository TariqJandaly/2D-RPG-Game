using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplier;
    [SerializeField] private float imageWidthOffset = 10f;

    private float imageFullWidth;
    private float imageHalfWidth;

    public void CalculateImageWidth()
    {
        imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2f;
    }

    public void MoveBackground(float distanceToMove)
    {
        background.position += Vector3.right * (distanceToMove * parallaxMultiplier);
    }

    public void LoopBackground(float cameraRightEdge, float cameraLeftEdge)
    {
        float imageRightEdge = background.position.x + imageHalfWidth - imageWidthOffset;
        float imageLeftEdge = background.position.x - imageHalfWidth + imageWidthOffset;

        if (imageRightEdge < cameraLeftEdge)
            background.position += Vector3.right * imageFullWidth;
        else if (imageLeftEdge > cameraRightEdge)
            background.position += Vector3.right * -imageFullWidth;
    }
}
