using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    public float HumanMoveSpeed = 4f; // 이동 속도 조절 매개변수
    private Rigidbody2D rb;
    private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();
    private SoundManager soundManager;

    string[] fallingAssets = { "poop(Clone)", "grape(Clone)", "banana(Clone)", "goldbar(Clone)", "strawberry(Clone)", "watermelon(Clone)" };

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        soundManager = GameObject.Find("Sounds").GetComponent<SoundManager>(); // SoundManager 가져오기

    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        Vector3 moveDirection = new Vector3(moveInput, 0, 0).normalized;
        transform.position += moveDirection * HumanMoveSpeed * Time.deltaTime;

        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, Camera.main.ViewportToWorldPoint(Vector3.zero).x + 0.5f,
                                Camera.main.ViewportToWorldPoint(Vector3.right).x - 0.5f);
        transform.position = viewPos;
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
