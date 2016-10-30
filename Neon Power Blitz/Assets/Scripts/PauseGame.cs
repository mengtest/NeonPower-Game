using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {

    // Update is called once per frame
    private bool pauseToggle; //toggle pause/resume stage
    private GameObject Player1;
    private GameObject Player2;
    private GameObject AudioManager;
    private SpriteRenderer PauseSprite;
    private AudioSource pauseSound;
    private bool paused;


    void Start()
    {
        pauseToggle = false;
        Player1 = GameObject.FindGameObjectWithTag("Player1");
        Player2 = GameObject.FindGameObjectWithTag("Player2");
        AudioManager = GameObject.FindGameObjectWithTag("BGM");
        PauseSprite = GameObject.Find("Life & Combo EventSystem").GetComponent<SpriteRenderer>();
        pauseSound = GameObject.Find("HURT-SOUND").GetComponent<AudioSource>();
        paused = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (pauseToggle)
                Resume();

            else
                Pause();

            pauseToggle = !pauseToggle;
        }
        if (paused == true && Input.GetButtonDown("ReturnToMenu"))
        {
            PauseSprite.enabled = false;
            Destroy(AudioManager);
            SceneManager.LoadScene(1);
        }
        if (Input.GetButtonDown("ResetScene"))
        {
            SceneManager.LoadScene(DetectDamage.currentLevel);
        }

    }

    void Pause()
    {
        pauseSound.Play();
        PauseSprite.enabled = true;
        paused = true;
        Player1.GetComponent<Unit01>().enabled = false;
        Player2.GetComponent<Unit02>().enabled = false;
        Time.timeScale = 0;
    }

    void Resume()
    {
        pauseSound.Play();
        PauseSprite.enabled = false;
        paused = false;
        Player1.GetComponent<Unit01>().enabled = true;
        Player2.GetComponent<Unit02>().enabled = true;
        Time.timeScale = 1;
    }
}
