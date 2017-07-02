using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour {
    public GameObject MenuLevel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadGame()
    {
        EditorSceneManager.LoadScene("MainScene");

    }

    public void openMenuLevel(string level)
    {
        MenuLevel.SetActive(true);
        MenuLevel.transform.FindChild("LevelName").GetComponent<Text>().text = "Beach nr." + level;
    }
}
