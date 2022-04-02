using UnityEngine;

namespace LD50
{
    [RequireComponent(typeof(CharacterController2D))]
    public class Player2D : MonoBehaviour
    {
        [SerializeField] float tryJumpTime = .1f;

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

            if (InputManager.jump.down)
                jump = Mathf.Max(Time.deltaTime, tryJumpTime);

            controller.jump |= jump > 0;
            controller.jumpHolding = InputManager.jump.holding;

            jump = Mathf.Max(0, jump - Time.deltaTime);

            jetPack.active = InputManager.fire3.holding;
        }
    }
}