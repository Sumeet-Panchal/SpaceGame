using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

    public Transform player;
    public GameObject DistCanvas;
    public static bool paused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) resume();
    }

    private void Start()
    {
        if (!paused)
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
            DistCanvas.SetActive(true);
            Time.timeScale = 1;
            paused = false;
        }
    }

    public void pause()
    {
        gameObject.SetActive(true);
        DistCanvas.SetActive(false);
        Time.timeScale = 0;
        paused = true;
    }

    public void resume()
    {
        gameObject.SetActive(false);
        DistCanvas.SetActive(true);
        Time.timeScale = 1;
        paused = false;
    }


    public void exit()
    {
        SceneManager.LoadScene("MainMenu");
        ShipController.playerSpawnInfo = new Vector3(player.position.x, player.position.y, player.rotation.eulerAngles.z);
    }
}
