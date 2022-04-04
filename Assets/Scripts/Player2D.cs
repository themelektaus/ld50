using UnityEngine;

namespace MT.LD50
{
    [RequireComponent(typeof(CharacterController2D))]
    public class Player2D : MonoBehaviour
    {
        [SerializeField] float tryJumpTime = .1f;
        public bool canUseJetpack;

        CharacterController2D controller;
        JetPack jetPack;

        float jump;

        void Awake()
        {
            controller = GetComponent<CharacterController2D>();
            jetPack = GetComponent<JetPack>();
        }

        void Update()
        {
            controller.move = InputManager.horizontal.axisRaw;

            if (controller.activeObject is null && controller.dialogTrigger)
            {
                if (InputManager.jump.down)
                {
                    CancelJump();
                    controller.dialogTrigger.Show(controller);
                }
                return;
            }

            if (controller.activeObject is not null)
            {
                CancelJump();
                if (InputManager.jump.down)
                    controller.activeObject.Next();
                return;
            }

            if (InputManager.jump.down)
                jump = Mathf.Max(Time.deltaTime, tryJumpTime);

            controller.jump |= jump > 0;
            controller.jumpHolding = InputManager.jump.holding;

            jump = Mathf.Max(0, jump - Time.deltaTime);

            jetPack.active = canUseJetpack && (InputManager.fire1.holding || InputManager.fire2.holding || InputManager.fire3.holding);
        }

        void CancelJump()
        {
            jump = 0;
            controller.jump = false;
            controller.jumpHolding = false;
        }
    }
}