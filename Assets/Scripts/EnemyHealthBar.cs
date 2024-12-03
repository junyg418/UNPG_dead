using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Transform player; // �÷��̾��� ��ġ�� �����ϴ� ����
    public Slider healthSlider; // ä�¹� �����̴�
    public float offsetY = 2f; // �÷��̾� �ؿ� ä�¹ٸ� �󸶳� �ø��� �����ϴ� ����

    void Update()
    {
        if (player != null)
        {
            // �÷��̾��� ��ġ�� �������� ä�¹��� ��ġ ����
            Vector3 healthBarPosition = player.position;

            // Y�࿡ offset�� �߰��Ͽ� ä�¹ٸ� �÷��̾� ���� ��ġ
            healthBarPosition.y += offsetY;

            // ä�¹��� ��ġ�� world position �������� ����
            healthSlider.transform.position = healthBarPosition;

            // �Ǵ� LocalPosition���� �����Ϸ���, Slider�� �θ� Canvas�� �� ������ ���� ����
            // healthSlider.transform.localPosition = new Vector3(0, offsetY, 0);
        }
    }
}
