using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class FallingAssetsController : MonoBehaviour
{
    GameObject human;
    GameObject humanShoes;
    GameObject taxi;
    GameObject ferrari;
    GameObject point;
    string[] movingAssets = { "human_with_basket", "human_with_basket_with_shoes", "taxi", "ferrari" };
    string[] fallingAssets = { "poop", "grape", "banana", "goldbar", "strawberry", "watermelon" };
    GameObject gameDirector;

    float leftXCoordinate;
    float rightXCoordinate;
    float topYCoordinate;
    float bottomYCoordinate;
    private AudioSource audioSource;
    public struct AssetInfo
    {
        public int point;
        public float speed;
        public float rotationSpeed;

        public AssetInfo(int point, float speed, float rotationSpeed)
        {
            this.point = point;
            this.speed = speed;
            this.rotationSpeed = rotationSpeed;
        }
    }

    // 에셋 정보를 담은 딕셔너리
    public Dictionary<string, AssetInfo> assetInfo = new Dictionary<string, AssetInfo>
    {
        { "poop", new AssetInfo(point: -50, speed: 0.01f, rotationSpeed: 0f) },
        { "watermelon", new AssetInfo(point: 10, speed: 0.001f, rotationSpeed: 0f) },
        { "grape", new AssetInfo(point: 20, speed: 0.002f, rotationSpeed: 0f) },
        { "banana", new AssetInfo(point: 30, speed: 0.004f, rotationSpeed: 0f) },
        { "strawberry", new AssetInfo(point: 50, speed: 0.005f, rotationSpeed: 0f) },
        { "goldbar", new AssetInfo(point: 100, speed: 0.01f, rotationSpeed: 0.1f) }
    };

    // Start is called before the first frame update
    void Start()
    {

        this.human = GameObject.Find(movingAssets[0]);
        this.humanShoes = GameObject.Find(movingAssets[1]);
        this.taxi = GameObject.Find(movingAssets[2]);
        this.ferrari = GameObject.Find(movingAssets[3]);
        gameDirector = GameObject.Find("GameDirector2");

/*        this.poop = GameObject.Find(fallingAssets[0]);
        this.grape = GameObject.Find(fallingAssets[1]);
        this.banana = GameObject.Find(fallingAssets[2]);
        this.goldbar = GameObject.Find(fallingAssets[3]);
        this.strawberry = GameObject.Find(fallingAssets[4]);
        this.watermelon = GameObject.Find(fallingAssets[5]);*/


    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            foreach (string assetName in assetInfo.Keys)
            {
                GameObject asset = GameObject.Find(assetName);
                if (asset != null)
                {
                    AssetInfo assetData = assetInfo[assetName];
                    GenerateFallingAssets(asset, assetData.speed, assetData.rotationSpeed);
                   
                }
                else
                {
                    Debug.LogError(assetName + "을(를) 찾을 수 없습니다.");
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string assetName = collision.gameObject.name;
        if (assetInfo.ContainsKey(assetName))
        {
            AssetInfo info = assetInfo[assetName];
            gameDirector = GameObject.Find("GameDirector2");
            if (gameDirector != null)
            {
                gameDirector.GetComponent<GameDirector2>().IncreasePoints(info.point);
                audioSource = gameDirector.GetComponent<AudioSource>();
                audioSource.Play();
            }
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Array.Exists(fallingAssets, name => name == collision.gameObject.name))
        {
            Destroy(collision.gameObject);
        }
    }

    private void GenerateFallingAssets(GameObject gameObject, float speed, float rotationSpeed)
    {
        // 랜덤한 x 위치 설정
        float randomX = UnityEngine.Random.Range(leftXCoordinate, rightXCoordinate);

        // 에셋의 위치를 설정하고 속도를 적용합니다.
        gameObject.transform.position = new Vector3(randomX, topYCoordinate + 2, 0f);

        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(0, -speed);
        rigidbody.angularVelocity = rotationSpeed;
        rigidbody.isKinematic = false;
    }

}


