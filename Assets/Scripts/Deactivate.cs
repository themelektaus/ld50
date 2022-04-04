using UnityEngine;

namespace MT.LD50
{
    public class Deactivate : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.gameObject.SetActive(false);
        }
    }
}