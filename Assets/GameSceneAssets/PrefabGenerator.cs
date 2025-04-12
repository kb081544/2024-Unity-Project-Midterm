using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrefabGenerator : MonoBehaviour
{
    public GameObject poopPrefab;
    public GameObject grapePrefab;
    public GameObject bananaPrefab;
    public GameObject goldbarPrefab;
    public GameObject strawberryPrefab;
    public GameObject watermelonPrefab;
    float leftXCoordinate;
    float rightXCoordinate;
    float topYCoordinate;
    float bottomYCoordinate;
    string[] fallingAssets = { "poop", "grape", "banana", "goldbar", "strawberry", "watermelon" };
    float poopDelta;
    float grapeDelta;
    float bananaDelta;
    float goldbarDelta;
    float strawberryDelta;
    float watermelonDelta;


    // Start is called before the first frame update
    void Start()
    {
        this.poopPrefab= GameObject.Find(fallingAssets[0]);
        this.grapePrefab = GameObject.Find(fallingAssets[1]);
        this.bananaPrefab = GameObject.Find(fallingAssets[2]);
        this.goldbarPrefab=GameObject.Find(fallingAssets[3]);
        this.strawberryPrefab = GameObject.Find(fallingAssets[4]);
        this.watermelonPrefab = GameObject.Find(fallingAssets[5]);
        Camera mainCamera = Camera.main;

        // 카메라의 시야 영역을 월드 좌표로 변환
        Vector3 cameraMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 cameraMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));


        // 상하좌우의 좌표를 저장하는 변수
        leftXCoordinate = cameraMin.x;
        rightXCoordinate = cameraMax.x;
        topYCoordinate = cameraMax.y;
        bottomYCoordinate = cameraMin.y;
    }
    void Update()
    {
        // GameScene일 때만 실행
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            poopGenerator();
            grapeGenerator();
            bananaGenerator();
            watermelonGenerator();
            strawberryGenerator();
            goldbarGenerator();
        }
    }
    IEnumerator DestroyWhenOutOfBounds(GameObject gameObject)
    {
        while (true)
        {
            // 에셋이 화면 밖으로 나갔는지 확인하고, 나갔다면 파괴합니다.
            if (gameObject.transform.position.y < bottomYCoordinate)
            {
                Destroy(gameObject);
                break; // 파괴 후 루프를 종료합니다.
            }
            yield return null; // 한 프레임을 기다립니다.
        }
    }
    void poopGenerator()
    {
        float poopSpan = UnityEngine.Random.Range(2.0f,4.0f);
        poopDelta += Time.deltaTime;
        if (poopDelta > poopSpan)
        {
            poopDelta = 0;
            GameObject temp = Instantiate(poopPrefab) as GameObject;
            float px= UnityEngine.Random.Range(leftXCoordinate, rightXCoordinate);
            temp.transform.position = new Vector3(px, topYCoordinate+2, 0);
            DestroyWhenOutOfBounds(temp);
        }
    }
    void grapeGenerator()
    {
        float grapeSpan = UnityEngine.Random.Range(1.0f, 3.0f);
        grapeDelta += Time.deltaTime;
        if (grapeDelta > grapeSpan)
        {
            grapeDelta = 0;
            GameObject temp = Instantiate(grapePrefab) as GameObject;
            float px = UnityEngine.Random.Range(leftXCoordinate, rightXCoordinate);
            temp.transform.position = new Vector3(px, topYCoordinate+2, 0);
            DestroyWhenOutOfBounds(temp);


        }
    }
    void watermelonGenerator()
    {
        float watermelonSpan = UnityEngine.Random.Range(1.0f, 2.5f);
        watermelonDelta += Time.deltaTime;
        if (watermelonDelta > watermelonSpan)
        {
            watermelonDelta = 0;
            GameObject temp = Instantiate(watermelonPrefab) as GameObject;
            float px = UnityEngine.Random.Range(leftXCoordinate, rightXCoordinate);
            temp.transform.position = new Vector3(px, topYCoordinate+2, 0);
            DestroyWhenOutOfBounds(temp);

        }
    }
    void bananaGenerator()
    {
        float bananSpan = UnityEngine.Random.Range(1.5f, 3.0f);
        bananaDelta += Time.deltaTime;
        if (bananaDelta > bananSpan)
        {
            bananaDelta = 0;
            GameObject temp = Instantiate(bananaPrefab) as GameObject;
            float px = UnityEngine.Random.Range(leftXCoordinate, rightXCoordinate);
            temp.transform.position = new Vector3(px, topYCoordinate+2, 0);
            DestroyWhenOutOfBounds(temp);

        }
    }
    void strawberryGenerator()
    {
        float strawberrySpan = UnityEngine.Random.Range(2.0f,3.0f);
        strawberryDelta += Time.deltaTime;
        if (strawberryDelta > strawberrySpan)
        {
            strawberryDelta = 0;
            GameObject temp = Instantiate(strawberryPrefab) as GameObject;
            float px = UnityEngine.Random.Range(leftXCoordinate, rightXCoordinate);
            temp.transform.position = new Vector3(px, topYCoordinate+2, 0);
            DestroyWhenOutOfBounds(temp);

        }
    }
    void goldbarGenerator()
    {
        float goldbarSpan = UnityEngine.Random.Range(3.0f, 5.0f);
        goldbarDelta += Time.deltaTime;
        if (goldbarDelta > goldbarSpan)
        {
            goldbarDelta = 0;
            GameObject temp = Instantiate(goldbarPrefab) as GameObject;
            float px = UnityEngine.Random.Range(leftXCoordinate, rightXCoordinate);
            temp.transform.position = new Vector3(px, topYCoordinate+2, 0);
            temp.transform.Translate(0, -0.1f, 0, Space.World);
            DestroyWhenOutOfBounds(temp);

        }
    }
}
