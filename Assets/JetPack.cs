using UnityEngine;

namespace LD50
{
    public class JetPack : MonoBehaviour
    {
        public bool active;

        [SerializeField] float power = 60;
        [SerializeField] float fuel = 10;

        CharacterController2D controller;

        void Awake()
        {
            controller = GetComponent<CharacterController2D>();
            controller.onVelocityUpdate += Controller_onVelocityUpdate;
        }

        float Controller_onVelocityUpdate(float velocity, float deltaTime)
        {
            if (active && fuel > 0)
            {
                fuel = Mathf.Max(0, fuel - deltaTime);
                velocity += Time.fixedDeltaTime * power;
            }
            return velocity;
        }
    }
}