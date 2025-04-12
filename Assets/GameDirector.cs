using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameDirector : MonoBehaviour
{
    GameObject roulette;
    GameObject rouletteUI;
    bool first = false;
    bool second = false;
    bool isSpinning = false; 
    public string winning;
    float duration;
    public Button gameStartButton;

    bool buttonVisible = false;


    void Start()
    {
        SceneManager.UnloadSceneAsync("GameScene");
        this.rouletteUI = GameObject.Find("RouletteUI");
        this.roulette = GameObject.Find("RouletteGame");
        this.gameStartButton = GameObject.Find("GameStartButton").GetComponent<Button>();

        this.gameStartButton.gameObject.SetActive(false);

        this.first = true;
        this.second = false;

        if (this.roulette == null)
        {
            Debug.LogError("Roulette GameObject not found!");
            return;
        }
        this.duration = roulette.GetComponent<RouletteController>().duration;

    }

    void Update()
    {
        if (this.roulette == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1.0f;
            if (first)
            {
                isSpinning = true;
                first = false;
                this.rouletteUI.GetComponent<TextMeshProUGUI>().text = "�αٵα�" + "\n";
                
            }
            else if (!second)
            {
                second = true;
                StartCoroutine(SpinRoulette());
            }
        }
    }

    IEnumerator SpinRoulette()
    {
        float timePassed = 0f;
        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            yield return null; 
        }

        float remainder = (roulette.transform.eulerAngles.z + 30) % 360;
        int result = Mathf.FloorToInt(remainder / 60);
        winning = " ";

        switch (result)
        {
            case 0:
                winning = "���";
                break;
            case 1:
                winning = "��";
                break;
            case 2:
                winning = "�ý�";
                break;
            case 3:
                winning = "��";
                break;
            case 4:
                winning = "����Ű�Ź�";
                break;
            case 5:
                winning = "��";
                break;
            default:
                winning = "���";
                break;
        }

        this.rouletteUI.GetComponent<TextMeshProUGUI>().text = winning + " ��÷!";
        isSpinning = false;
        PlayerPrefs.SetString("Player", winning);
        if (!buttonVisible && !isSpinning)
        {
            this.gameStartButton.gameObject.SetActive(true);
            this.gameStartButton.GetComponentInChildren<TextMeshProUGUI>().text = "Game Start!";
            buttonVisible = true;
        }
    }
}

