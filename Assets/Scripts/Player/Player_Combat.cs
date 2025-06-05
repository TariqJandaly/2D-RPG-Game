using UnityEngine;

public class Player_Combat : Entity_Combat
{

    [Header("Counter Attack Details")]
    [SerializeField] private float counterRecovery = 0.1f;

    public bool CounterAttackPerformed()
    {
        bool hasCountered = false;

        foreach (Collider2D target in GetDetectedColliders())
        {
            ICounterable counterable = target.GetComponent<ICounterable>();
            if (counterable == null)
                continue;
            
            hasCountered = counterable.CanBeCountered;
            if (hasCountered)
                counterable.HandleCounter();
        }

        return hasCountered;
    }

    public float GetCounterRecoveryDuration() => counterRecovery;
}
