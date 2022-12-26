using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNextStageState : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var spawner = animator.GetComponent<CircularSpawnProjectile>();
        spawner.Stage++;

        var changeLight = animator.GetComponent<ChangeLightsComponent>();
        changeLight.SetColor();
    }
}
