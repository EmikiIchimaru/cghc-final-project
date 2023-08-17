using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /* [SerializeField] private Button buttonBen;
    [SerializeField] private Button buttonEric;
    [SerializeField] private Button buttonKen;
    [SerializeField] private Button buttonNana;
    [SerializeField] private Button buttonRohith; */

    /* 
    public enum Display { Title, Chapters, Credits };

    private Display display;

    [SerializeField] private GameObject title;
    [SerializeField] private GameObject chapters;
    [SerializeField] private GameObject credits; 
    //[SerializeField] private GameObject black; */

    /* void Start()
    {
        display = Display.Title;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetDisplayType(Display.Title);
        } 
    }

    private void SetDisplayType(Display newdisplay)
    {
        if (newdisplay == display) 
        { 
            return; 
        }

        display = newdisplay;

        title.SetActive(false);
        chapters.SetActive(false);
        credits.SetActive(false);
        //black.SetActive(false);

        switch (newdisplay)
        {
            case Display.Title:
            {   
                title.SetActive(true);
                break;
            }
            case Display.Chapters:
            {   
                chapters.SetActive(true);
                //black.SetActive(true);
                break;
            }
            case Display.Credits:
            {   
                credits.SetActive(true);
                //black.SetActive(true);
                break;
            } 
        }
    }
 */
   

    public void LevelBen()
    {
        SceneManager.LoadScene("BenScene");
        Debug.Log("Loading!");
    }

    public void LevelEric()
    {
        SceneManager.LoadScene("EricScene");
        Debug.Log("Loading!");
    }

    public void LevelKen()
    {  
        SceneManager.LoadScene("KenScene");
        Debug.Log("Loading!");
    }

    public void LevelNana()
    {
        SceneManager.LoadScene("FathinaScene");
        Debug.Log("Loading!");
    }

    public void LevelRohith()
    {
        SceneManager.LoadScene("RohithScene");
        Debug.Log("Loading!");
    }

    /* public void PlayButton()
    {
        SetDisplayType(Display.Chapters);
        //click.Play();
    }

    public void CreditsButton()
    {
        //click.Play();
        SetDisplayType(Display.Credits);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
 */

}
