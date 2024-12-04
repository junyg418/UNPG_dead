using UnityEngine;
using UnityEngine.UI;


public class EnemyState : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;
    public Text currentHPText;

    private Transform enemyTransform;
    
    private void Start()
    {
        enemyTransform = transform;
        currentHP = maxHP;
        EnemyHealthBarManager.Instance.AddHealthBar(enemyTransform, maxHP);
        if (currentHPText != null) currentHPText.text = $"{currentHP} / {maxHP}"; // 추후 제거
    }

    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
        EnemyHealthBarManager.Instance.UpdateHealth(enemyTransform, currentHP);
        if (currentHPText != null) currentHPText.text = $"{currentHP} / {maxHP}"; // 추후 제거
        if (currentHP <= 0) Debug.Log("사망"); // TODO 사망
    }
}
