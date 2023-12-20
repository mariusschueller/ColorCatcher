using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public void levelSelect()
    {
        SceneManager.LoadScene("level select");
    }

    public void menu()
    {
        SceneManager.LoadScene("title");
        Time.timeScale = 1;
    }

    public void level1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void level2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void level3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void level4()
    {
        SceneManager.LoadScene("Level4");
    }
    public void level5()
    {
        SceneManager.LoadScene("Level5");
    }
    public void tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void credits()
    {
        SceneManager.LoadScene("credits");
    }
}
