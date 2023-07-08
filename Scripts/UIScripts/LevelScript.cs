using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameAnalyticsSDK;

public class LevelScript : MonoBehaviour
{
    public int LevelNo;
    public Text SelfText;
    public GameObject LoadingPanelWTD;
    public Button ButtonComponent;

    private void Awake()
    {
        
    }

    private void Start()
    {
        SelfText.text = LevelNo.ToString();

        if(LevelNo > PlayerPrefs.GetInt("LevelsUnloked"))
        {
            ButtonComponent.interactable = false;
        }
    }

    private void Update()
    {
    }

    public void OnClickFunction()
    {
        PlayerPrefs.SetInt("OpenLevelNo", LevelNo);
        StartCoroutine(LoadFunction());

        //GAEvent to know How much progression Players are making in the game
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "LevelStarted", LevelNo);

    }

    IEnumerator LoadFunction()
    {
        LoadingPanelWTD.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameScene");
    }
}
