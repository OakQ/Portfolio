using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    bool paused;
    bool pressed;
    public GameObject pauseText;
    public GameObject instructionsShow;
    public GameObject instructionsHide;
    public GameObject instructionsText;
    public GameObject mainMenu;
	// Use this for initialization
	void Start () {
        Time.timeScale = 0.0f;
        paused = true;
        pressed = false;
        pauseText.SetActive(true);
        instructionsShow.SetActive(false);
        instructionsHide.SetActive(true);
        instructionsText.SetActive(true);
        mainMenu.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused && !pressed)
        {
            Time.timeScale = 0.0f;
            paused = true;
            pressed = true;
            pauseText.SetActive(true);
            instructionsShow.SetActive(true);
            mainMenu.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && paused && !pressed)
        {
            Time.timeScale = 1.0f;
            paused = false;
            pressed = true;
            pauseText.SetActive(false);
            instructionsShow.SetActive(false);
            instructionsHide.SetActive(false);
            instructionsText.SetActive(false);
            mainMenu.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pressed = false;
        }
	}
}
