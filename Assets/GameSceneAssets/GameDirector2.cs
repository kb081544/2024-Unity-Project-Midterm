using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameDirector2 : MonoBehaviour
{
    GameObject countdown;
    GameObject lefttime;
    GameObject progressbar;
    GameObject point;
    GameObject poop;
    GameObject grape;
    GameObject goldbar;
    GameObject watermelon;
    GameObject strawberry;
    GameObject banana;
    GameObject human;
    GameObject humanShoes;
    GameObject taxi;
    GameObject ferrari;
    GameObject sounds;
    float timer;
    string p;
    float span = 15.0f; //�÷��� �ð�
    private GameDirector player;
    bool started = false;
    public int finalScore =0;
    bool temp = true;
    float leftXCoordinate;
    float rightXCoordinate;
    float topYCoordinate;
    float bottomYCoordinate;

    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.UnloadSceneAsync("RouletteScene");
        this.countdown = GameObject.Find("Timer");

        // Find game objects
        this.poop = GameObject.Find("poop");
        this.grape = GameObject.Find("grape");
        this.watermelon = GameObject.Find("watermelon");
        this.strawberry = GameObject.Find("strawberry");
        this.banana = GameObject.Find("banana");
        this.goldbar = GameObject.Find("goldbar");
        this.human = GameObject.Find("human_with_basket");
        this.humanShoes = GameObject.Find("human_with_basket_with_shoes");
        this.taxi = GameObject.Find("taxi");
        this.ferrari = GameObject.Find("ferrari");
        this.progressbar = GameObject.Find("ProgressBar");
        this.point = GameObject.Find("Point");
        this.lefttime = GameObject.Find("LeftTime");
        this.sounds = GameObject.Find("Sounds");
        //�ӽ÷� �־��
        //this.p = "���";

        // Get player selection
        //���� ������ �� �̰� Ȱ��ȭ ���Ѿ� �� 
        this.p = PlayerPrefs.GetString("Player");
        Camera mainCamera = Camera.main;

        // ī�޶��� �þ� ������ ���� ��ǥ�� ��ȯ
        Vector3 cameraMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 cameraMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));


        // �����¿��� ��ǥ�� �����ϴ� ����
        leftXCoordinate = cameraMin.x;
        rightXCoordinate = cameraMax.x;
        topYCoordinate = cameraMax.y;
        bottomYCoordinate = cameraMin.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (temp)
        {
            StartCoroutine(StartCountdown(3.0f));
            temp = false;  
        }

       if (this.p == "��")
        {
            this.human.SetActive(true);
            this.humanShoes.SetActive(false);
            this.taxi.SetActive(false);
            this.ferrari.SetActive(false);
        }
        else if (this.p == "�ý�")
        {
            this.human.SetActive(false);
            this.humanShoes.SetActive(false);
            this.taxi.SetActive(true);
            this.ferrari.SetActive(false);
        }
        else if (this.p == "���")
        {
            this.human.SetActive(false);
            this.humanShoes.SetActive(false);
            this.taxi.SetActive(false);
            this.ferrari.SetActive(true);
        }
        else if (this.p == "����Ű�Ź�")
        {
            this.human.SetActive(false);
            this.humanShoes.SetActive(true);
            this.taxi.SetActive(false);
            this.ferrari.SetActive(false);
        }
       if(started)
        {
            PlayTime();

        }


    }

    IEnumerator StartCountdown(float countdownTime)
    {
        Time.timeScale = 0f;
        float currentTime = countdownTime;
        yield return new WaitForSecondsRealtime(1.0f);
        while (currentTime > 0)
            {
            this.countdown.GetComponent<TextMeshProUGUI>().text = currentTime.ToString();
            this.sounds.GetComponent<SoundManager>().PlayMusic2();
            yield return new WaitForSecondsRealtime(1.0f);
            
            currentTime--;
            }

            // Countdown finished, start the game
       this.countdown.GetComponent<TextMeshProUGUI>().text = "Start!";
        this.sounds.GetComponent<SoundManager>().PlayMusic2();
        Time.timeScale = 1f;

        yield return new WaitForSecondsRealtime(1.0f);
            this.countdown.GetComponent<TextMeshProUGUI>().text = " ";
            started = true;
            currentTime = this.span;
            while (currentTime > 0)
            {
                this.lefttime.GetComponent<TextMeshProUGUI>().text = currentTime.ToString();
                yield return new WaitForSeconds(1.0f);
                currentTime--;
            }
            this.lefttime.GetComponent<TextMeshProUGUI>().text = "0";
            this.countdown.GetComponent<TextMeshProUGUI>().text = "Finish!" + '\n' + "����: " + finalScore.ToString();
        this.sounds.GetComponent<SoundManager>().PlayWhistle2();
        Time.timeScale = 0f;
        started = true;
        

    }

    public void PlayTime()
    {
        this.progressbar.GetComponent<Image>().fillAmount -= Time.deltaTime / span;
    }
    public void IncreasePoints(int point)
    {
        finalScore = finalScore + point;
        this.point.GetComponent<TextMeshProUGUI>().text =finalScore.ToString();
    }
    void CheckOutOfBoundsAndDestroy(GameObject gameObject)
    {
        if (gameObject.transform.position.y < bottomYCoordinate)
        {
            Destroy(gameObject);
        }
    }

}
