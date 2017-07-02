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
    public int chargeClicks;
    public int waveCharge;
    public int totalClicksWave=0;

    public GameObject MenuGameOver;

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
        totalClicks = 0;
        totalClicksWave = 0;
        for (int i = 0; i < castillos.Length; i++)
        {
            totalClicks += castillos[i].clicks;
            totalClicksWave += castillos[i].clicksWave;
        }

        chargeWave();

        for (int i=0; i < castillos.Length; i++)
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
            
            MenuGameOver.transform.FindChild("LevelResult").GetComponent<Text>().text="Victory!";
            Debug.Log("SUNSET IS HERE" +totalClicks);
        }
        else if (cause == "kidswin")
        {
            Debug.Log("JAJAJ LOL XD NOOB");

            MenuGameOver.transform.FindChild("LevelResult").GetComponent<Text>().text = "You lose!";
        }

        MenuGameOver.transform.FindChild("Castles").transform.FindChild("Text").GetComponent<Text>().text = CM.numCastlesDestroyed.ToString();
        MenuGameOver.transform.FindChild("Seashells").transform.FindChild("Text").GetComponent<Text>().text = "25";

        GameObject.Find("CanvasGameOver").GetComponent<Canvas>().enabled = true;

        while (!Input.GetMouseButtonDown(0))
        {

            yield return StartCoroutine(WaitForKeyDown(Input.GetMouseButtonDown(0)));
        }
        

    }

    protected IEnumerator WaitForKeyDown(bool mousePressed)
    {

        yield return null;

    }


    public void chargeWave()
    {
         chargeClicks = totalClicksWave;
        if (chargeClicks <10)
        {
            //No carga
            waveCharge = 0;
            GameObject.Find("Wave1").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Wave2").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Wave3").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Wave4").GetComponent<SpriteRenderer>().enabled = false;

        }
        else if (chargeClicks >= 10 && chargeClicks < 20)
        {
            //sprite2 activo
            waveCharge = 1;
            GameObject.Find("Wave1").GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (chargeClicks >= 20 && chargeClicks < 30)
        {

            waveCharge = 2;

            GameObject.Find("Wave2").GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (chargeClicks >= 30 && chargeClicks < 40)
        {

            waveCharge = 3;

            GameObject.Find("Wave3").GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (chargeClicks >= 40 && chargeClicks <= 50)
        {

            waveCharge = 4;

            GameObject.Find("Wave4").GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void reinitiateClicksWave()
    {
        for (int i = 0; i < castillos.Length; i++)
        {
             castillos[i].clicksWave=0;
        }
    }


    public void restartGame()
    {

        Time.timeScale = 1.0f;
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().name);
    }

    public void exitGame()
    {

        Time.timeScale = 1.0f;
        EditorSceneManager.LoadScene("StartScene");
    }
}