using UnityEngine;

namespace LD50
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Unstable2D : MonoBehaviour
    {
        Rigidbody2D body;
        bool active;

        void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (active)
                return;

            if (!collision.transform.CompareTag("Player"))
                return;

            active = true;
            body.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}