using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneBtnControl : MonoBehaviour
{

    public void OnClickStartBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickExitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

}
