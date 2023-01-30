using UnityEngine;
using UnityEngine.InputSystem;

namespace @_.Scripts.Player
{
    public partial class PlayerController
    {
        [Header("Player Component Refrences")] 
        public Rigidbody2D rigidbody2D;
        public BoxCollider2D collider;

        [Header("Player Settings")] public float speed = 8f;
        public float jumpForce = 16f;
        public float horizontal;
        public float coyoteTime = 0.2f;
        public float jumpBufferTime = 0.2f;
        public bool canMove = true;
        public bool isFacingRight = true;

        private float _jumpBufferCounter;
        private float _coyoteTimeCounter;

        [Header("Player Ground Check")] 
        public LayerMask groundLayerMask;

        [Header("Player Wall Check")] 
        public LayerMask wallLayerMask;
        public Transform wallCheck;
        public float wallDragSpeed = 2f;
        public bool grabbingWall;
        
        [Header("Player Dash")]
        public float dashingPower = 12f;
        public float dashingTime = 0.2f;
        public float dashingCooldown = 1f;
        public bool isDashing;
        public bool canDash = true;

    }
}