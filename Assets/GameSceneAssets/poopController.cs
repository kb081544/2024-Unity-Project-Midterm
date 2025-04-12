using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poopController : MonoBehaviour
{
    float leftXCoordinate;
    float rightXCoordinate;
    float topYCoordinate;
    float bottomYCoordinate;
    GameObject gameDirector;

    // Start is called before the first frame update
    void Start()
    {
        gameDirector = GameObject.Find("GameDirector2");

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

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < bottomYCoordinate)
        {
            Destroy(gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string[] movingAssets = { "human_with_basket", "human_with_basket_with_shoes", "taxi", "ferrari" };

        foreach (string assetName in movingAssets)
        {
            if (collision.gameObject.name == assetName)
            {
                gameDirector.GetComponent<GameDirector2>().IncreasePoints(-50);
                Destroy(gameObject);
            }
        }
    }
}
