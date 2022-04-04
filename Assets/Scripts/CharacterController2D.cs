using System;
using UnityEngine;

namespace MT.LD50
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterController2D : MonoBehaviour
    {
        [HideInInspector] public float move;
        [HideInInspector] public bool jump;
        [HideInInspector] public bool jumpHolding;

        [SerializeField] Transform cameraTarget;
        [SerializeField] float cameraTargetMultiplier = .2f;
        [SerializeField] float cameraTargetMaxOffset = 1;

        [SerializeField] float speed = 5;
        [SerializeField] float groundAcceleration = 45;
        [SerializeField] float airAcceleration = 35;
        [SerializeField] float jumpHeight = 8;
        [SerializeField] float jumpHoldBreakSpeed = 20;
        [SerializeField] float jumpGravity = 2;
        [SerializeField] float fallGravity = 3;
        [SerializeField] float coyoteTime = .1f;
        [SerializeField] int jumpCount = 1;
        [SerializeField] Vector2 verticalVelocityRange = new(-15, 10);

        [SerializeField] GameObject interactMarker;
        [SerializeField] Animator animator;
        [SerializeField] Transform spriteTransform;
        [SerializeField] GameObject jumpEffect;

        Rigidbody2D body;
        [NonSerialized] public Ground2D ground;
        float moveVelocity;
        float airTime;
        float jumpTime;
        int currentJumpCount;
        bool wasGrounded;


        public DialogTrigger dialogTrigger;
        public IActiveObject activeObject;

        public event Func<float, float, float> onVelocityUpdate;

        void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            ground = GetComponentInChildren<Ground2D>();
            spriteTransform = animator.GetComponentInChildren<SpriteRenderer>().transform;
        }

        float GetMove()
        {
            return activeObject is null ? move : 0;
        }

        void Update()
        {
            var move = GetMove();

            ground.SetSticky(move == 0);
            moveVelocity = move * Mathf.Max(speed - ground.friction, 0);
            jumpTime = Mathf.Max(0, jumpTime - Time.deltaTime);

            dialogTrigger = null;
            var colliders = Physics2D.OverlapPointAll(body.position);
            foreach (var collider in colliders)
                if (collider.TryGetComponent(out dialogTrigger))
                    break;

            interactMarker.SetActive(dialogTrigger is not null);

            animator.SetFloat("Move", airTime < .1f ? Mathf.Abs(move) : 0);
            if (wasGrounded != ground.isTouching)
            {
                if (!wasGrounded)
                    animator.SetTrigger("Land");
                wasGrounded = ground.isTouching;
            }
            else
            {
                animator.ResetTrigger("Land");
            }

            if (move != 0)
            {
                var scale = spriteTransform.localScale;
                scale.x = move < 0 ? -1 : 1;
                spriteTransform.localScale = scale;
            }
        }

        void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;

            var velocity = body.velocity;
            FixedUpdate_Move(deltaTime, ref velocity);
            FixedUpdate_Jump(deltaTime, ref velocity);

            if (onVelocityUpdate is not null)
                velocity.y = onVelocityUpdate.Invoke(velocity.y, deltaTime);

            velocity.y = Mathf.Clamp(velocity.y, verticalVelocityRange.x, verticalVelocityRange.y);

            body.velocity = velocity;

            var position = cameraTarget.localPosition;
            position.x = Mathf.Clamp(velocity.x * cameraTargetMultiplier, -cameraTargetMaxOffset, cameraTargetMaxOffset);
            cameraTarget.localPosition = position;

            FixedUpdate_Ground();
        }

        void FixedUpdate_Move(float deltaTime, ref Vector2 velocity)
        {
            float accelaration;
            if (ground.isTouching)
            {
                accelaration = groundAcceleration;
                airTime = 0;
            }
            else
            {
                accelaration = airAcceleration;
                airTime += deltaTime;
            }

            velocity.x = Mathf.MoveTowards(velocity.x, moveVelocity, accelaration * deltaTime);
        }

        void FixedUpdate_Jump(float deltaTime, ref Vector2 velocity)
        {
            if (airTime >= coyoteTime && currentJumpCount == 0)
                currentJumpCount = 1;

            if (activeObject is null && jump)
            {
                if (jumpCount > 0 && (
                    ground.isTouching || (
                        jumpTime == 0 && (
                            airTime < coyoteTime ||
                            currentJumpCount < jumpCount
                        )
                    )
                ))
                {
                    jumpTime = .2f;
                    currentJumpCount++;

                    var jumpSpeed = Mathf.Sqrt(Physics2D.gravity.y * -jumpHeight);

                    if (velocity.y > 0)
                        jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0);

                    else if (velocity.y < 0)
                        jumpSpeed += Mathf.Abs(velocity.y);

                    velocity.y += jumpSpeed;

                    if (currentJumpCount > 1)
                        Instantiate(jumpEffect, body.position, Quaternion.identity);

                    animator.SetTrigger("Jump");
                }
            }

            jump = false;

            if (body.velocity.y > 0)
            {
                body.gravityScale = jumpGravity;
                if (!ground.isTouching && !jumpHolding)
                    velocity.y -= jumpHoldBreakSpeed * deltaTime;
            }

            else if (body.velocity.y < 0)
                body.gravityScale = fallGravity;

            else
                body.gravityScale = 1;
        }

        void FixedUpdate_Ground()
        {
            if (!ground.isTouching)
                return;

            jumpTime = 0;
            currentJumpCount = 0;
        }

        public void UnlockBoots()
        {
            jumpCount = 2;
        }

        public void UnlockJetPack()
        {
            if (TryGetComponent<Player2D>(out var player))
                player.canUseJetpack = true;
        }
    }
}