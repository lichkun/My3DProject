using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private static MenuController instance = null;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject music;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(music);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeInHierarchy);
        }
    }

    public void PlayGame()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            menu.SetActive(!menu.activeInHierarchy);
            SceneManager.LoadScene(1);
        }
    }
    public void ExitGame()
    {
        Application.Quit(); 
    }
}
