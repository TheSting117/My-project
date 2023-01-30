using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace @_.Scripts.Player
{
    public partial class PlayerController
    {
        public void Move(InputAction.CallbackContext context)
        {
            horizontal = context.ReadValue<Vector2>().x;
        }

        public void Jump(InputAction.CallbackContext context)
        {
            var velocity = rigidbody2D.velocity;
            if (context.performed)
                _jumpBufferCounter = jumpBufferTime;
            else
                _jumpBufferCounter -= Time.deltaTime;

            if (_jumpBufferCounter >0f && _coyoteTimeCounter >0f)
            {
                velocity = new Vector2(velocity.x, jumpForce);
                _jumpBufferCounter = 0f;
            }

            if (!context.canceled || !(rigidbody2D.velocity.y > 0f)) return;
            velocity = new Vector2(velocity.x, velocity.y * 0.5f);
            rigidbody2D.velocity = velocity;
            _coyoteTimeCounter = 0;
        }
        public void Dash(InputAction.CallbackContext context)
        {
            if (!context.performed || !canDash) return;
            isDashing = true;
            StartCoroutine(DashCooldown());
        }
        private IEnumerator DashCooldown()
        {
            canDash = false;
            isDashing = true;
            var originalGravity = rigidbody2D.gravityScale;
            rigidbody2D.gravityScale = 0f;
            rigidbody2D.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
            yield return new WaitForSeconds(dashingTime);
            rigidbody2D.gravityScale = originalGravity;
            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }
    }
}