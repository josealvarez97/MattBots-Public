using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ComicsManager : MonoBehaviour
{

    // Singleton
    public static ComicsManager instance;

    // Real World Resources
    //public Image loadingScreen;
    //public Text loadingText;

    private void Awake()
    {
        // Set up singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private static float CalculateTransitionIn(float bpm)
    {
        float quarterNote = 60 / bpm;
        float transitionIn = quarterNote;

        return transitionIn;
    }
    private static float CalculateTransitionOut(float bpm)
    {
        Debug.Log("BPM " + bpm);
        float quarterNote = 60 / bpm;
        Debug.Log("Quarter note " + quarterNote);
        float transitionOut = quarterNote * 32;
        Debug.Log("Transition out " + transitionOut);

        return transitionOut;

    }

    public static IEnumerator PlayComicAndLoadSceneAsync(Comic comic, int sceneBuildIndex, Image comicsScreen, Image loadingScreen, Text loadingText)
    {
        // LOAD SCENE ASYNC
        Debug.LogAssertion("Starting Async Scene Load");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex);
        // Do not allow activation until comic squence has finished
        asyncLoad.allowSceneActivation = false;
        //SceneManager.sceneLoaded += OnSceneLoaded;

        // SET UP COMIC AUDIO SOURCE
        AudioSource comicAudioSource = comicsScreen.GetComponent<AudioSource>();
        Debug.Log("Audio source reference retrieved");


        // TRY FINISH SETTING UP COMIC AUDIOSOURCE
        try
        {
            comicAudioSource.clip = comic.comicBgMusic;
            comicAudioSource.outputAudioMixerGroup = comic.comicBgMusicOutputAudioMixerGroup; // or channel...
            // PLAY COMIC AUDIO SOURCE - Initial snapshot will be just settled start snapshot.
            comicAudioSource.Play();
            comic.loudSnapshot.TransitionTo(CalculateTransitionIn(comic.comicBgMusicBpm));
        }
        catch (UnassignedReferenceException e)
        {
            Debug.Log("Failed setting up comic audio source. Check Comic File.");
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log("Failed setting up comic audio source. Check Comic File.");
        }
        catch (System.Exception e)
        {
            throw e;
        }

        
        
        // ACTIVE COMICS SCREEN
        Debug.LogAssertion("Setting COMICS screen active");
        comicsScreen.gameObject.SetActive(true);

        // SHOW SEQUENCE - through coroutine
        for (int i = 0; i < comic.comicSequence.Length; i++)
        {
            // Put Comic On Screen
            Debug.LogAssertion("Display Comic sprite " + i);
            comicsScreen.sprite = comic.comicSequence[i];


            // Play Corresponding Sound Effect
            Debug.Log("Audio Source " + comicAudioSource.ToString());
            if (i < comic.audioEffectsSequence.Length)
            {
                Debug.Log("Audio Clip" + comic.audioEffectsSequence[i]);
                comicAudioSource.PlayOneShot(comic.audioEffectsSequence[i], 2);
            }
            

            // continue execution later.
            yield return new WaitForSeconds(comic.timeSequence[i]);
            //yield return null;
        }

        // Fade into silent snapshot...
        try
        {
            float transitionOut = CalculateTransitionOut(comic.comicBgMusicBpm);
            float transitionIn = CalculateTransitionIn(comic.comicBgMusicBpm);

            Debug.LogAssertion("Comic bg bpm " + comic.comicBgMusicBpm);
            Debug.LogAssertion("Comic Transition Out " + transitionOut);

            comic.silentSnapshot.TransitionTo(transitionIn * 2);

        }
        catch (UnassignedReferenceException e)
        {
            Debug.Log("Failed setting up transition in and out for silent snapshot. Check comic file ");
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log("Failed setting up transition in and out for silent snapshot. Check comic file ");
        }
        catch (System.Exception e)
        {
            throw e;
        }



        loadingScreen.gameObject.SetActive(true);

        if (comic.loadingScreenComic != null)
        {
            loadingScreen.sprite = comic.loadingScreenComic;
            loadingScreen.color = Color.white;
        }
        //loadingScreen.gameObject.SetActive(true);
        //loadingScreen.sprite = null;

        loadingText.gameObject.SetActive(false);
        for (int i = 0; i < 4; i++)
        {

            // Put loading screen
            //if (loadingText.text != "...")
            //    loadingText.text += ".";
            //else
            //    loadingText.text = "";


            yield return new WaitForSeconds(1f);
        }


        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone)
        {

            // Put loading screen
            if (loadingText.text != "...")
                loadingText.text += ".";
            else
                loadingText.text = "";

            yield return new WaitForSeconds(0.5f);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="comic">The comic file with the comic information</param>
    /// <param name="comicsScreen"> the UI object where the comic will be played. It must contain an audiosource component too.</param>
    /// <returns></returns>
    public static IEnumerator PlayComic(Comic comic, Image comicsScreen)
    {
        // LOAD SCENE ASYNC
        Debug.LogAssertion("Starting to play Comic");
        
        // SET UP COMIC AUDIO SOURCE
        AudioSource comicAudioSource = comicsScreen.GetComponent<AudioSource>();
        Debug.Log("Audio source reference retrieved");


        // TRY FINISH SETTING UP COMIC AUDIOSOURCE
        try
        {
            comicAudioSource.clip = comic.comicBgMusic;
            comicAudioSource.outputAudioMixerGroup = comic.comicBgMusicOutputAudioMixerGroup; // or channel...
            // PLAY COMIC AUDIO SOURCE - Initial snapshot will be just settled start snapshot.
            comicAudioSource.Play();
            comic.loudSnapshot.TransitionTo(CalculateTransitionIn(comic.comicBgMusicBpm));
        }
        catch (UnassignedReferenceException e)
        {
            Debug.Log("Failed setting up comic audio source. Check Comic File.");
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log("Failed setting up comic audio source. Check Comic File.");
        }
        catch (System.Exception e)
        {
            throw e;
        }



        // ACTIVE COMICS SCREEN
        Debug.LogAssertion("Setting COMICS screen active");
        comicsScreen.gameObject.SetActive(true);

        // SHOW SEQUENCE - through coroutine
        for (int i = 0; i < comic.comicSequence.Length; i++)
        {
            // Put Comic On Screen
            Debug.LogAssertion("Display Comic sprite " + i);
            comicsScreen.sprite = comic.comicSequence[i];


            // Play Corresponding Sound Effect
            Debug.Log("Audio Source " + comicAudioSource.ToString());
            if (i < comic.audioEffectsSequence.Length)
            {
                Debug.Log("Audio Clip" + comic.audioEffectsSequence[i]);
                comicAudioSource.PlayOneShot(comic.audioEffectsSequence[i], 2);
            }


            // continue execution later.
            yield return new WaitForSeconds(comic.timeSequence[i]);
            //yield return null;
        }

        // Fade into silent snapshot...
        try
        {
            float transitionOut = CalculateTransitionOut(comic.comicBgMusicBpm);
            float transitionIn = CalculateTransitionIn(comic.comicBgMusicBpm);

            Debug.LogAssertion("Comic bg bpm " + comic.comicBgMusicBpm);
            Debug.LogAssertion("Comic Transition Out " + transitionOut);

            comic.silentSnapshot.TransitionTo(transitionIn * 2);

        }
        catch (UnassignedReferenceException e)
        {
            Debug.Log("Failed setting up transition in and out for silent snapshot. Check comic file ");
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log("Failed setting up transition in and out for silent snapshot. Check comic file ");
        }
        catch (System.Exception e)
        {
            throw e;
        }



        comicsScreen.gameObject.SetActive(false);
    }

    public static IEnumerator PlayComicAndFreeze(Comic comic, Image comicsScreen)
    {
        // LOAD SCENE ASYNC
        Debug.LogAssertion("Starting to play Comic");

        // SET UP COMIC AUDIO SOURCE
        AudioSource comicAudioSource = comicsScreen.GetComponent<AudioSource>();
        Debug.Log("Audio source reference retrieved");


        // TRY FINISH SETTING UP COMIC AUDIOSOURCE
        try
        {
            comicAudioSource.clip = comic.comicBgMusic;
            comicAudioSource.outputAudioMixerGroup = comic.comicBgMusicOutputAudioMixerGroup; // or channel...
            // PLAY COMIC AUDIO SOURCE - Initial snapshot will be just settled start snapshot.
            comicAudioSource.Play();
            comic.loudSnapshot.TransitionTo(CalculateTransitionIn(comic.comicBgMusicBpm));
        }
        catch (UnassignedReferenceException e)
        {
            Debug.Log("Failed setting up comic audio source. Check Comic File.");
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log("Failed setting up comic audio source. Check Comic File.");
        }
        catch (System.Exception e)
        {
            throw e;
        }



        // ACTIVE COMICS SCREEN
        Debug.LogAssertion("Setting COMICS screen active");
        comicsScreen.gameObject.SetActive(true);

        // SHOW SEQUENCE - through coroutine
        for (int i = 0; i < comic.comicSequence.Length; i++)
        {
            // Put Comic On Screen
            Debug.LogAssertion("Display Comic sprite " + i);
            comicsScreen.sprite = comic.comicSequence[i];


            // Play Corresponding Sound Effect
            Debug.Log("Audio Source " + comicAudioSource.ToString());
            if (i < comic.audioEffectsSequence.Length)
            {
                Debug.Log("Audio Clip" + comic.audioEffectsSequence[i]);
                comicAudioSource.PlayOneShot(comic.audioEffectsSequence[i], 2);
            }


            // continue execution later.
            yield return new WaitForSeconds(comic.timeSequence[i]);
            //yield return null;
        }

        // Fade into silent snapshot...
        try
        {
            float transitionOut = CalculateTransitionOut(comic.comicBgMusicBpm);
            float transitionIn = CalculateTransitionIn(comic.comicBgMusicBpm);

            Debug.LogAssertion("Comic bg bpm " + comic.comicBgMusicBpm);
            Debug.LogAssertion("Comic Transition Out " + transitionOut);

            comic.silentSnapshot.TransitionTo(transitionIn * 2);

        }
        catch (UnassignedReferenceException e)
        {
            Debug.Log("Failed setting up transition in and out for silent snapshot. Check comic file ");
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log("Failed setting up transition in and out for silent snapshot. Check comic file ");
        }
        catch (System.Exception e)
        {
            throw e;
        }

    }


    private static void OnSceneLoaded(Scene sender, LoadSceneMode loadSceneMode)
    {
        // I could call a sound effect here...
    }




}
