using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemyHealthBarManager : MonoBehaviour
{
    public static EnemyHealthBarManager Instance { get; private set; }
    
    public Canvas worldSpaceCanvas; // 월드 스페이스 캔버스
    public GameObject sliderPrefab; // Slider 프리팹

    private Dictionary<Transform, (Slider slider, RectTransform rectTransform)> healthBars = new Dictionary<Transform, (Slider, RectTransform)>();
    private Camera mainCamera;

    public GameObject exEnmy;
    private RectTransform _canvasRect;

    private void Awake()
    {
        _canvasRect = worldSpaceCanvas.GetComponent<RectTransform>();
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    void Start()
    {
        mainCamera = Camera.main;
        AddHealthBar(exEnmy.transform, 100);
    }

    // 체력바 추가
    public void AddHealthBar(Transform target, int maxHp)
    {
        if (healthBars.ContainsKey(target)) return;

        // Slider 생성
        GameObject healthBarObject = Instantiate(sliderPrefab, worldSpaceCanvas.transform);
        Slider slider = healthBarObject.GetComponent<Slider>();
        RectTransform rectTransform = healthBarObject.GetComponent<RectTransform>();

        slider.maxValue = maxHp;
        slider.value = maxHp;

        // Dictionary에 Slider와 RectTransform 저장
        healthBars[target] = (slider, rectTransform);
    }

    // 체력 업데이트
    public void UpdateHealth(Transform target, int currentHp)
    {
        if (healthBars.ContainsKey(target))
        {
            healthBars[target].slider.value = currentHp;
        }
    }

    // 체력바 삭제
    public void RemoveHealthBar(Transform target)
    {
        if (healthBars.ContainsKey(target))
        {
            Destroy(healthBars[target].slider);
            healthBars.Remove(target);
        }
    }

    void Update()
    {
        foreach (var entry in healthBars)
        {
            Transform target = entry.Key;
            (Slider slider, RectTransform sliderRect) = entry.Value;

            // 월드 좌표 → 스크린 좌표 변환
            Vector3 worldPosition = target.position + new Vector3(-1.1f, 1.3f, 0); // 머리 위
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

            // Z축이 카메라 뒤에 있지 않으면 업데이트
            if (screenPosition.z > 0)
            {
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, screenPosition, mainCamera, out Vector2 localPoint))
                {
                    sliderRect.localPosition = localPoint; // 로컬 좌표 설정
                    slider.gameObject.SetActive(true);
                }
            }
            else
            {
                slider.gameObject.SetActive(false); // 카메라 밖에 있으면 숨김
            }
        }
    }
}
