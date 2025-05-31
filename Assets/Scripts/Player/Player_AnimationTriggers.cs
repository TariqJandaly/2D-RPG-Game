using UnityEngine;

public class Player_AnimationTriggers : MonoBehaviour
{
    private void CurrentStateTrigger()
    {
        // Get access to the Player component
        Player player = GetComponentInParent<Player>();
        player.CallAnimationTrigger();
    }

}
