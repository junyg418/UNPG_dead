using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 10; // ���� ���� ������
    public float attackInterval = 1.5f; // ���� ����
    private PlayerState playerState; // PlayerState ��ũ��Ʈ ����
    private bool isPlayerInRange = false; // �÷��̾ ���� ���� �ȿ� �ִ��� ����

    private void Start()
    {
        // PlayerState ��ũ��Ʈ�� ã���ϴ�.
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerState = player.GetComponent<PlayerState>();
        }
        else
        {
            Debug.LogError("Player ��ü�� ã�� �� �����ϴ�.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�浹 ����: " + collision.gameObject.name);

        // �浹�� ��ü�� �±װ� "Player"���� Ȯ��
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player�� ������ ���Խ��ϴ�.");
            isPlayerInRange = true;
            StartCoroutine(AttackPlayer());
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        // �÷��̾ ������ ����� ��
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private IEnumerator AttackPlayer()
    {
        Debug.Log("111");

        while (isPlayerInRange)
        {
            if (playerState != null)
            {
                playerState.DecreaseHp(attackDamage); // ü�� ����
                Debug.Log("�÷��̾� ����! ���� ü��: " + playerState.currentHp);
            }
            yield return new WaitForSeconds(attackInterval); // ���� ���ݸ�ŭ ���
        }
    }
}
