using UnityEngine;

namespace @_.Scripts.Player
{
    public partial class PlayerController : MonoBehaviour
    {
        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            collider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if ((!isFacingRight && horizontal > 0f) || (isFacingRight && horizontal < 0f)) Flip();

            if (GroundCheck())
            {
                _coyoteTimeCounter = coyoteTime;
                canMove = true;
            }
            else
            {
                _coyoteTimeCounter -= Time.deltaTime;
            }

            grabbingWall = WallCheck();
        }

        private void FixedUpdate()
        {
            if (isDashing)
            {
                Debug.Log("dash");
                canDash = false;
                rigidbody2D.gravityScale = 0f;
                rigidbody2D.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
                return;
            }
            rigidbody2D.velocity = new Vector2(horizontal * speed, rigidbody2D.velocity.y);
            WallSlide();
        }
        private bool GroundCheck()
        {
            var bounds = collider.bounds;
            return Physics2D.BoxCast(bounds.center, bounds.size, 0f, 
                Vector2.down, .1f, groundLayerMask);
        }
        
        private bool WallCheck()
        {
            return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayerMask);
        }

        private void WallSlide()
        {
            if (WallCheck() && !GroundCheck() && horizontal != 0f)
            {
                grabbingWall = true;
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,
                    Mathf.Clamp(rigidbody2D.velocity.y, -wallDragSpeed, float.MaxValue));
            }
            else
            {
                grabbingWall = false;
            }
        }

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            var localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}