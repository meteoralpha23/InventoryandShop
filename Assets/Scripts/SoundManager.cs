using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource uiSource;

    [Header("UI Sounds")]
    [SerializeField] private AudioClip buttonHoverSound;
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip tabSwitchSound;
    [SerializeField] private AudioClip popupOpenSound;
    [SerializeField] private AudioClip popupCloseSound;

    [Header("Transaction Sounds")]
    [SerializeField] private AudioClip buySuccessSound;
    [SerializeField] private AudioClip buyFailSound;
    [SerializeField] private AudioClip sellSuccessSound;
    [SerializeField] private AudioClip notEnoughGoldSound;
    [SerializeField] private AudioClip inventoryFullSound;

    [Header("Inventory Sounds")]
    [SerializeField] private AudioClip itemAddedSound;
    [SerializeField] private AudioClip itemRemovedSound;
    [SerializeField] private AudioClip weightWarningSound;
    [SerializeField] private AudioClip categorySwitchSound;

    [Header("Game Feel Sounds")]
    [SerializeField] private AudioClip shopOpenSound;
    [SerializeField] private AudioClip shopCloseSound;
    [SerializeField] private AudioClip quantityChangeSound;
    [SerializeField] private AudioClip confirmationSound;
    [SerializeField] private AudioClip cancelSound;

    [Header("Merchant Sounds")]
    [SerializeField] private AudioClip merchantGreetingSound;
    [SerializeField] private AudioClip merchantGoodbyeSound;
    [SerializeField] private AudioClip merchantHaggleSound;
    [SerializeField] private AudioClip merchantLaughSound;
    [SerializeField] private AudioClip merchantDisappointedSound;

    [Header("Settings")]
    [SerializeField] private float masterVolume = 1f;
    [SerializeField] private float musicVolume = 0.7f;
    [SerializeField] private float sfxVolume = 1f;
    [SerializeField] private float uiVolume = 0.8f;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Initialize audio sources if not assigned
        if (musicSource == null)
            musicSource = gameObject.AddComponent<AudioSource>();
        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();
        if (uiSource == null)
            uiSource = gameObject.AddComponent<AudioSource>();

        // Configure audio sources
        ConfigureAudioSources();
        
        Debug.Log("SoundManager initialized successfully!");
    }

    private void ConfigureAudioSources()
    {
        // Music source (looping, lower volume)
        musicSource.loop = true;
        musicSource.volume = musicVolume * masterVolume;
        musicSource.playOnAwake = false;

        // SFX source (one-shot, full volume)
        sfxSource.loop = false;
        sfxSource.volume = sfxVolume * masterVolume;
        sfxSource.playOnAwake = false;

        // UI source (one-shot, medium volume)
        uiSource.loop = false;
        uiSource.volume = uiVolume * masterVolume;
        uiSource.playOnAwake = false;
    }

    #region UI Sounds
    public void PlayButtonHover()
    {
        PlayUISound(buttonHoverSound);
    }

    public void PlayButtonClick()
    {
        PlayUISound(buttonClickSound);
    }

    public void PlayTabSwitch()
    {
        PlayUISound(tabSwitchSound);
    }

    public void PlayPopupOpen()
    {
        PlayUISound(popupOpenSound);
    }

    public void PlayPopupClose()
    {
        PlayUISound(popupCloseSound);
    }
    #endregion

    #region Transaction Sounds
    public void PlayBuySuccess()
    {
        PlaySFXSound(buySuccessSound);
    }

    public void PlayBuyFail()
    {
        PlaySFXSound(buyFailSound);
    }

    public void PlaySellSuccess()
    {
        PlaySFXSound(sellSuccessSound);
    }

    public void PlayNotEnoughGold()
    {
        PlaySFXSound(notEnoughGoldSound);
    }

    public void PlayInventoryFull()
    {
        PlaySFXSound(inventoryFullSound);
    }
    #endregion

    #region Inventory Sounds
    public void PlayItemAdded()
    {
        PlaySFXSound(itemAddedSound);
    }

    public void PlayItemRemoved()
    {
        PlaySFXSound(itemRemovedSound);
    }

    public void PlayWeightWarning()
    {
        PlaySFXSound(weightWarningSound);
    }

    public void PlayCategorySwitch()
    {
        PlaySFXSound(categorySwitchSound);
    }
    #endregion

    #region Game Feel Sounds
    public void PlayShopOpen()
    {
        PlaySFXSound(shopOpenSound);
    }

    public void PlayShopClose()
    {
        PlaySFXSound(shopCloseSound);
    }

    public void PlayQuantityChange()
    {
        PlayUISound(quantityChangeSound);
    }

    public void PlayConfirmation()
    {
        PlayUISound(confirmationSound);
    }

    public void PlayCancel()
    {
        PlayUISound(cancelSound);
    }
    #endregion

    #region Merchant Sounds
    public void PlayMerchantGreeting()
    {
        PlaySFXSound(merchantGreetingSound);
    }

    public void PlayMerchantGoodbye()
    {
        PlaySFXSound(merchantGoodbyeSound);
    }

    public void PlayMerchantHaggle()
    {
        PlaySFXSound(merchantHaggleSound);
    }

    public void PlayMerchantLaugh()
    {
        PlaySFXSound(merchantLaughSound);
    }

    public void PlayMerchantDisappointed()
    {
        PlaySFXSound(merchantDisappointedSound);
    }
    #endregion

    #region Helper Methods
    private void PlayUISound(AudioClip clip)
    {
        if (clip != null && uiSource != null)
        {
            uiSource.PlayOneShot(clip);
        }
    }

    private void PlaySFXSound(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip != null && musicSource != null)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }
    #endregion

    #region Volume Controls
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        UpdateAllVolumes();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (musicSource != null)
            musicSource.volume = musicVolume * masterVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        if (sfxSource != null)
            sfxSource.volume = sfxVolume * masterVolume;
    }

    public void SetUIVolume(float volume)
    {
        uiVolume = Mathf.Clamp01(volume);
        if (uiSource != null)
            uiSource.volume = uiVolume * masterVolume;
    }

    private void UpdateAllVolumes()
    {
        if (musicSource != null)
            musicSource.volume = musicVolume * masterVolume;
        if (sfxSource != null)
            sfxSource.volume = sfxVolume * masterVolume;
        if (uiSource != null)
            uiSource.volume = uiVolume * masterVolume;
    }

    public float GetMasterVolume() => masterVolume;
    public float GetMusicVolume() => musicVolume;
    public float GetSFXVolume() => sfxVolume;
    public float GetUIVolume() => uiVolume;
    #endregion

    #region Utility Methods
    public void MuteAll()
    {
        masterVolume = 0f;
        UpdateAllVolumes();
    }

    public void UnmuteAll()
    {
        masterVolume = 1f;
        UpdateAllVolumes();
    }

    public void StopAllSounds()
    {
        if (musicSource != null) musicSource.Stop();
        if (sfxSource != null) sfxSource.Stop();
        if (uiSource != null) uiSource.Stop();
    }

    // Test method to verify sound system is working
    public void TestSound()
    {
        Debug.Log("Testing sound system...");
        if (buttonClickSound != null)
        {
            PlayButtonClick();
            Debug.Log("Button click sound played successfully!");
        }
        else
        {
            Debug.LogWarning("No button click sound assigned!");
        }
    }
    #endregion
} 