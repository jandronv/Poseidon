using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.
using UnityEditor.SceneManagement;


public class GameManager : MonoBehaviour
{
    public Slider SunSlider;
    public float MaxTime = 20f;
    public float ActiveTime = 0f;
    bool end = false;

    public CastleManager CM;
    private CastilloArena[] castillos;
    private GameObject CA;

    [Tooltip("Cantidad de castillos completos con los que se acabaría el juego")]
    public int castleToGameover;

    [Tooltip("Cantidad de castillos completados en el momento")]
    private int completedCastles=0;
    // Use this for initialization

    int totalClicks=0;

    public Image sunsetBG;

    void Start()
    {
        CM.CreateGrid();
        SunSlider.value = 0;
        
        CA = GameObject.FindGameObjectWithTag("Castillos");
        castillos = CA.GetComponentsInChildren<CastilloArena>();
    }

    public void Update()
    {
        for(int i=0; i < castillos.Length; i++)
        {
            if (castillos[i].CastilloTerminado == true)
            {
                completedCastles++;
            }
        }

        ActiveTime += Time.deltaTime;
        var percent = ActiveTime / MaxTime;
        SunSlider.value = Mathf.Lerp(0, 1, percent);
        Color c = sunsetBG.color;
        c.a= Mathf.Lerp(0, 0.4f, percent);
        sunsetBG.color = c;

        if (SunSlider.value >= 1 && end == false)
        {
            end = true;
            StartCoroutine(GameOver("timeup"));
        }else if (completedCastles == 3 & end == false)
        {
            end = true;
            StartCoroutine(GameOver("kidswin"));
        }

        completedCastles = 0;
    }

    protected IEnumerator GameOver(string cause)
    {
        Time.timeScale = 0f;
        if (cause == "timeup")
        {
            for (int i = 0; i < castillos.Length; i++)
            {
                totalClicks += castillos[i].clicks;
            }

            Debug.Log("SUNSET IS HERE" +totalClicks);
        }
        else if (cause == "kidswin")
        {
            Debug.Log("JAJAJ LOL XD NOOB");

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