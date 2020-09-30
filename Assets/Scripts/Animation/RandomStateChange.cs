using UnityEngine;

#pragma warning disable 649

public class RandomStateChange : StateMachineBehaviour
{
    [SerializeField] private string[] stateNames;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.Play(stateNames[Random.Range(0, stateNames.Length)]);
    }
}
