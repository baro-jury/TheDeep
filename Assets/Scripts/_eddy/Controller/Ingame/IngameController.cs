using System.Collections;
using System.Collections.Generic;
using TMPro;
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
            //DontDestroyOnLoad(this);
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

    [Header("Characters")]
    public List<GameObject> characters = new List<GameObject>();

    [Header("Canvas")]
    public Image hpBar;
    public TextMeshProUGUI hpInfo;
    public Image shieldBar;
    public TextMeshProUGUI shieldInfo;
    public Button btPause;
    public GameObject panelPause;
    public Button btResume;
    public Button btHome;

    void Start()
    {
        Instantiate(characters[0], transform);

        hpBar.type = Image.Type.Filled;
        hpBar.fillMethod = Image.FillMethod.Horizontal;
        hpBar.fillOrigin = (int)Image.OriginHorizontal.Left;

        shieldBar.type = Image.Type.Filled;
        shieldBar.fillMethod = Image.FillMethod.Horizontal;
        shieldBar.fillOrigin = (int)Image.OriginHorizontal.Left;

        btPause.gameObject.SetActive(true);
        panelPause.gameObject.SetActive(false);

        RemoveButtonListener(btPause, btResume, btHome);

        btPause.onClick.AddListener(Pause);
        btResume.onClick.AddListener(Resume);
        btHome.onClick.AddListener(GoHome);
    }

    private void RemoveButtonListener(params Button[] buttons)
    {
        foreach (var button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    #region Canvas
    public void Pause()
    {
        Time.timeScale = 0;
        panelPause.gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        panelPause.gameObject.SetActive(false);
    }

    public void GoHome()
    {
        print("way back homeeee");
    }

    public void SetHP(float currentHP, float maxHP)
    {
        hpBar.fillAmount = currentHP / maxHP;
        hpInfo.text = (int)currentHP + "/" + (int)maxHP;
    }

    public void SetShield(float currentShield, float maxShield)
    {
        shieldBar.fillAmount = currentShield / maxShield;
        shieldInfo.text = (int)currentShield + "/" + (int)maxShield;
    } 
    #endregion
}
