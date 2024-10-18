using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [Header("Characters")]
    public List<GameObject> characters = new List<GameObject>();
    private PlayerController player;

    [Header("Canvas")]
    public Image hpBar;
    public TextMeshProUGUI hpInfo;
    public Image shieldBar;
    public TextMeshProUGUI shieldInfo;
    public Button btPause;
    public GameObject panelPause;
    public Button btResume;
    public GameObject panelGameover;
    public Button btReplay;

    public Button[] btnsHome;

    void Start()
    {
        InitPlayer();

        hpBar.type = Image.Type.Filled;
        hpBar.fillMethod = Image.FillMethod.Horizontal;
        hpBar.fillOrigin = (int)Image.OriginHorizontal.Left;

        shieldBar.type = Image.Type.Filled;
        shieldBar.fillMethod = Image.FillMethod.Horizontal;
        shieldBar.fillOrigin = (int)Image.OriginHorizontal.Left;

        btPause.gameObject.SetActive(true);
        panelPause.gameObject.SetActive(false);
        panelGameover.gameObject.SetActive(false);

        RemoveButtonListener(btPause, btResume, btReplay);
        RemoveButtonListener(btnsHome);

        btPause.onClick.AddListener(Pause);
        btResume.onClick.AddListener(Resume);
        btReplay.onClick.AddListener(Replay);
        foreach (var btHome in btnsHome)
        {
            btHome.onClick.AddListener(GoHome);
        }

        Time.timeScale = 1;
    }

    void InitPlayer()
    {
        GameObject character = Instantiate(characters[0], transform);
        player = character.GetComponent<PlayerController>();
    }

    private void RemoveButtonListener(params Button[] buttons)
    {
        foreach (var button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    #region Canvas
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

    public void Replay()
    {
        player.playerData.isInitialized = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoHome()
    {
        print("way back homeeee");
        //player.playerData.isInitialized = false;
    }
    
    public void Gameover()
    {
        Time.timeScale = 0;
        panelGameover.gameObject.SetActive(true);
        //player.playerData.isInitialized = false;
    }

    #endregion
}
