using UnityEngine;

namespace MT.LD50
{
    public class JetPack : MonoBehaviour
    {
        public bool active;

        [SerializeField] float power = 60;
        [SerializeField] float fuel = 10;

        [SerializeField] float autoRestoreFuel;

        [SerializeField] ParticleSystem fx;

        [SerializeField] GameObject ui;
        [SerializeField] Transform uiBar;
        [SerializeField] AudioSource audioSource;

        CharacterController2D controller;
        float maxFuel;

        void Awake()
        {
            controller = GetComponent<CharacterController2D>();
            controller.onVelocityUpdate += Controller_onVelocityUpdate;
            maxFuel = fuel;
        }

        void Update()
        {
            var emission = fx.emission;
            emission.enabled = active && fuel > 0;

            ui.SetActive(fuel < maxFuel);
            var scale = uiBar.localScale;
            scale.x = fuel / maxFuel;
            uiBar.localScale = scale;

            audioSource.volume = active && fuel > 0 ? 1 : 0;

            if (autoRestoreFuel == 0)
                return;

            if (!active && controller.ground.isTouching)
                fuel = Mathf.Min(maxFuel, fuel + Time.deltaTime * autoRestoreFuel);
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