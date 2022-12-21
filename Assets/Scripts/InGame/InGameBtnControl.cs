using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBtnControl : MonoBehaviour
{

    public void OnClickCrystalBtn()
    {

    }

    public void OnClickUpgradeBtn()
    {
        GameManager.Inst.UpgradePick();
    }

    public void OnClickRetryBtn()
    {
        GameManager.Inst.GetPauseCanvas.gameObject.SetActive(false);
        GameManager.Inst.InitLevel(GameManager.Inst.MyLevel);
        GameManager.Inst.TimePlay();
    }

    public void OnClickSelectStageBtn()
    {
        GameManager.Inst.GetPauseCanvas.gameObject.SetActive(false);
        GameManager.Inst.GetSelectStageCanvas.gameObject.SetActive(true);
    }

    public void OnClickExitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void OnClickStageBtn(int level)
    {
        GameManager.Inst.InitLevel(level);
        GameManager.Inst.GetSelectStageCanvas.gameObject.SetActive(false);
    }
}
