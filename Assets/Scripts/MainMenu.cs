using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {


    public void onStartClicked()
    {
        SceneManager.LoadSceneAsync("MainLevel");
    }

    public void onCreditsClicked()
    {

    }

    public void onQuitClicked()
    {
        Application.Quit();
    }
    
    

}
