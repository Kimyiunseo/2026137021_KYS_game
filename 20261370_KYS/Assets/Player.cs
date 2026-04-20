using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 5f;    // 이동 속도
    public float jumpForce = 10f;   // 점프 힘

    public Transform groundCheck;   // 바닥 체크 위치
    public float groundCheckRadius = 0.2f;  // 바닥 체크 범위 반경
    public LayerMask groundLayer;   // 바닥으로 인식할 레이어

    private Rigidbody2D rb; // 물리 연산 담당 Rigidbody2D
    private float moveInput;    // 좌우 입력 값
    private bool isGrounded;    // 바닥에 닿아 있는지 여부
    private bool facingRight = true;    // 캐릭터가 오른쪽을 보고 있는지 여부


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   // Rigidbody2D 컴포넌트 가져오기
    }

    
    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal"); // 좌우 키 입력 받기

        isGrounded = Physics2D.OverlapCircle
        (
            groundCheck.position,   // 좌우 키 입력 받기
            groundCheckRadius,  // 체크 반경
            groundLayer // 체크할 레이어
        );  // 바닥에 닿아 있는지 판별

        if (Input.GetButtonDown("Jump") && isGrounded)  // 점프 키 입력 + 바닥에 있을 때
        {
            Jump(); // 점프 실행
        }

        Flip(); // 캐릭터 방향 전환 처리
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);    // 좌우 이동 처리
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);    // 점프 힘 적용
    }

    private void Flip()
    {
        if (moveInput > 0 && !facingRight)  // 오른쪽 이동 입력인데 왼쪽을 보고 있을 때
        {
            facingRight = true; // 오른쪽 바라보기
            transform.localScale = new Vector3(1, 1, 1);    // 스케일을 양수로 변경
        }
        else if (moveInput < 0 && facingRight)  // 왼쪽 이동 입력인데 오른쪽을 보고 있을 때
        {
            facingRight = false;    // 왼쪽 바라보기
            transform.localScale = new Vector3(-1, 1, 1);   // 스케일을 음수로 변경 (좌우 반전)
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green; // 기즈모 색상 설정
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius); // 바닥 체크 범위 시각화
    }
}