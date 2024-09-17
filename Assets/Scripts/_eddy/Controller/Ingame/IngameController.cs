using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameController : MonoBehaviour
{
    public static IngameController instance;

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    void Awake()
    {
        MakeInstance();
    }

    [Header("Canvas")]
    public Image[] hearts;
    public Button btPause, btResume;

    // Start is called before the first frame update
    void Start()
    {
        btPause.gameObject.SetActive(true);
        btResume.gameObject.SetActive(false);

        RemoveButtonListener(btPause, btResume);

        btPause.onClick.AddListener(Pause);
        btResume.onClick.AddListener(Resume);

    }

    private void RemoveButtonListener(params Button[] buttons)
    {
        foreach (var button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    void Pause()
    {
        Time.timeScale = 0;
        btResume.gameObject.SetActive(true);
    }

    void Resume()
    {
        Time.timeScale = 1;
        btResume.gameObject.SetActive(false);
    }
}
