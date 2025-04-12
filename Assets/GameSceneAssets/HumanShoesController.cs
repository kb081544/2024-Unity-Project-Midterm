using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanShoesController : MonoBehaviour
{
    public float HumanShoesMoveSpeed = 5f;
    public float jumpForce = 6f;
    private Rigidbody2D rb;
    private float minHeight;
    private bool canJump = true;
    private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();
    private SoundManager soundManager; // SoundManager 추가
    string[] fallingAssets = { "poop(Clone)", "grape(Clone)", "banana(Clone)", "goldbar(Clone)", "strawberry(Clone)", "watermelon(Clone)" };

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2f;
        minHeight = transform.position.y;

        soundManager = GameObject.Find("Sounds").GetComponent<SoundManager>(); // SoundManager 가져오기
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        Vector3 moveDirection = new Vector3(moveInput, 0, 0).normalized;
        transform.position += moveDirection * HumanShoesMoveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
        }
        if (transform.position.y < minHeight)
        {
            transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
            canJump = true;
        }

        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, Camera.main.ViewportToWorldPoint(Vector3.zero).x + 0.5f,
                                Camera.main.ViewportToWorldPoint(Vector3.right).x - 0.5f);
        transform.position = viewPos;
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        canJump = false;
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
