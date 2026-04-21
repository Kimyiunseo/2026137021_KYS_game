using UnityEngine;
using UnityEngine.InputSystem; // <-- 이 줄이 반드시 있어야 InputValue를 인식합니다!

public class PlayerMove : MonoBehaviour
{
    float speed = 5f;
    float inputValue;

    Rigidbody2D body;
    Animator anim;
    SpriteRenderer spriter;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>(); // 이제 빨간 줄이 사라질 겁니다.
    }

    private void FixedUpdate()
    {
        // 2026년 기준 Unity에서는 linearVelocityX 대신 velocity를 주로 사용하지만 
        // 최신 버전(6)이라면 linearVelocity가 맞습니다.
        body.linearVelocity = new Vector2(inputValue * speed, body.linearVelocity.y);
    }

    private void LateUpdate()
    {
        if (anim != null) // 에러 방지를 위한 널 체크
        {
            anim.SetFloat("Speed", Mathf.Abs(inputValue));
        }

        if (inputValue != 0)
        {
            spriter.flipX = inputValue < 0;
        }
    }

    // New Input System 메시지 수신 함수
    private void OnMove(InputValue value)
    {
        inputValue = value.Get<Vector2>().x;
    }
}