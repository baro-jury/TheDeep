using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AudioManager.instance.PlaySFX("Click");
        AudioManager.instance.musicSource.Stop();
    }

    public void openOption()
    {
        AudioManager.instance.PlaySFX("Click");
    }
    public void clickBack()
    {
        AudioManager.instance.PlaySFX("Click");
    }
    public void quitGame()
    {
        Application.Quit();
        AudioManager.instance.PlaySFX("Click");
    }
}
