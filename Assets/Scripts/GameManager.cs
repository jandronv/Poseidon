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
            Debug.Log("SUNSET IS HERE");

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