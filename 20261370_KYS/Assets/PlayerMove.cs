using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 3.5f; // 이동 속도

    [Header("컴포넌트 참조")]
    [SerializeField] private Rigidbody2D rb = null; // Rigidbody2D 참조
    [SerializeField] private Animator animator = null; // Animator 참조

    private Vector2 moveInput = Vector2.zero; // 입력 벡터 저장
    private Vector2 lastMoveDirection = Vector2.down; // 마지막 바라본 방향

    private void Reset()
    {
        // 컴포넌트 자동 할당
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 rawInput = new Vector2(horizontal, vertical);

        if (rawInput.sqrMagnitude > 1f)
        {
            rawInput = rawInput.normalized;
        }

        moveInput = rawInput;
        bool isMoving = moveInput.sqrMagnitude > 0f;

        if (isMoving)
        {
            lastMoveDirection = moveInput;
        }

        UpdateAnimationParameters(moveInput, isMoving);
    }

    private void FixedUpdate()
    {
        // 물리 이동(FixedUpdate에서 수행)
        if (rb == null)
        {
            return;
        }

        Vector2 targetPosition = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPosition);
    }

    private void UpdateAnimationParameters(Vector2 currentMove, bool isMoving)
    {
        // 애니메이터 파라미터 설정
        if (animator == null)
        {
            return;
        }

        animator.SetFloat("moveX", currentMove.x);
        animator.SetFloat("moveY", currentMove.y);
        animator.SetBool("isMoving", isMoving);
        animator.SetFloat("lastMoveX", lastMoveDirection.x);
        animator.SetFloat("lastMoveY", lastMoveDirection.y);
    }

}