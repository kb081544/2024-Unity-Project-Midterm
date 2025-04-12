using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerrariController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector3 lastMousePosition;
    private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();
    private SoundManager soundManager; // SoundManager �߰�
    string[] fallingAssets = { "poop(Clone)", "grape(Clone)", "banana(Clone)", "goldbar(Clone)", "strawberry(Clone)", "watermelon(Clone)" };

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        soundManager = GameObject.Find("Sounds").GetComponent<SoundManager>(); // SoundManager ��������
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.y = transform.position.y;
        mousePosition.z = 0f;

        // ���� ���콺 ��ġ�� ���� ���콺 ��ġ�� ������ Ȯ���Ͽ� ������ ����
        if (mousePosition.x > lastMousePosition.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (mousePosition.x < lastMousePosition.x)
        {
            spriteRenderer.flipX = true;
        }

        // �̵�
        transform.position = mousePosition;

        // ���콺�� ������ ���� ���� ���������� �ٲ� ��ġ�� ����
        lastMousePosition = mousePosition;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        string collidedObjectName = collision.gameObject.name;
        if (System.Array.Exists(fallingAssets, element => element == collidedObjectName))
        {
                // ���� �Ŵ������� �Լ� ����
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
