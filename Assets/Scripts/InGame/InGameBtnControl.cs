using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBtnControl : MonoBehaviour
{

    public void OnClickCrystalBtn()
    {
        GameManager.Inst.CrystalHp -= 3;
    }

    public void OnClickUpgradeBtn()
    {

    }

    public void OnClickResumeBtn()
    {
        
        GameManager.Inst.GetPauseCanvas.gameObject.SetActive(false);
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

    }
}
