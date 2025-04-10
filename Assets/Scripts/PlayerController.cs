using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer; // Layer trong Inspector
    [SerializeField] private Transform groundCheck;
    
    GameManager gameManager;
    AudioManager audioManager;
    
    private bool isGrounded;
    
    private Animator animator;

    private Rigidbody2D rb;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameOver() || gameManager.isGameWin()) return;
        HandleJump();
        HandleMovement();
        UpdateAnimation();
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y); // di chuyển theo vận tốc trục x, y giữ nguyên

        if (moveInput > 0) {
            transform.localScale = new Vector3(1,1,1);
        }
        else if (moveInput < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
    }

    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            audioManager.PlayJumpSound();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);// vận tốc nhảy lên trục y
        }
        // kiểm tra xem có collider(groundLayer) nằm trong 1 vòng tròn tại vị trí chỉ định không
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer); 
        //groundCheck.position	Vị trí Tâm cảu gameObject của vòng tròn kiểm tra
        //0.2f	Bán kính vòng tròn
        //groundLayer	LayerMask để lọc đối tượng cần kiểm tra
    }

    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;// Gía trị tuyệt đối vận tốc trục |x| > 0 => đang di chuyên
        bool isJumping = !isGrounded;// isGrounded = fales => Nv đang ko tiếp xúc mặt đất => nhảy
        
        // xét đkiện chạy animator
        animator.SetBool("isRunning", isRunning);//"isRunning" tên animator, true/fale
        animator.SetBool("isJumping", isJumping);//"isJumping"" tên animator, true/fale
    }
}
