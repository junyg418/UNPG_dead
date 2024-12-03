using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player; // 플레이어의 위치를 참조할 변수
    private Transform myGround; // 적이 위치한 바닥
    private Transform playerGround; // 플레이어가 위치한 바닥

    public float moveSpeed = 3f; // 적의 이동 속도
    public float stoppingDistance = 1f; // 플레이어와의 최소 거리
    public float maxFollowDistance = 10f; // 적이 추적할 최대 거리
    public float yTolerance = 0.5f; // y축 차이가 이 범위 안에 있을 때만 추적

    private Collider2D playerCollider;
    private Collider2D enemyCollider;
    void Start()
    {
        // 적이 위치한 바닥을 찾아서 설정
        myGround = GetCurrentGround(transform.position);

        // 만약 player가 이미 설정되어 있으면 자동 탐색하지 않음
        if (player == null)
        {
            // "Player" 태그를 가진 오브젝트 검색
            GameObject playerObject = GameObject.FindWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform; // Transform 설정
            }
            else
            {
                Debug.LogError("Player 오브젝트를 찾을 수 없습니다. 주인공 캐릭터의 태그를 'Player'로 설정했는지 확인하세요.");
            }
            enemyCollider = GetComponent<Collider2D>();

            if (player != null)
            {
                playerCollider = player.GetComponent<Collider2D>();

                // 플레이어와 적의 충돌을 무시
                Physics2D.IgnoreCollision(playerCollider, enemyCollider);
            }
        }
    }

    void Update()
    {
        // 플레이어가 없다면 스크립트 종료
        if (player == null) return;

        // 플레이어의 바닥을 확인
        playerGround = GetCurrentGround(player.position);
        if (myGround != playerGround)
        {
            // 바닥이 다르면 추적하지 않음
            return;
        }
        // y축 차이가 tolerance 범위 밖이면 추적하지 않음
        if (Mathf.Abs(player.position.y - transform.position.y) > yTolerance)
        {
            return; // y축 차이가 tolerance 범위 밖이면 추적을 멈춤
        }
        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 추적할 최대 거리를 넘으면 추적을 멈춤
        if (distanceToPlayer > maxFollowDistance)
        {
            return;
        }

        // 플레이어와 일정 거리 이상 떨어져 있으면 플레이어로 이동
        if (distanceToPlayer > stoppingDistance)
        {
            // x축으로만 이동
            Vector3 direction = new Vector3(player.position.x - transform.position.x, 0, 0).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    // 바닥을 확인하는 메서드
    private Transform GetCurrentGround(Vector3 position)
    {
        // Raycast를 이용해 바닥을 확인합니다.
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 1f); // 1f는 Ray의 거리입니다.

        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            return hit.collider.transform;  // 바닥의 Transform 반환
        }

        return null;  // 바닥에 닿지 않았을 경우 null 반환
    }
}
