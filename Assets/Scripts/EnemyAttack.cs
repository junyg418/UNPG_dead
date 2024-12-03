using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 10; // 적의 공격 데미지
    public float attackInterval = 1.5f; // 공격 간격
    private PlayerState playerState; // PlayerState 스크립트 참조
    private bool isPlayerInRange = false; // 플레이어가 공격 범위 안에 있는지 여부

    private void Start()
    {
        // PlayerState 스크립트를 찾습니다.
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerState = player.GetComponent<PlayerState>();
        }
        else
        {
            Debug.LogError("Player 객체를 찾을 수 없습니다.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌 감지: " + collision.gameObject.name);

        // 충돌한 객체의 태그가 "Player"인지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player가 범위에 들어왔습니다.");
            isPlayerInRange = true;
            StartCoroutine(AttackPlayer());
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어가 범위를 벗어났을 때
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
                playerState.DecreaseHp(attackDamage); // 체력 감소
                Debug.Log("플레이어 공격! 남은 체력: " + playerState.currentHp);
            }
            yield return new WaitForSeconds(attackInterval); // 공격 간격만큼 대기
        }
    }
}
