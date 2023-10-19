using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private const string MUSIC_EFFECT_VOLUME_PLAYER_PREFS = "MusicEffectVolume";

    private float volumeModifier = 0.5f;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        volumeModifier = PlayerPrefs.GetFloat(MUSIC_EFFECT_VOLUME_PLAYER_PREFS, 0.5f);

        audioSource.volume = volumeModifier;
    }
    public void ChangeVolume()
    {
        volumeModifier += 0.1f;

        if (volumeModifier > 1)
            volumeModifier = 0;

        audioSource.volume = volumeModifier;

        PlayerPrefs.SetFloat(MUSIC_EFFECT_VOLUME_PLAYER_PREFS, volumeModifier);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return Mathf.Round(volumeModifier * 10f);
    }
}
