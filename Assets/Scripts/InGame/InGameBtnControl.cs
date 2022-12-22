using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBtnControl : MonoBehaviour
{

    public void OnClickCrystalBtn()
    {
        GameManager.Inst.OnClickDamage();
    }

    public void OnClickUpgradeBtn()
    {
        GameManager.Inst.UpgradePick();
        SoundManager.Inst.PlaySFX("Button");
    }

    public void OnClickRetryBtn()
    {
        GameManager.Inst.GetPauseCanvas.gameObject.SetActive(false);
        GameManager.Inst.InitLevel(GameManager.Inst.MyLevel);
        GameManager.Inst.TimePlay();
        SoundManager.Inst.PlaySFX("Button");
    }

    public void OnClickSelectStageBtn()
    {
        GameManager.Inst.GetPauseCanvas.gameObject.SetActive(false);
        GameManager.Inst.GetSelectStageCanvas.gameObject.SetActive(true);
        SoundManager.Inst.PlaySFX("Button");
    }

    public void OnClickExitBtn()
    {
#if     UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        SoundManager.Inst.PlaySFX("Button");
#endif
        Application.Quit();
    }

    public void OnClickStageBtn(int level)
    {
        GameManager.Inst.InitLevel(level);
        GameManager.Inst.GetSelectStageCanvas.gameObject.SetActive(false);
        GameManager.Inst.TimePlay();
        SoundManager.Inst.PlaySFX("Button");
    }
}
