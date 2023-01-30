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
            if (context.performed || context.started)
        {
            if (!(_coyoteTimeCounter > 0)) return;
            
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
        }

        if (context.canceled && rigidbody2D.velocity.y >0f)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            _coyoteTimeCounter = 0f;
        }
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