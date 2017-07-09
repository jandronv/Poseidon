using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public GameObject MenuLevel;
    public ToggleGroup TG;
    // Use this for initialization
    void Start()
    {


        unlockLevels();


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadGame()
    {

        SceneManager.LoadScene("MainScene");

    }

    public void openMenuLevel(string level)
    {
        MenuLevel.SetActive(true);
        PlayerPrefs.SetInt("actualLevel", int.Parse(level));
        MenuLevel.transform.Find("LevelName").GetComponent<Text>().text = "Beach nr." + level;
        if(PlayerPrefs.GetString("activeSkill")!="")
            TG.transform.FindChild(PlayerPrefs.GetString("activeSkill")).GetComponent<Toggle>().isOn = true;
    }

    public void setParametersLevel(string columnrows)
    {
        //Revisar esto, check a web 
        //http://answers.unity3d.com/questions/923959/ui-system-multiple-ints-as-function-arguments.html

        string[] columnrowssplit = columnrows.Split('-');
        PlayerPrefs.SetInt("columns", int.Parse(columnrowssplit[0]));
        PlayerPrefs.SetInt("rows", int.Parse(columnrowssplit[1]));
    }

    public void unlockLevels()
    {
        for (int i = 2; i <= 15; i++)
        {
            if (PlayerPrefs.GetString(i.ToString()) == "unlocked")
            {
                Debug.Log("Level (" + i + "), unlocked");
                GameObject.Find("Level (" + i + ")").GetComponent<Button>().interactable = true;
                Color c = new Color(255f, 255f, 255f, 255f);
                GameObject.Find("Level (" + i + ")").GetComponent<Image>().color = c;
            }
        }
    }

    public void setSkill(string skill)
    {
        Debug.Log("Se ha activado " + skill);
        PlayerPrefs.SetString("activeSkill", skill);
    }
}
