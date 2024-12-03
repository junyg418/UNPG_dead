using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public Vector3 offset = new Vector3(0, 2f, 0); // �÷��̾��� ��ġ�� ���� ������(�÷��̾� ���� 2 ����)

    private RectTransform healthBarRectTransform;

    void Start()
    {
        // HealthBar�� RectTransform�� �����ɴϴ�.
        healthBarRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // �÷��̾ ���ٸ�, ������Ʈ�� ����ϴ�.
        if (player == null) return;

        // ĳ������ ��ġ�� ä�� �� ��ġ ������Ʈ
        Vector3 worldPosition = player.position + offset; // �������� ���� �÷��̾� ������ ��ġ ����
        healthBarRectTransform.position = worldPosition; // ���� �������� ��ġ ����
    }
}
