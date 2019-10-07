using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Repeat : MonoBehaviour
{

    public static Repeat instance;

    [SerializeField]
    public Button button;

    [SerializeField]
    public Button exit;

    [SerializeField]
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        exit.onClick.AddListener(Quit);
        text.text = DontDestroyOnLoadClass.SecondsAlive.ToString() + " seconds";
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene(sceneName: "InGame");
    }

    void Quit()
    {
        Application.Quit();
    }
}
