using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public int maxHp = 100;
    public int currentHp = 100;
    public Slider hpSlider;
    public Text currentHpText;
    
    public int maxMental = 100;
    private int currentMental = 100;
    public Slider mentalSlider;
    public Text currentMentalText;

    public int mentalRegenRate = 2;

    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        currentHp = maxHp;
        hpSlider.value = currentHp;
        currentHpText.text = $"{currentHp} / {maxHp}";


        // currentMental = maxMental;
        currentMental = 0;
        mentalSlider.value = currentMental;
        currentMentalText.text = $"{currentMental} / {maxMental}";

        StartCoroutine(RegenerateMental());
    }

    private void Update()
    {
    }

    public void DecreaseHp(int value)
    {
        if (isInvincible) return;
        
        ModifyHpState(-value);
        if (currentHp <= 0) Debug.Log("사망"); // TODO 사망
        StartCoroutine(InvincibilityCoroutine());
    }
    
    public void ModifyHpState(int value, bool isCurrent=true)
    {
        if (isCurrent)
        {
            currentHp = Math.Clamp(currentHp + value, 0, maxHp);
            hpSlider.value = currentHp;
        }
        else
        {
            maxHp += value;
            hpSlider.maxValue = maxHp;
        }
        currentHpText.text = $"{currentHp} / {maxHp}";
    }
    
    public void ModifyMentalState(int value, bool isCurrent=true)
    {
        if (isCurrent)
        {
            currentMental = Math.Clamp(currentMental + value, 0, maxMental);
            mentalSlider.value = currentMental;
        }
        else
        {
            maxMental += value;
            mentalSlider.maxValue = maxMental;
        }
        currentMentalText.text = $"{currentMental} / {maxMental}";
    }
    
    private IEnumerator InvincibilityCoroutine(float duration=1.0f)
    {
        isInvincible = true; // 무적 상태 활성화
        Color originalColor = spriteRenderer.color;
        
        float elapsed = 0f;
        while (elapsed < duration)
        {
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f); // 반투명
            yield return new WaitForSeconds(0.15f); // x초 동안 대기
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(0.15f);
            
            elapsed += 0.3f;
        }

        spriteRenderer.color = originalColor; // 원래 색 복원
        isInvincible = false; // 무적 상태 비활성화
    }

    private IEnumerator RegenerateMental()
    {
        while (true)
        {
            if(currentMental < maxMental) ModifyMentalState(mentalRegenRate);
            yield return new WaitForSeconds(1.5f);
        }
    }
}