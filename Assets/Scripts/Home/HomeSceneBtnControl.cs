using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneBtnControl : MonoBehaviour
{

    public void OnClickStartBtn()
    {
        SoundManager.Inst.PlaySFX("Button");
        SceneManager.LoadScene(1);
    }

    public void OnClickExitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        SoundManager.Inst.PlaySFX("Button");
        Application.Quit();
    }

}
