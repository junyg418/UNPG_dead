using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Transform player; // 플레이어의 위치를 참조하는 변수
    public Slider healthSlider; // 채력바 슬라이더
    public float offsetY = 2f; // 플레이어 밑에 채력바를 얼마나 올릴지 설정하는 변수

    void Update()
    {
        if (player != null)
        {
            // 플레이어의 위치를 기준으로 채력바의 위치 설정
            Vector3 healthBarPosition = player.position;

            // Y축에 offset을 추가하여 채력바를 플레이어 위로 배치
            healthBarPosition.y += offsetY;

            // 채력바의 위치를 world position 기준으로 설정
            healthSlider.transform.position = healthBarPosition;

            // 또는 LocalPosition으로 설정하려면, Slider의 부모가 Canvas일 때 다음과 같이 설정
            // healthSlider.transform.localPosition = new Vector3(0, offsetY, 0);
        }
    }
}
