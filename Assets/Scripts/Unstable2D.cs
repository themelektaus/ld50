using UnityEngine;

namespace MT.LD50
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Unstable2D : MonoBehaviour
    {
        [SerializeField] public float delay;
        [SerializeField] public bool force;
        [SerializeField] public float respawnDelay = 3;

        Rigidbody2D body;
        Shake shake;
        Vector2 originalPosition;

        bool active;
        float timer;
        float? respawnTimer;
        SpriteRenderer spriteRenderer;
        float visibility;

        public bool blockRespawn { get; set; }

        void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            originalPosition = transform.localPosition;
            shake = GetComponentInChildren<Shake>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            visibility = 0;
            UpdateVisibility();
        }

        void Respawn()
        {
            active = false;
            timer = 0;
            respawnTimer = null;
            body.bodyType = RigidbodyType2D.Static;
            gameObject.layer = LayerMask.NameToLayer("Default");
            transform.localPosition = originalPosition;

            visibility = 0;
            UpdateVisibility();
        }

        void Update()
        {
            visibility = Mathf.Min(1, visibility + Time.deltaTime * 5);

            UpdateVisibility();

            Update_Body();
            Update_Shake();
        }

        void UpdateVisibility()
        {
            spriteRenderer.transform.localScale = Vector3.one * Mathf.Lerp(1.3f, 1, visibility);
            spriteRenderer.color = new(1, 1, 1, visibility);
            
        }

        void Update_Body()
        {
            if (respawnTimer.HasValue)
            {
                respawnTimer = respawnTimer.Value - Time.deltaTime;
                if (respawnTimer.Value <= 0 && !blockRespawn)
                    Respawn();
                return;
            }

            if (!active)
                return;

            if (timer < delay)
            {
                timer += Time.deltaTime;
                return;
            }

            body.bodyType = RigidbodyType2D.Dynamic;
            gameObject.layer = LayerMask.NameToLayer("No Collision");
            respawnTimer = respawnDelay;
        }

        void Update_Shake()
        {
            shake.enabled = !respawnTimer.HasValue && active && timer < delay;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (active)
                return;

            if (!collision.transform.CompareTag("Player"))
                return;

            active = true;
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (respawnTimer.HasValue || force)
                return;

            if (!active)
                return;

            if (!collision.transform.CompareTag("Player"))
                return;

            active = false;
            timer = 0;
        }
    }
}