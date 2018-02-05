using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Quit();
    }

    public void LoadScene(string LevelName)
    {
        Time.timeScale = 1;
        ShipController.playerSpawnInfo = Vector3.zero;
        PlanetController.planetNames = null;
        PlanetController.planetPositions = null;
        SceneManager.LoadScene(LevelName, LoadSceneMode.Single);
        SceneManager.sceneLoaded += GameLoaded;
    }

    private void GameLoaded(Scene arg0, LoadSceneMode arg1)
    {

        SceneManager.sceneLoaded -= GameLoaded;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
