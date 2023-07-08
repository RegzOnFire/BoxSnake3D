using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GAButtons : MonoBehaviour
{
    public void StartBPressed()
    {
        GameAnalytics.NewDesignEvent("StartButtonPressed");
    }
    
    public void StoreBPressed()
    {
        GameAnalytics.NewDesignEvent("StoreButtonPressed");
    }
    
    public void AboutBPressed()
    {
        GameAnalytics.NewDesignEvent("AboutButtonPressed");
    }
    
    public void ScoreLevelBPressed()
    {
        GameAnalytics.NewDesignEvent("ScoreLevelButtonPressed");
    }
    
    public void InsaneLevelBPressed()
    {
        GameAnalytics.NewDesignEvent("InsaneLevelButtonPressed");
    }
    
    
    public void CharSelectBPressed()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "CharacterSelected", PlayerPrefs.GetInt("SelectedCharacterNumber"));
    }


}
