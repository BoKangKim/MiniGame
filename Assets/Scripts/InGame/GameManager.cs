using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SaveLoad;

public class GameManager : MonoBehaviour
{
    #region SingleTon
    private static GameManager instance = null;

    public static GameManager Inst 
    {
        get 
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if(instance == null)
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

    [Header("Scriptable")]
    [SerializeField] private CrystalScriptable[] CrystalDatas = null;
    [SerializeField] private PickScriptable[] PickDatas = null;

    [Header("TEXT")]
    [SerializeField] private TextMeshProUGUI stageInfo = null;
    [SerializeField] private TextMeshProUGUI goldInfo = null;
    [SerializeField] private TextMeshProUGUI upgradeGold = null;
    [SerializeField] private TextMeshProUGUI timer = null;
    [SerializeField] private TextMeshProUGUI hpInfo = null;
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
    private int maxHP = 0;
    private int curHP = 0;
    private int gold = 0;
    private float time = 0;

    private Save save = null;
    private Load load = null;
    private SaveData data;
    public Save GetSave() { return save; }


    private void Awake()
    {
        //세이브 데이터 들어갈 자리
        save = new Save();
        load = new Load();

        data = load.Start();

        MyLevel = data.level;
        MyPickLevel = data.pick;
        gold = data.gold;

        if(MyLevel == 0)
        {
            MyLevel = 1;
        }

        if(MyPickLevel == 0)
        {
            MyPickLevel = 1;
        }

        crystal.TryGetComponent<Image>(out crystalImg);

        for(int i = 0; i < MyLevel; i++)
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

        gold = 10000000;
        InitLevel(MyLevel);
    }

    private void Update()
    {
        if (time <= 0f)
        {
            TimePause();
            save.Start(MyLevel,gold,MyPickLevel);
            PauseCanvas.gameObject.SetActive(true);
            return;
        }

        time -= Time.deltaTime;

        timer.text = "TIMER \n" + ((int)time).ToString();

        

        if(Input.GetKeyDown(KeyCode.Escape) == true)
        {
            if(PauseCanvas.gameObject.activeSelf == false 
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

        stageInfo.text = "STAGE \n" +  myCrystal.GetStage().ToString();

        timer.text = "TIMER \n" + myCrystal.GetTimer().ToString();
        time = myCrystal.GetTimer();

        goldInfo.text = gold.ToString() + "G";

        if(MyPickLevel < 5)
        {
            upgradeGold.text = PickDatas[MyPickLevel].Getgold().ToString() + "G";
        }
        else
        {
            UpgradeCanvas.gameObject.SetActive(false);
        }
    }

    public void UpgradePick()
    {

        if (gold < myPick.Getgold()
            || MyPickLevel > 5)
        {
            return;
        }

        save.Start(MyLevel, gold, MyPickLevel);

        MyPickLevel++;
        myPick = PickDatas[MyPickLevel - 1];
        pickImg.sprite = Picks[MyPickLevel - 1];
        

        if(MyPickLevel < 5)
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
    }

    public void ClearStage()
    {
        // if Clear Stage
        gold += myCrystal.GetGoldEarned();
        MyLevel++;
        InitLevel(MyLevel);

        //Save
        save.Start(MyLevel,gold,MyPickLevel);
    }
}
