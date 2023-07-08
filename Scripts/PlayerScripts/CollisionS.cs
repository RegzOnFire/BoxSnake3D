using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameAnalyticsSDK;

public class CollisionS : MonoBehaviour
{

    //Settings
    public float GroundScaleValue = 2, SteerSpeedIncreaseValue = 180, MoveSpeedIncreaseValue = 8, SmallSizeValue = 0.5f;
    public GameObject[] UnknownBoxItems;
    public int ShieldValue;
    [SerializeField] public int CurrentScore; 




    //Refrences
    [Space]
    public SnakeController SnakeControllerScript;
    public Player_Gravity player_GravityScript;
    public GameObject Ground,GameOverPanel,FruitPrefab;
    public TextMeshProUGUI CurrentScoreText, HighScoreText;
    public GButtons GButtonsScript;
    public AudioSource EatSound;
    public LevelManagerS LevelManagerScript;
    public Animator PlayerCamAnimator;

    //Bools
    [SerializeField]
    public bool DeadBool;
 

    private void Start()
    {
        // CurrentScore = 0 (in Starting)
        CurrentScore = 0;
    }

    private void Update()
    {
        //Updating Score Text
        CurrentScoreText.text = "Score " + CurrentScore.ToString();
        HighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        // Using Switch To Compaire Tags
        switch(other.gameObject.tag)
        {
            // Increase Body And Score By eating Fruit
            case "Fruit":
                SnakeControllerScript.UseNewSpawnPosition = true;
                SnakeControllerScript.GrowSnake();
                SpawnFruitAtRandomPlace();

                //Updating LevelData if Fruit Is A last fruit
                LevelManagerScript.LevelProgressManager();
                Destroy(other.gameObject);

            // Using ScoreManager to update CurrentScore And HighScore
                ScoreManager();
                break;

            // Time Stop(Dead) when collision with body
            case "Body":
                GameOver();
                break;

            case "Border":
                GameOver();
                break;

            case "Obstecle":
                GameOver();
                break;

            // Scaling Ground Area You can Adjust Scale Value on top settings Called GroundScaleValue
            case "IncreaseArea":
                Ground.transform.localScale += new Vector3(GroundScaleValue, 0, GroundScaleValue);
                Destroy(other.gameObject);
                break;

            // CanJump = true In Player_GravityScript For Jump Function Activation
            case "Jump":
                player_GravityScript.CanJump = true;
                Destroy(other.gameObject);
                break;

            // Increasing Steer Speed By changing SnakeControllerScript's SteerSpeed
            case "SteerSpeedIncreaser":
                SnakeControllerScript.SteerSpeed = SteerSpeedIncreaseValue;
                Destroy(other.gameObject);
                break;

            // Increasing MoveSpeed
            case "MoveSpeedIncreaser":
                SnakeControllerScript.MoveSpeed = MoveSpeedIncreaseValue;
                Destroy(other.gameObject);
                break;

            // UnknownBox Can Instantiate Random Items
            case "UnknownBox":
                Instantiate(UnknownBoxItems[Random.Range(0,UnknownBoxItems.Length)], other.transform.position,Quaternion.identity);
                Destroy(other.gameObject);
                break;

            // Affecting SnakeController Script
            case "OppositeSideController":
                SnakeControllerScript.SteerChanger = -1;
                Destroy(other.gameObject);
                break;

            // Colliding With Shield item will Increase ShieldValue (ShieldValue = Health)
            //currently not working
            case "Shield":
                ShieldValue += 1;
                Debug.Log(ShieldValue);
                Destroy(other.gameObject);
                break;

            //currently not working
            case "SmallSize":
                transform.localScale = new Vector3(SmallSizeValue, SmallSizeValue, SmallSizeValue);
                Destroy(other.gameObject);
                break;

            case "Magnet":
                transform.localScale = new Vector3(SmallSizeValue, SmallSizeValue, SmallSizeValue);
                Destroy(other.gameObject);
                break;
        }
            
    }

    public void ScoreManager()
    {
        // Increasing Score
        CurrentScore += 1;

        // Comparing HighScore with CurrentScore
        if (CurrentScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", CurrentScore);
        }

        //sound
        EatSound.Play();
        //Vibrate
        Vibration.Vibrate(15);

    }

    public void GameOver()
    {
        if(!GButtonsScript.GameIsPaused)
        {
            Debug.Log("Dead Bruuu!");
            SnakeControllerScript.SteerSpeed = 0;
            SnakeControllerScript.MoveSpeed = 0;
            player_GravityScript.CanJump = false;
            //panel
            GameOverPanel.SetActive(true);

            //CamAnimation
            PlayerCamAnimator.SetBool("GameOver", true);

            //Vibrate
            Vibration.Vibrate(100);

            //When player will die This Line of code will be executed and send HighScore Of player to the server
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "HighScore ", PlayerPrefs.GetInt("HighScore"));

        }
    }


    public void SpawnFruitAtRandomPlace()
    {
        Instantiate(FruitPrefab, new Vector3(Random.Range(-13.5f, 13.5f), 0, Random.Range(-13.5f, 13.5f)), Quaternion.identity); //Used Values Are Also Used In FruitScript

    }
        

}
