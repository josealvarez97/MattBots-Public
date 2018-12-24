# MattBots-Public

[1. Overview and relevant links](https://github.com/josealvarez97/MattBots-Public#1-overview-and-relevant-links)  
[2. Summary](https://github.com/josealvarez97/MattBots-Public#2-summary)  
[3. Technical details of the app (and sample scripts)](https://github.com/josealvarez97/MattBots-Public#3-technical-details-of-the-app)  
[4. More precise details on my contribution as the chief programmer of the project.](https://github.com/josealvarez97/MattBots-Public#4-more-precise-details-on-my-contribution-as-the-chief-programmer-of-the-project)  

![*Matt embarks in this epic robotic adventure full of mathematical challenges and intense learning!*](https://ksr-ugc.imgix.net/assets/022/413/395/fb5426b1f170cd461a184c4cbdb79ff9_original.JPG?ixlib=rb-1.1.0&w=680&fit=max&v=1535788360&auto=format&gif-q=50&q=92&s=29231ceb8112778884d6a309c0250a39)

*Matt embarks in this epic robotic adventure full of mathematical challenges and intense learning!*

# 1. Overview and relevant links.

MattBots is one of the ventures that best represents my skills, not only technically. However, it is a personal venture for which I'm not willing to share the full source code. This repository highlights the main features, explains the design of the app, among other relevant things that allow to understand (and prove) the result of my work.

P.S. The fact that I don't plan to post the full source code here doesn't mean that I won't be **happy to walk you around any specific technical aspect of the app [*if you reach out*](mailto:j.alvarez@minerva.kgi.edu)** - even, then, the full source code!

**My role:** I'm the chief programmer of **TecúnTecs**, a group of young creators - that I started - currently developing [MattBots](http://www.tecuntecs.com), a Game-Based Learning app that will aim to stimulate Early Childhood Mathematical Thinking. 

**The team:** To learn more about the team, visit [http://www.tecuntecs.com/#team](http://www.tecuntecs.com/#team)

The app is currently in a state of development, but a **demo can be tried out here**: 
* [Windows ZIP DEMO VERSION (90 mb)](https://storage.googleapis.com/www.tecuntecs.com/mattbots-builds/MattBots-ByTecunTecs.zip)
* [Android APK DEMO VERSION (85 mb)](https://storage.googleapis.com/www.tecuntecs.com/mattbots-builds/MattBots-ByTecunTecs.apk)
*  iPhone beta versions will be available after we raise enough funds.

To get notice of the latest updates, please take a look at any of our social media channels by searching for **@mattbotsgame** ([Instagram](https://www.instagram.com/mattbotsgame/), [Facebook page](https://www.facebook.com/mattbotsgame/))

# 2. Summary

![](https://ksr-ugc.imgix.net/assets/022/413/477/341f0a31a5c8ac4023c9707fd565a310_original.png?ixlib=rb-1.1.0&w=680&fit=max&v=1535789440&auto=format&gif-q=50&lossless=true&s=d971f6bd0e0765afc05d4d1b9f1bd90f)

**MattBots is** an application that *improves* the early childhood mathematical thinking of children through game-based learning, and *reveals* insights on children's abilities by tracking the player's actions within the game and performing learning analytics to all the data generated during game time - **giving a chance to offer valuable feedback and advice to parents.**

![](https://ksr-ugc.imgix.net/assets/022/413/487/313ac70281ad24b2d68a8feecb7cae6d_original.png?ixlib=rb-1.1.0&w=680&fit=max&v=1535789627&auto=format&gif-q=50&lossless=true&s=f996e2b49adc2fc16ecb88e3a2e22b20)

As Early Childhood Mathematical Thinking is a broad concept - you can find what MattBots focuses on in the [MattBots Competency Model](http://www.tecuntecs.com/mattbots-competency-model.html), which refers to the collection of knowledge, skills, and attributes to be stimulated and assessed of the player.

### More on how the app looks so far?

**Colorful comis will tell Matt's adventures**

Such as that time when Matt played a dance battle against that jealous robot and defeated it.

![](https://ksr-ugc.imgix.net/assets/022/413/667/44d70e3e873811e179e25464ba39b7f0_original.png?ixlib=rb-1.1.0&w=680&fit=max&v=1535792397&auto=format&gif-q=50&lossless=true&s=3e26520c6771b82ecc9001c5e82b3b65)

Or that time he got lost in a Mars-like dessert and saw himself in need of building a bridge to cross over a cliff - all because the other jealous robot threw his ship there.

![](https://ksr-ugc.imgix.net/assets/022/413/750/644a96e7ad8be6290e3ceff9a044d093_original.png?ixlib=rb-1.1.0&w=680&fit=max&v=1535793708&auto=format&gif-q=50&lossless=true&s=559e254d8ac5f9e5f0e28afd0a0619b1)

Immersing children in engaging and exciting experiences is a core part of MattBots. That's why we put time and effort in crafting every stage of the game.

### But you were looking for something like a video trailer?

[Check out this!](http://www.tecuntecs.com)

# 3. Technical details of the app.

MattBots is powered by Unity3D, a popular game engine that employs the c# programming language for its API. The current prototype of the app leverages Unity's capabilities to define scriptable objects, follows well an OPP and event-driven paradigm, and suppports keeping track of the users' progress locally. In the future we plan to continue making further use of UnityAnalytics API to keep track of the player's interactions withing the game and use the data to inform relevant feedback.

#### Diving more into details

* **We make broad use of [Unity's scriptable objects]()** in the application. Here's a funny example: 

As every single stage of the game starts with a comic sequence that introduces the player into the scenario (check out the demo!!!), I came up with a structure that allows to set each sequence in a matter of seconds. A 'Comic' file type, defined by the Comic class (specified as an scriptable object), in [Comic.cs](https://github.com/josealvarez97/MattBots-Public/blob/master/SampleScripts/Comic.cs) contains fields for things like the sequence of images, an array of times for each image, an audio effects sequnce, general comic background music, and some other details; that later on are used by a comics manager attached to any game object in a scene that calls for a comic. [ComicsManager.cs](https://github.com/josealvarez97/MattBots-Public/blob/master/SampleScripts/ComicsManager.cs) makes some good use of such information, and plays the comic while loading the scene asynchronously, calculating and executing transitions smoothly. It is intended to be flexible and easy to use when needed, and hence defines methods such as 'PlayComic(Comic comic, Image comicsScreen)', 'PlayComicAndFreeze(Comic comic, Image comicsScreen)', and many others (in [ComicsManager.cs](https://github.com/josealvarez97/MattBots-Public/blob/master/SampleScripts/ComicsManager.cs)).

* **We have taken care to persist data of the user's locally (e.g., AchievementsData)**. For example, here are some excerpts of methods that save/load some data, and perform the necessary validations and proper handling of common exceptions.

*SaveData is a generic method that serializes some objects*

```c#
    public static void SaveData<TData>(TData objectToSave) where TData : Data, new()
    {
        if (!dataHasBeenLoaded)
        {
            Debug.LogError("Data had not been loaded yet.");
            LoadSerialazableData();
            Debug.Log("Loaded serialazable data");
        }

        string dataPath = null;
        if (typeof(TData) == typeof(AchievementsData))
            dataPath = GAME_ACHIEVEMENTS_PATH;


        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + dataPath);


        bf.Serialize(file, objectToSave);
        file.Close();
    }

```

*And calls these other guys*

```c#
    public static void LoadSerialazableData()
    {
        if (!dataHasBeenLoaded)
        {
            LoadRecordsData();
            LoadStarsRatingData();
            LoadData<AchievementsData>(ref achievementsData);
            dataHasBeenLoaded = true;
            Debug.Log("All Serializable Data Has Been Load");

        }
        else
        {
            Debug.LogError("WARNING: Attempt of loading serializable data when it had already been loaded.");
        }



    }
```

*Here we finally have some handling of common exceptions (excuse the raw comments, I pasted it as it is)*

```c#
    public static void LoadRecordsData()
    {
        Debug.LogAssertion("Loading Records Data");


        // IF FILE EXISTS - LOAD IT!!!
        if (File.Exists(Application.persistentDataPath + GAME_RECORDS_PATH))
        {
            Debug.LogAssertion("File exists.");


            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + GAME_RECORDS_PATH, FileMode.Open);

            RecordsData recordsData = null;
            // Try deserializing it without problems.
            try
            {
                recordsData = (RecordsData)bf.Deserialize(file);
                file.Close();

                Debug.Log("File serialization was possible. Records Data.");
            }
            // There was a problem deserializing, it's best to delete and start all over again.
            catch (System.Runtime.Serialization.SerializationException e)
            {
                Debug.LogError("Serialization Exception. Data file will be deleted and recreated");
                Debug.LogException(e);

                file.Close();
                File.Delete(Application.persistentDataPath + GAME_RECORDS_PATH);
                recordsData = null;
                Debug.LogAssertion("Exception Handled Successfully");
            }
            catch (Exception e)
            {
                Debug.LogError("Exception while Loading Records Data");
                Debug.LogException(e);
                throw e;
            }


            // WE GOTTA INITIALIZE ApplicationModel.levelsBestScores
            //ApplicationModel.levelsBestScores = new Dictionary<int, int>(); Already done...

            for (int i = 0; i < gameLevelsList.Count; i++)
            {

                int levelID = gameLevelsList[i].levelID;
                Debug.Log("Initializing best score for level (ID) " + levelID);

                // If Serialization was successful and there exists a value for the key
                if (recordsData != null && recordsData.levelsBestScores != null)
                    if (recordsData.levelsBestScores.ContainsKey(levelID))
                    {
                        Debug.Log("LEVEL BEST SCORE: " + recordsData.levelsBestScores[levelID].ToString());
                        Debug.Log("Assigning best score (" + recordsData.levelsBestScores[levelID] + "for level (ID) " + levelID);

                        // Assign such value to the static reference.
                        ApplicationModel.levelsBestScores.Add(levelID, recordsData.levelsBestScores[levelID]);
                        continue;
                    }


                // Otherwise
                // It'll be initialized at zero 
                ApplicationModel.levelsBestScores.Add(levelID, 0);
            }



            //dataHasBeenLoaded = true;
            //initialRecordsData = recordsData;
        }
        // If no file was found...
        else
        {
            Debug.LogAssertion("File does not exist.");

            for (int i = 0; i < ApplicationModel.gameLevelsList.Count; i++)
            {
                int levelID = gameLevelsList[i].levelID;
                ApplicationModel.levelsBestScores.Add(levelID, 0);
            }
        }


        Debug.LogAssertion("Finish with Records Data");
    }
```

* **We adhere to an arquitecture that aims to follow proper standards of OPP and event-driven paradigms**. Excerpts part of [EstimationLevelController.cs](https://github.com/josealvarez97/MattBots-Public/blob/master/SampleScripts/EstimationLevelController.cs)

```c#
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
```

* **Here we have started to make use of UnityAnalytics API**. So far, normal analytics; but in the future, we plan to do more sophisticated analyses of our own to this data focused on finding patterns to inform *educational* feedback (not only common usage patterns and statistics). Excerpt part of [EstimationLevelController.cs](https://github.com/josealvarez97/MattBots-Public/blob/master/SampleScripts/EstimationLevelController.cs)

```c#
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
```



# 4. More precise details on my contribution as the chief programmer of the project.

So far, I have done most of the programming, and **every single sample script or excerpt I have showcased here is part of what I have done by myself**. [@oswilehi](https://github.com/oswilehi), *a good old friend and great teammate*, has started to explore integrations with social media API's (we envision some features that may require parents/teachers to be able to log in), and options for database software & services as we are seeking to persist and handle some data in the cloud.

[Luis](https://www.linkedin.com/in/luis-gerardo-hern%C3%A1ndez-de-le%C3%B3n-b16559103/) has been our great graphic designer. Remember to check our social media channels to see more of the result of his work (e.g., [Instagram page](https://www.instagram.com/mattbotsgame/)), if you are interested.

Read more about us at [http://www.tecuntecs.com/#team](http://www.tecuntecs.com/#team).

Also, you can always reach out to any of us if you have more questions about our team dyanamics at [jose@tecuntecs.com](mailto:jose@tecuntecs.com), [oscar@tecuntecs.com](mailto:oscar@tecuntecs.com), [luis@tecuntecs.com](mailto:luis@tecuntecs.com).


***We are not done yet, but it's been a wonderful adventure so far alongside this great team.***

![Jose, Luis, and Oscar](https://ksr-ugc.imgix.net/assets/022/452/482/6aeb837503e5cfdf178d48264d503457_original.png?ixlib=rb-1.1.0&w=680&fit=max&v=1536133904&auto=format&gif-q=50&lossless=true&s=d855a65bec3d0dfb1b0e35a93dac725c)

*Myself, Luis, and Oscar*



