using UnityEngine;

namespace MT.LD50
{
    public class Ground2D : MonoBehaviour
    {
        [HideInInspector] public bool isTouching;
        [HideInInspector] public float friction;

        [SerializeField] Collider2D stickyCollider;
        [SerializeField] Collider2D frictionlessCollider;

        void OnCollisionEnter2D(Collision2D collision) => OnCollision2D(collision);
        void OnCollisionStay2D(Collision2D collision) => OnCollision2D(collision);
        void OnCollisionExit2D(Collision2D collision)
        {
            isTouching = false;
            friction = 0;
        }

        void OnCollision2D(Collision2D collision)
        {
            isTouching = true;
            friction = 0;

            if (!collision.rigidbody)
                return;

            var material = collision.rigidbody.sharedMaterial;
            if (!material)
                return;

            friction = material.friction;
        }

        public void SetSticky(bool enabled)
        {
            stickyCollider.enabled = enabled;
            frictionlessCollider.enabled = !enabled;
        }
    }
}