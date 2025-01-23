using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class main_menu : MonoBehaviour {

	public void Level_1()
	{
        SceneManager.LoadScene("Level 1");
	}
    public void Level_2()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void Exit()
    {
        Application.Quit();
    }


}
