using System.Collections;
using UnityEngine;

public class RouletteController : MonoBehaviour
{
    public float rotSpeed;
    bool isRotating = false;
    bool isSlowingDown = false;
    public float duration = 1f;
    // Start is called before the first frame update
    void Start()
    {
        // Initial rotation speed
        rotSpeed = 300f * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isRotating)
            {
                StartCoroutine(SpinRoulette());
            }
            else if (!isSlowingDown)
            {
                isSlowingDown = true;
                StartCoroutine(SlowDownRoulette());
            }
        }

        if (isRotating)
        {
            transform.Rotate(0, 0, rotSpeed);
        }
    }

    IEnumerator SpinRoulette()
    {
        isRotating = true;
        while (isRotating)
        {
            yield return null;
        }
    }

    IEnumerator SlowDownRoulette()
    {
        float initialRotSpeed = rotSpeed;
        float targetRotSpeed = 0f;
        float timePassed = 0f;

        while (timePassed < duration)
        {
            float t = timePassed / duration;
            rotSpeed = Mathf.Lerp(initialRotSpeed, targetRotSpeed, t);
            timePassed += Time.deltaTime;
            yield return null;
        }

        isRotating = false;
        isSlowingDown = false;

        // Calculate result based on final rotation angle
        float remainder = (transform.eulerAngles.z + 30) % 360;
        int result = Mathf.FloorToInt(remainder / 60);
        switch (result)
        {
            case 0:
                Debug.Log("其扼府");
                break;
            case 1:
                Debug.Log("参");
                break;
            case 2:
                Debug.Log("琶矫");
                break;
            case 3:
                Debug.Log("参");
                break;
            case 4:
                Debug.Log("唱捞虐脚惯");
                break;
            case 5:
                Debug.Log("参");
                break;
            default:
                Debug.Log("其扼府");
                break;
        }
    }
}
