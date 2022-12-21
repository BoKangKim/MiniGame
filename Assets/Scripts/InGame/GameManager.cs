using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Images")]
    [SerializeField] private Sprite[] Cystals = null;
    [SerializeField] private Sprite[] Picks = null;
    [SerializeField] private Sprite InteractImg = null;
    [SerializeField] private Sprite NotInteractImg = null;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer crystal = null;

    [Header("Button")]
    [SerializeField] private Button[] levels = null;

    [Header("Canvases")]
    [SerializeField] private Canvas PauseCanvas = null;
    [SerializeField] private Canvas SelectStageCanvas = null;

    [Header("Scriptable")]
    [SerializeField] private CrystalScriptable[] CrystalDatas = null;
    [SerializeField] private PickScriptable[] PickDatas = null;

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
    public int CrystalHp;
    public int MyLevel { get; private set; } = 0;
    public int MyPickLevel { get; private set; } = 0;
    [HideInInspector] public CrystalScriptable myCrystal = null;
    [HideInInspector] public PickScriptable myPick = null;

    private void Awake()
    {
        //세이브 데이터 들어갈 자리

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
        myCrystal = CrystalDatas[MyLevel];
        myPick = PickDatas[MyPickLevel];

        SetCrystalHp();
    }

    private void Update()
    {
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
        Debug.Log(CrystalHp);
    }

    void SetCrystalHp()
    {
        //테스트중
        CrystalHp = CrystalDatas[0].GetHp();
    }
}
