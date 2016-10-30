using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour {

    public string nextScene; //playable scene

    public GameObject loadingScreenBG;
    public Slider progBar;
    public Image startUI;
    public Image controlUI;
    public Image quitUI;
    public Image copyrightUI;
    public GameObject controlSplash;
    public AudioSource selectionSound;

    private GameObject particleSys;

    public bool isFakeLoadingBar = false;
    private bool startGame = false;
    public float fakeIncrement = 0f;
    public float fakeTiming = 0f;
    void Start()
    {

        particleSys = GameObject.Find("Particle System");
        Time.timeScale = 1;
        progBar.value = 0;
        var am = GameObject.Find("AudioManager");
        if (am != null)
        {
            am.GetComponent<AudioSource>().enabled = false;
        }

    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel")&& startGame==false)
        {
            particleSys.SetActive(true);
            controlUI.gameObject.SetActive(true);
            startUI.gameObject.SetActive(true);
            quitUI.gameObject.SetActive(true);
            copyrightUI.gameObject.SetActive(true);
            controlSplash.SetActive(false);
        }
    }

    public void StartGame()
    {
        particleSys.SetActive(false);
        startGame = true;
        selectionSound.Play();
        loadingScreenBG.SetActive(true);
        progBar.gameObject.SetActive(true);

        controlUI.gameObject.SetActive(false);
        startUI.gameObject.SetActive(false);
        quitUI.gameObject.SetActive(false);
        copyrightUI.gameObject.SetActive(false);

        if (isFakeLoadingBar == true)
        {
            StartCoroutine(LoadLevelWithFakeProgress());
        }
    }
    public void ShowControls()
    {
        particleSys.SetActive(false);
        selectionSound.Play();
        controlUI.gameObject.SetActive(false);
        startUI.gameObject.SetActive(false);
        quitUI.gameObject.SetActive(false);
        copyrightUI.gameObject.SetActive(false);
        controlSplash.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator LoadLevelWithFakeProgress()
    {
        yield return new WaitForSeconds(1);

        while (progBar.value != 1f)
        {
            {
                //print("Progress: " + progBar.value);
                progBar.value += fakeIncrement;
                yield return new WaitForSeconds(fakeTiming);
            }

            if (progBar.value == 1f)
            {
                //print("Finished: " + progBar.value);
                SceneManager.LoadScene(nextScene);
            }
            yield return null;
        }
    }
}
