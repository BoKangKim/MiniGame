using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private Sprite[] Picks = null;
    [SerializeField] private Sprite InteractImg = null;
    [SerializeField] private Sprite NotInteractImg = null;

    [Header("Canvases")]
    [SerializeField] private Canvas PauseCanvas = null;
    [SerializeField] private Canvas SelectStageCanvas = null;

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
    }
}
