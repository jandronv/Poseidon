using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.
using UnityEditor.SceneManagement;


public class GameManager : MonoBehaviour {
    public Slider SunSlider;
    public float MaxTime = 20f;
    public float ActiveTime = 0f;
    bool end = false;
    // Use this for initialization
    void Start () {
        SunSlider.value = 0;
	}

    public void Update()
    {
        ActiveTime += Time.deltaTime;
        var percent = ActiveTime / MaxTime;
        SunSlider.value = Mathf.Lerp(0, 1, percent);

        if (SunSlider.value >= 1 && end==false)
        {
            end = true;
            StartCoroutine(GameOver("timeup"));
        }
    }

    protected IEnumerator GameOver(string cause)
    {
        Time.timeScale = 0f;
        if (cause == "timeup")
        {
            Debug.Log("SUNSET IS HERE");

        }
        else if(cause=="kidsWin")
        {

        }



        while (!Input.GetMouseButtonDown(0))
        {

            yield return StartCoroutine(WaitForKeyDown(Input.GetMouseButtonDown(0)));
        }

        restartGame();

    }

    protected IEnumerator WaitForKeyDown(bool mousePressed)
    {

        yield return null;

    }
    public void restartGame()
    {

        Time.timeScale = 1.0f;
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().name);
    }
}
