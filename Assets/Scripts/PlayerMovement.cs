using System;
using System.Collections;
using UnityEngine;

/*
 벽 꼭지점에서 이단점프 시 튕겨나는 버그가 있었다.
 -> 벽에 붙어있을 때 속도를 줄여 해결 
 */

public class PlayerMovement : MonoBehaviour
{   
    private Rigidbody2D rb;
    private Collider2D col;
    
    public CameraController cameraController;
    public PlayerState playerState;
    
    public float speed      = 5f;
    public float jumpForce  = 15f;
    public float dashDistance = 5;
    public float dashCooldown = 0.5f;
    
    private int horizontal;
    private int lastDirection = 1; // -1(L) | 1(R)
    private int maxJumps = 2;
    private int jumpCount = 0;
    private bool isDashing = false;
    private bool isTouchingWall = false;
    private Collider2D currentFloorCollider;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        cameraController = FindObjectOfType<CameraController>();
        playerState = GetComponent<PlayerState>();
    }

    void Update()
    {
        int rightInput = Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
        int leftInput = Input.GetKey(KeyCode.LeftArrow) ? -1 : 0;
        horizontal = (leftInput != 0 && rightInput != 0) ? 0 : leftInput + rightInput;
        lastDirection = Math.Abs(horizontal) == 1 ? horizontal : lastDirection;
        transform.localScale = new Vector3(lastDirection*2, 2, 1);
        
        // 점프 관련 로직
        if (currentFloorCollider is not null && Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.DownArrow))
            StartCoroutine(DisableCollider(currentFloorCollider));
        else if (Input.GetKeyDown(KeyCode.C) && jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpCount++;
            UpdateCameraState();
        }
        
        // 대쉬
        if (Input.GetKeyDown(KeyCode.Z) && !isDashing)
        {
            isDashing = true;
            transform.position += new Vector3(lastDirection * dashDistance, 0, 0);
            StartCoroutine(ResetDash());
        }
    }

    private void FixedUpdate()
    {   // 이동
        // if (horizontal != 0 && !isTouchingWall)
        if (horizontal != 0)
            transform.Translate(new Vector2(horizontal * speed * Time.deltaTime, 0));
        // else if (isTouchingWall)
        //     transform.Translate(new Vector2(horizontal * speed * Time.deltaTime, 0));
        else // 정지상태 일 때
            rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            cameraController.accelCamera();
            cameraController.isCameraLocked = false;
            cameraController.offset.y = cameraController.maxYOffset;
            jumpCount = 0;
        }
        if (collision.gameObject.CompareTag("Wall"))
            isTouchingWall = true;
        if (collision.gameObject.CompareTag("Floor"))
        {
            currentFloorCollider = collision.collider;
            jumpCount = 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            cameraController.offset.y = 0f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            isTouchingWall = false;
        if (collision.gameObject.CompareTag("Floor"))
        {
            cameraController.offset.y = cameraController.maxYOffset/2;
            currentFloorCollider = null;
        }
    }

    private IEnumerator DisableCollider(Collider2D floorCollider)
    {
        Physics2D.IgnoreCollision(col,floorCollider,true);
        yield return new WaitForSecondsRealtime(0.4f);
        Physics2D.IgnoreCollision(col,floorCollider,false);
    }

    private void UpdateCameraState()
    {
        if (jumpCount == 1) cameraController.isCameraLocked = true;
        if (jumpCount == 2) cameraController.isCameraLocked = false;
    }

    private IEnumerator ResetDash()
    {
        yield return new WaitForSecondsRealtime(dashCooldown);
        isDashing = false;
    }
}
