using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MT.LD50
{
    public class CharacterController2D_FirstTry : MonoBehaviour
    {
        [SerializeField] float jumpForce = 10;
        [SerializeField] AnimationCurve jumpCurve;
        [SerializeField] AnimationCurve fallingCurve;
        
        [SerializeField, Range(0, .2f)] float moveSmoothness = .02f;
        [SerializeField] float airControl = .1f;

        [SerializeField] LayerMask groundLayer;
        [SerializeField] Transform bottomPosition;
        [SerializeField] float groundedRadius = .2f;

        public UnityEvent onLand = new();

        bool grounded;
        float airTime;
        bool falling;
        float fallTime;
        Rigidbody2D body;
        List<Collider2D> colliders;
        bool flipped;
        Vector2 velocity;

        void Awake()
        {
            body = GetComponentInChildren<Rigidbody2D>();
            body.freezeRotation = true;

            colliders = new(GetComponentsInChildren<Collider2D>());
        }

        void FixedUpdate()
        {
            var wasGrounded = grounded;
            grounded = false;

            foreach (var c in Physics2D.OverlapCircleAll(bottomPosition.position, groundedRadius, groundLayer))
            {
                if (colliders.Contains(c))
                    continue;

                grounded = true;
                airTime = 0;
                falling = false;
                fallTime = 0;
                if (!wasGrounded)
                    onLand.Invoke();
            }

            if (grounded)
                return;

            airTime += Time.fixedDeltaTime;

            falling = body.velocity.y < 0;
            if (falling)
                fallTime += Time.fixedDeltaTime;
        }

        public void Move(float move, bool jump, bool jumpHolding)
        {
            var x = body.velocity.x;
            var y = body.velocity.y;

            x = Mathf.Lerp(x, move * 10, grounded ? 1 : airControl);

            if (jump && grounded)
            {
                grounded = false;
                y = jumpForce;
            }

            if (!jumpHolding && !grounded && !falling)
                y = 0;

            if (falling)
                y *= fallingCurve.Evaluate(fallTime);
            else if (!grounded)
                y *= jumpCurve.Evaluate(airTime);

            var velocity = new Vector2(x, y);
            body.velocity = Vector2.SmoothDamp(body.velocity, velocity, ref this.velocity, moveSmoothness);

            if ((move < 0 && !flipped) || (move > 0 && flipped))
            {
                flipped = !flipped;

                var scale = body.transform.localScale;
                scale.x *= -1;
                body.transform.localScale = scale;
            }
        }
    }
}