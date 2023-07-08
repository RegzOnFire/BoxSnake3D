using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HButtonS : MonoBehaviour
{
    public float DTWPanelCountdown = 1.5f;

    public GameObject StorePanel,MainPanel, DTWPanel,NoticePanel;
    public Animator CamAnimator;

    private void Awake()
    {
        // For Notice Panel
        if(PlayerPrefs.GetInt("FirstTimeOpenBool") == 1)
        {
            NoticePanel.SetActive(false);
        }
        else
        {
            NoticePanel.SetActive(true);
        }
    }

    private void Update()
    {
        CountdownFunction();
    }

    public void StoreB()
    {

        //Animation Of Cam
        CamAnimator.SetBool("OnStore", true);

        StorePanel.SetActive(true);
        MainPanel.SetActive(false);
    }

    public void BackBOfStore()
    {

        //Animation Of Cam
        CamAnimator.SetBool("OnStore", false);

        MainPanel.SetActive(true);
        StorePanel.SetActive(false);
    }

    // CountDown Function For Destroy DTWPanel
    public void CountdownFunction()
    {
        DTWPanelCountdown -= 1 * Time.deltaTime;
        if (DTWPanelCountdown < 0)
        {
            Destroy(DTWPanel);
            DTWPanelCountdown = 0;
        }
    }

    public void OkB()
    {
        PlayerPrefs.SetInt("FirstTimeOpenBool", 1);
    }
}
