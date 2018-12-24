using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EstimationLevelController : MonoBehaviour
{

    // No idea why I'm using a singleton here...
    public static EstimationLevelController instance;

    //PUBLIC
    #region Public Variables
    // Physical Game Objects
    [Header("Physical Game Objects")]
    public Boss bossReference;
    public GameObject playerReference;
    // UI
    [Header("UI")]
    public Button[] ruleButtons;
    public ProgressWheel progressWheel;
    public Image levelPassedPanel;
    public Image levelLostPanel;
    public Image pausedMenuPanel;
    [Header("Trackers / Controllers")]
    public EstimationLevelPointsTracker pointsTracker;

    [Header("References to other game objects")]
    public GameObject starsRatingComponent;

    #endregion
    //PRIVATE



    // EVENT HANDLERS
    public System.EventHandler levelPassedEventHandler;

    #region Awake, Start, Update
    void Awake()
    {
        // Let's secure data first...
        if (!ApplicationModel.DataHasBeenLoaded)
            SceneManager.LoadScene(0, LoadSceneMode.Single);


        // Again, no idea why I'm using a singleton... convenient?
        if (instance == null)
        {
            instance = this;


        }
        else
        {
            Destroy(this);
        }
    }


    void Start()
    {
        //ApplicationModel.PlayingMode = PlayingMode.Battle;

        if (ApplicationModel.PlayingMode == PlayingMode.Story)
        {
            // Subscribe functions to events...
            progressWheel.ProgressWheelDisplayEnds += ProgressWheel_OnProgressWheelDisplayEnds;
            bossReference.shapeDefeatedEventHandler += Boss_OnShapeDefeated;
            bossReference.bossDefeatedEventHandler += Boss_OnBossDefeated;
            progressWheel.ListenToBoss(bossReference);
            playerReference.GetComponent<PlayerHealth>().PlayerDies += PlayerHealthBar_OnPlayerDies;

            // Let's show the progress wheel for a bit during the begining
            progressWheel.StartProgressWheelCycle();

            // Hide and Display things as necessary
            pointsTracker.HideScoresUI();
        }
        else if (ApplicationModel.PlayingMode == PlayingMode.Battle)
        {
            // Subscribe functions to events...
            bossReference.shapeDefeatedEventHandler += Boss_OnShapeDefeated;
            playerReference.GetComponent<PlayerHealth>().PlayerDies += PlayerHealthBar_OnPlayerDies;


            // Set up points tracker
            //ApplicationModel.estimationLevelBestScore = 47;
            //pointsTracker.SetBestScore(ApplicationModel.estimationLevelBestScore);

            // Hide and Display things as necessary
            pointsTracker.DisplayScoresUI();
        }





    }

    void Update()
    {
        if (InterfaceCommandsController.currentGlobalInterfaceCommand == InterfaceCommand.ExitLevel)
        {
            if (ApplicationModel.PlayingMode == PlayingMode.Battle)
                pointsTracker.SaveBestScoreToFile();

            SceneManager.LoadScene(1);
            InterfaceCommandsController.currentGlobalInterfaceCommand = InterfaceCommand.NoCommand;
        }
        else if (InterfaceCommandsController.currentGlobalInterfaceCommand == InterfaceCommand.Restart)
        {
            if (ApplicationModel.PlayingMode == PlayingMode.Battle)
                pointsTracker.SaveBestScoreToFile();

            SceneManager.LoadScene("Estimation");
            InterfaceCommandsController.currentGlobalInterfaceCommand = InterfaceCommand.NoCommand;
        }
        else if (InterfaceCommandsController.currentGlobalInterfaceCommand == InterfaceCommand.PauseLevel)
        {
            InterfaceCommandsController.currentGlobalInterfaceCommand = InterfaceCommand.NoCommand;
            pausedMenuPanel.gameObject.SetActive(true);

            Time.timeScale = 0;
        }
        else if (InterfaceCommandsController.currentGlobalInterfaceCommand == InterfaceCommand.PlayBackLevel)
        {
            InterfaceCommandsController.currentGlobalInterfaceCommand = InterfaceCommand.NoCommand;
            pausedMenuPanel.gameObject.SetActive(false);

            Time.timeScale = 1;
        }


    }
    #endregion



    #region EVENTS SUBSCRIBERS
    void ProgressWheel_OnProgressWheelDisplayEnds(object sender, ProgressWheelEventArgs e)
    {
        //progressWheel.gameObject.SetActive(false);

        // Do shit here if you need to...
    }

    public void OnRuleClick(Button button)
    {
        Debug.Log("Rule buttons " + ruleButtons.ToString());
        Debug.Log("Current number shape " + bossReference.CurrentNumberShape);
        Debug.Log("Right button " + ruleButtons[bossReference.CurrentNumberShape]);
        Debug.Log("Selected button " + button);

        if (ruleButtons[bossReference.CurrentNumberShape] == button)
        {
            playerReference.GetComponent<BulletFireScript>().Fire();
        }
        else
        {
            //button.image.color = new Color(217,28,92,255);
            button.image.CrossFadeColor(Color.red, 0.25f, true, true);


        }
    }

    public void Boss_OnShapeDefeated(object sender, ShapeDefeatedEventArgs e)
    {
        playerReference.GetComponent<BulletFireScript>().ResetPlayerCharges(bossReference.CurrentNumberShape); // this could be in the player with a function subscribed to the event...

        if (ApplicationModel.PlayingMode == PlayingMode.Story)
            progressWheel.StartProgressWheelCycle();
        else if (ApplicationModel.PlayingMode == PlayingMode.Battle)
        {
            pointsTracker.AddUpScore();
        }



    }

    public void Boss_OnBossDefeated(object sender, BossDefeatedEventArgs e)
    {
        bossReference.gameObject.SetActive(false);
        progressWheel.StartProgressWheelDestruction();
        levelPassedPanel.gameObject.SetActive(true);

        OnLevelPassed();
    }


    public void PlayerHealthBar_OnPlayerDies(object sender, System.EventArgs e)
    {
        if (ApplicationModel.PlayingMode == PlayingMode.Battle)
            pointsTracker.SaveBestScoreToFile();


        levelLostPanel.gameObject.SetActive(true);
    }
    #endregion




    #region EventRaisers

    protected virtual void OnLevelPassed()
    {
        if (levelPassedEventHandler != null)
            levelPassedEventHandler(this, null);


        // Adjust stars rating
        StarsRating initialStarsRating = ApplicationModel.levelsStarsRatings[4];

        if (playerReference.GetComponent<PlayerHealth>().currentHealth > 150f)
            ApplicationModel.levelsStarsRatings[4] = StarsRating.ThreeStars;
        else if (playerReference.GetComponent<PlayerHealth>().currentHealth > 100f)
            ApplicationModel.levelsStarsRatings[4] = StarsRating.TwoStars;
        else if (playerReference.GetComponent<PlayerHealth>().currentHealth > 50)
            ApplicationModel.levelsStarsRatings[4] = StarsRating.OneStar;
        else //if (failCount < 5)
            ApplicationModel.levelsStarsRatings[4] = StarsRating.ZeroStars;

        starsRatingComponent.GetComponent<StarsRatingComponent>().SetAppropriateStarRating();

        // -- Analytics -- 
        AnalyticsUtilities.ReportStandardEvent_LevelComplete(SceneManager.GetActiveScene(), 4, ApplicationModel.PlayingMode, (int)ApplicationModel.levelsStarsRatings[4], playerReference.GetComponent<PlayerHealth>().currentHealth / 200, Time.time);

        if (ApplicationModel.levelsStarsRatings[4] > initialStarsRating)
            ApplicationModel.SaveStarsRatingData();
        else
            ApplicationModel.levelsStarsRatings[4] = initialStarsRating;


        //starsRatingComponent.gameObject.SetActive(true);

        ApplicationModel.achievementsData.achievements[AchievementsData.MATT_WILL_RETURN_UNLOCKED].achieved = true;
        ApplicationModel.SaveData<AchievementsData>(ApplicationModel.achievementsData);


        bossReference.GetComponent<BossFire>().CancelInvoke("Fire");
    }
    #endregion
}
