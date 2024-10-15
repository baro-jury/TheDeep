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
    public Dictionary<int, GameObject> characterDictionary = new Dictionary<int, GameObject>();

    [Header("Canvas")]
    public Image hpBar;
    public TextMeshProUGUI hpInfo;
    public Image shieldBar;
    public TextMeshProUGUI shieldInfo;
    public Button btPause, btResume;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            characterDictionary.Add(i + 1, characters[i]);
        }
        Instantiate(characterDictionary[1], transform);

        hpBar.type = Image.Type.Filled;
        hpBar.fillMethod = Image.FillMethod.Horizontal;
        hpBar.fillOrigin = (int)Image.OriginHorizontal.Left;

        shieldBar.type = Image.Type.Filled;
        shieldBar.fillMethod = Image.FillMethod.Horizontal;
        shieldBar.fillOrigin = (int)Image.OriginHorizontal.Left;

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
}
