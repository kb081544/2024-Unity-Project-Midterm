using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerrariController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector3 lastMousePosition;
    private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();
    private SoundManager soundManager; // SoundManager 추가
    string[] fallingAssets = { "poop(Clone)", "grape(Clone)", "banana(Clone)", "goldbar(Clone)", "strawberry(Clone)", "watermelon(Clone)" };

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        soundManager = GameObject.Find("Sounds").GetComponent<SoundManager>(); // SoundManager 가져오기
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.y = transform.position.y;
        mousePosition.z = 0f;

        // 이전 마우스 위치와 현재 마우스 위치가 같은지 확인하여 방향을 변경
        if (mousePosition.x > lastMousePosition.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (mousePosition.x < lastMousePosition.x)
        {
            spriteRenderer.flipX = true;
        }

        // 이동
        transform.position = mousePosition;

        // 마우스가 가만히 있을 때는 마지막으로 바뀐 위치를 유지
        lastMousePosition = mousePosition;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        string collidedObjectName = collision.gameObject.name;
        if (System.Array.Exists(fallingAssets, element => element == collidedObjectName))
        {
                // 사운드 매니저에서 함수 실행
                if (collidedObjectName == "poop(Clone)")
                {
                    soundManager.PlayPoopSound();
                }
                else if (collidedObjectName == "goldbar(Clone)")
                {
                    soundManager.PlayGoldbarSound();
                }
                else
                {
                    soundManager.PlayPickSound();
                }
            
        }
    }
}
