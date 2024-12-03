using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player; // �÷��̾��� ��ġ�� ������ ����
    private Transform myGround; // ���� ��ġ�� �ٴ�
    private Transform playerGround; // �÷��̾ ��ġ�� �ٴ�

    public float moveSpeed = 3f; // ���� �̵� �ӵ�
    public float stoppingDistance = 1f; // �÷��̾���� �ּ� �Ÿ�
    public float maxFollowDistance = 10f; // ���� ������ �ִ� �Ÿ�
    public float yTolerance = 0.5f; // y�� ���̰� �� ���� �ȿ� ���� ���� ����

    private Collider2D playerCollider;
    private Collider2D enemyCollider;
    void Start()
    {
        // ���� ��ġ�� �ٴ��� ã�Ƽ� ����
        myGround = GetCurrentGround(transform.position);

        // ���� player�� �̹� �����Ǿ� ������ �ڵ� Ž������ ����
        if (player == null)
        {
            // "Player" �±׸� ���� ������Ʈ �˻�
            GameObject playerObject = GameObject.FindWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform; // Transform ����
            }
            else
            {
                Debug.LogError("Player ������Ʈ�� ã�� �� �����ϴ�. ���ΰ� ĳ������ �±׸� 'Player'�� �����ߴ��� Ȯ���ϼ���.");
            }
            enemyCollider = GetComponent<Collider2D>();

            if (player != null)
            {
                playerCollider = player.GetComponent<Collider2D>();

                // �÷��̾�� ���� �浹�� ����
                Physics2D.IgnoreCollision(playerCollider, enemyCollider);
            }
        }
    }

    void Update()
    {
        // �÷��̾ ���ٸ� ��ũ��Ʈ ����
        if (player == null) return;

        // �÷��̾��� �ٴ��� Ȯ��
        playerGround = GetCurrentGround(player.position);
        if (myGround != playerGround)
        {
            // �ٴ��� �ٸ��� �������� ����
            return;
        }
        // y�� ���̰� tolerance ���� ���̸� �������� ����
        if (Mathf.Abs(player.position.y - transform.position.y) > yTolerance)
        {
            return; // y�� ���̰� tolerance ���� ���̸� ������ ����
        }
        // �÷��̾���� �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ������ �ִ� �Ÿ��� ������ ������ ����
        if (distanceToPlayer > maxFollowDistance)
        {
            return;
        }

        // �÷��̾�� ���� �Ÿ� �̻� ������ ������ �÷��̾�� �̵�
        if (distanceToPlayer > stoppingDistance)
        {
            // x�����θ� �̵�
            Vector3 direction = new Vector3(player.position.x - transform.position.x, 0, 0).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    // �ٴ��� Ȯ���ϴ� �޼���
    private Transform GetCurrentGround(Vector3 position)
    {
        // Raycast�� �̿��� �ٴ��� Ȯ���մϴ�.
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 1f); // 1f�� Ray�� �Ÿ��Դϴ�.

        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            return hit.collider.transform;  // �ٴ��� Transform ��ȯ
        }

        return null;  // �ٴڿ� ���� �ʾ��� ��� null ��ȯ
    }
}
