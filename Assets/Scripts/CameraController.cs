using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CAMERA SIZE : 7.5
// 해상도 : 1920 x 1080 기준
// 가로 넓이 최대 - 13.35, RESULT : -14
// defalut offset : 4, minOffset: 1.56, jumpDuration: 0.3


public class CameraController : MonoBehaviour
{
    public Transform player; 
    public Vector3 offset;
    public float maxYOffset = 4f;
    public float speed = 0.03f;
    public bool isCameraLocked = false;

    public float joffset;
    public float offsetUpDuration;
    public float offsetDownDuration;

    private float _leftX;
    private float _rightX;

    void Start()
    {
        MapBoundary mapBoundary = FindObjectOfType<MapBoundary>();
        offset.y = maxYOffset;
        _leftX = mapBoundary.leftX;
        _rightX = mapBoundary.rightX;
    }
    
    void LateUpdate()
    {
        Vector3 pos = player.position + offset;
        if (isCameraLocked) pos.y = transform.position.y;
        
        pos.x = Mathf.Clamp(pos.x, _leftX+14, _rightX-14);
        Vector3 cameraPos = Vector3.Lerp(transform.position, pos, speed);
        transform.position = cameraPos;
    }

    public void accelCamera()
    {
        StartCoroutine(adjustCameraSpeed(0.05f));
    }

    private IEnumerator adjustCameraSpeed(float newSpeed)
    {
        float defalutSpeed = speed;
        speed = newSpeed;
        yield return new WaitForSeconds(0.3f);
        speed = defalutSpeed;
    }

    public void abjustOffset()
    {
        StartCoroutine(jumpCoroutine());
    }

    private IEnumerator jumpCoroutine()
    {
        yield return StartCoroutine(abjustSmoothCameraOffset(joffset, offsetUpDuration));
        yield return StartCoroutine(abjustSmoothCameraOffset(maxYOffset, offsetDownDuration));
    }
    
    
    private IEnumerator abjustSmoothCameraOffset(float targetOffset, float duration)
    {
        float curTime = 0;
        float startYOffset = offset.y;

        while (curTime < duration)
        {
            offset.y = Mathf.Lerp(startYOffset, targetOffset, curTime / duration);
            curTime += Time.deltaTime;
            yield return null;
        }
        offset.y = targetOffset;
    }
}
