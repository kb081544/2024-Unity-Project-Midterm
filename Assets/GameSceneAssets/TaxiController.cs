using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiController : MonoBehaviour
{
    public float TaxiMoveSpeed = 10f;
    private Vector3 initialScale;
    private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();
    private SoundManager soundManager; // SoundManager 추가
    string[] fallingAssets = { "poop(Clone)", "grape(Clone)", "banana(Clone)", "goldbar(Clone)", "strawberry(Clone)", "watermelon(Clone)" };

    void Start()
    {
        initialScale = transform.localScale;

        soundManager = GameObject.Find("Sounds").GetComponent<SoundManager>(); // SoundManager 가져오기

    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        Vector3 moveDirection = new Vector3(moveInput, 0f, 0f).normalized;
        transform.position += moveDirection * TaxiMoveSpeed * Time.deltaTime;
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, Camera.main.ViewportToWorldPoint(Vector3.zero).x + 0.5f,
                                Camera.main.ViewportToWorldPoint(Vector3.right).x - 0.5f);
        transform.position = viewPos;

        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
        }
        else if (moveInput > 0)
        {
            transform.localScale = initialScale;
        }
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
