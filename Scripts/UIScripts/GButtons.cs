using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GButtons : MonoBehaviour
{
    public GameObject LoadingPanelWTD;
    public GameObject GameCompletePanel;
    public SnakeController SnakeControllerScipt;
    public Player_Gravity Player_GravityScript;

    [SerializeField] public bool GameIsPaused;

    private void Start()
    {
        GameIsPaused = false;
    }
    public void PauseB()
    {
        Time.timeScale = 0;
        GameIsPaused = true;

    }

    public void ResumeB()
    {
        
        Time.timeScale = 1;
        //GameCompletePanel.SetActive(false);
        GameIsPaused = false;

    }

    public void NextB()
    {

        Time.timeScale = 1;
        PlayerPrefs.SetInt("OpenLevelNo", PlayerPrefs.GetInt("OpenLevelNo") + 1);
        SceneManager.LoadScene("GameScene");

        GameCompletePanel.SetActive(false);

    }

    public void RestartB()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }


    public void HomeB()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadFunction());
        

    }

    public void JumpB()
    {
        Player_GravityScript.Jump();


    }

    IEnumerator LoadFunction()
    {
        
        LoadingPanelWTD.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("1HomeScene");
        
    }


}
