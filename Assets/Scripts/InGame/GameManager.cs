using SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    #region SingleTon
    private static GameManager instance = null;

    public static GameManager Inst
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    instance = new GameObject().AddComponent<GameManager>();
                }
            }

            return instance;
        }
    }
    #endregion

    #region INFO
    [Header("Images")]
    [SerializeField] private Sprite[] Crystals = null;
    [SerializeField] private Sprite[] Picks = null;
    [SerializeField] private Sprite InteractImg = null;
    [SerializeField] private Sprite NotInteractImg = null;
    [SerializeField] private Image pickImg = null;

    [Header("Button")]
    [SerializeField] private Button[] levels = null;
    [SerializeField] private Button crystal = null;
    [SerializeField] private Button upgradeBtn = null;

    [Header("Canvases")]
    [SerializeField] private Canvas PauseCanvas = null;
    [SerializeField] private Canvas SelectStageCanvas = null;
    [SerializeField] private GameObject UpgradeCanvas = null;
    [SerializeField] private Canvas EndingCanvas = null;

    [Header("Scriptable")]
    [SerializeField] private CrystalScriptable[] CrystalDatas = null;
    [SerializeField] private PickScriptable[] PickDatas = null;

    [Header("TEXT")]
    [SerializeField] private TextMeshProUGUI stageInfo = null;
    [SerializeField] private TextMeshProUGUI goldInfo = null;
    [SerializeField] private TextMeshProUGUI upgradeGold = null;
    [SerializeField] private TextMeshProUGUI timer = null;
    [SerializeField] private TextMeshProUGUI hpInfo = null;

    [Header("Slider")]
    [SerializeField] private Slider slider = null;

    [Header("Effect")]
    [SerializeField] private GameObject effect = null;

    #endregion
    public Canvas GetPauseCanvas { get { return PauseCanvas; } }
    public Canvas GetSelectStageCanvas { get { return SelectStageCanvas; } }

    #region Pause & Play
    public delegate void TimeControl();
    public TimeControl TimePause = () =>
    {
        Time.timeScale = 0f;
    };
    public TimeControl TimePlay = () =>
    {
        Time.timeScale = 1f;
    };
    #endregion

    public int MyLevel { get; private set; } = 1;
    public int MyPickLevel { get; private set; } = 1;
    [HideInInspector] public CrystalScriptable myCrystal = null;
    [HideInInspector] public PickScriptable myPick = null;
    private Image crystalImg = null;
    private float maxHP = 0;
    private float curHP = 0;
    private int gold = 0;
    private float time = 0;
    private RectTransform crystalTrans = null;

    private Save save = null;
    private Load load = null;
    private SaveData data;
    public Save GetSave() { return save; }
    [SerializeField] private Camera cam = null;
    private Vector3 startPos;

    private void Awake()
    {
        //���̺� ������ �� �ڸ�
        save = new Save();
        load = new Load();

        data = load.Start();

        MyLevel = data.level;
        MyPickLevel = data.pick;
        gold = data.gold;

        crystalTrans = crystal.GetComponent<RectTransform>();
        startPos = crystalTrans.position;

        if (MyLevel == 0)
        {
            MyLevel = 1;
        }

        if (MyPickLevel == 0)
        {
            MyPickLevel = 1;
        }

        crystal.TryGetComponent<Image>(out crystalImg);

        for (int i = 0; i < MyLevel; i++)
        {
            Image img = null;
            if (levels[i].TryGetComponent<Image>(out img) == true)
            {
                img.sprite = InteractImg;
                levels[i].interactable = true;
            }
            else
            {
                Debug.LogError("Not Found Image");
            }
        }

        myPick = PickDatas[MyPickLevel - 1];

        InitLevel(MyLevel);
    }

    private void Update()
    {
        if (time <= 0f)
        {
            InitLevel(MyLevel);
            return;
        }

        time -= Time.deltaTime;

        timer.text = "TIMER \n" + ((int)time).ToString();



        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            if (PauseCanvas.gameObject.activeSelf == false
                && SelectStageCanvas.gameObject.activeSelf == false)
            {
                TimePause();
                PauseCanvas.gameObject.SetActive(true);
            }
            else
            {
                TimePlay();
                PauseCanvas.gameObject.SetActive(false);
                SelectStageCanvas.gameObject.SetActive(false);
            }
        }
    }

    public void InitLevel(int level)
    {
        MyLevel = level;
        myCrystal = CrystalDatas[level - 1];
        crystalImg.sprite = Crystals[level - 1];

        maxHP = curHP = myCrystal.GetHP();
        hpInfo.text = curHP.ToString() + " / " + maxHP.ToString();

        stageInfo.text = "STAGE \n" + myCrystal.GetStage().ToString();

        timer.text = "TIMER \n" + myCrystal.GetTimer().ToString();
        time = myCrystal.GetTimer();

        slider.value = 1;

        goldInfo.text = gold.ToString() + "G";

        pickImg.sprite = Picks[MyPickLevel - 1];
        if (MyPickLevel < 5)
        {
            Image img = null;
            if (upgradeBtn.TryGetComponent<Image>(out img) == true)
            {
                img.sprite = Picks[MyPickLevel];
            }
            upgradeGold.text = PickDatas[MyPickLevel].Getgold().ToString() + "G";
        }
        else
        {
            upgradeBtn.gameObject.SetActive(false);
            UpgradeCanvas.gameObject.SetActive(false);
        }
    }

    public void UpgradePick()
    {

        if (MyPickLevel < 5 && gold < PickDatas[MyPickLevel].Getgold())
        {
            return;
        }

        if (MyPickLevel > 5)
        {
            return;
        }

        MyPickLevel++;
        myPick = PickDatas[MyPickLevel - 1];
        pickImg.sprite = Picks[MyPickLevel - 1];


        if (MyPickLevel < 5)
        {
            // Next Pick
            Image img = null;
            upgradeBtn.TryGetComponent<Image>(out img);
            img.sprite = Picks[MyPickLevel];

            upgradeGold.text = PickDatas[MyPickLevel].Getgold().ToString() + "G";

        }
        else
        {
            UpgradeCanvas.SetActive(false);
        }


        gold -= myPick.Getgold();
        goldInfo.text = gold.ToString() + "G";

        save.Start(MyLevel, gold, MyPickLevel);

    }

    public void ClearStage()
    {

        gold += myCrystal.GetGoldEarned();
        MyLevel++;

        if (MyLevel > 5)
        {
            Gameover();
        }
        else
        {
            InitLevel(MyLevel);

            //Save
            save.Start(MyLevel, gold, MyPickLevel);

            for (int i = 0; i < MyLevel; i++)
            {
                Image img = null;
                if (levels[i].TryGetComponent<Image>(out img) == true)
                {
                    img.sprite = InteractImg;
                    levels[i].interactable = true;
                }
                else
                {
                    Debug.LogError("Not Found Image");
                }
            }
        }





    }

    public void OnClickDamage()
    {
        StartCoroutine(Shake());
        SoundManager.Inst.PlaySFX("Click");
        GameObject obj = Pool.Instantiate(effect, MousePos(), Quaternion.identity);
        curHP -= myPick.GettouchDamage();
        slider.value = curHP / maxHP;
        hpInfo.text = curHP.ToString() + " / " + maxHP.ToString();
        if (curHP <= 0)
        {
            ClearStage();
        }
    }

    IEnumerator Shake()
    {
        float time = 0f;
        
        while(time < 0.2f)
        {
            time += Time.deltaTime;
            float rnd = Random.Range(-4.0f, 4.0f);
            Debug.Log(rnd);
            crystalTrans.position = new Vector3(rnd,startPos.y,startPos.z);

            yield return null;
        }

        crystalTrans.position = startPos;
    }
    private Vector3 MousePos()
    {
        Vector3 pos = Input.mousePosition;

        pos = cam.ScreenToWorldPoint(pos);
        pos = new Vector3(pos.x, pos.y, 2f);
        return pos;
    }

    public void Gameover()
    {
        TimePause();
        EndingCanvas.gameObject.SetActive(true);
        SoundManager.Inst.PlayBGM("Ending");
    }
}
