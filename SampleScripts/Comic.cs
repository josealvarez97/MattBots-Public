using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[CreateAssetMenu(menuName = "Comic File")]
public class Comic : ScriptableObject {


    public Sprite[] comicSequence;
    public Sprite loadingScreenComic;

    public float[] timeSequence;

    public AudioClip[] audioEffectsSequence;

    public AudioClip comicBgMusic;
    public float comicBgMusicBpm;
    public AudioMixerGroup comicBgMusicOutputAudioMixerGroup;
    public AudioMixerSnapshot loudSnapshot;
    public AudioMixerSnapshot silentSnapshot;




}
