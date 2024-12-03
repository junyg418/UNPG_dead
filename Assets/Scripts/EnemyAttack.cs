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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogError("OnTriggerEnter2D");

        // �÷��̾ ������ ������ ��
        if (other.CompareTag("Player"))
        {
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
