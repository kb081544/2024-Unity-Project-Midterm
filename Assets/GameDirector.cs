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
                this.rouletteUI.GetComponent<TextMeshProUGUI>().text = "두근두근" + "\n";
                
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
                winning = "페라리";
                break;
            case 1:
                winning = "꽝";
                break;
            case 2:
                winning = "택시";
                break;
            case 3:
                winning = "꽝";
                break;
            case 4:
                winning = "나이키신발";
                break;
            case 5:
                winning = "꽝";
                break;
            default:
                winning = "페라리";
                break;
        }

        this.rouletteUI.GetComponent<TextMeshProUGUI>().text = winning + " 당첨!";
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

