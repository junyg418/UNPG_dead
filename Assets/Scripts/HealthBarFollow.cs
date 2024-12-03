using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public Vector3 offset = new Vector3(0, 2f, 0); // 플레이어의 위치에 대한 오프셋(플레이어 위로 2 유닛)

    private RectTransform healthBarRectTransform;

    void Start()
    {
        // HealthBar의 RectTransform을 가져옵니다.
        healthBarRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // 플레이어가 없다면, 업데이트를 멈춥니다.
        if (player == null) return;

        // 캐릭터의 위치에 채력 바 위치 업데이트
        Vector3 worldPosition = player.position + offset; // 오프셋을 더해 플레이어 밑으로 위치 조정
        healthBarRectTransform.position = worldPosition; // 월드 공간에서 위치 설정
    }
}
