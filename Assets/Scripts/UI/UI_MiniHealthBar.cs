using UnityEngine;

public class UI_MiniHealthBar : MonoBehaviour
{
    Entity entity;

    void OnEnable()
    {
        entity = GetComponentInParent<Entity>();
        entity.OnEntityFlipped += HandleFlip;
    }

    void OnDisable()
    {
        entity.OnEntityFlipped -= HandleFlip;
    }

    private void HandleFlip() => transform.rotation = Quaternion.identity;
}