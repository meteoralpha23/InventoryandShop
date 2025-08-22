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

    [Header("Background Music")]
    [SerializeField] private AudioClip sceneBackgroundMusic;
    [SerializeField] private bool playBackgroundMusicOnStart = true;
    [SerializeField] private float backgroundMusicFadeInTime = 2f;

    



    [Header("Merchant Sounds")]
    [SerializeField] private AudioClip merchantGreetingSound;
    [SerializeField] private AudioClip merchantGoodbyeSound;
    [SerializeField] private AudioClip merchantLaughSound;
    [SerializeField] private AudioClip merchantDisappointedSound;

    [Header("Settings")]
    [SerializeField] private float masterVolume = 1f;
    [SerializeField] private float musicVolume = 0.3f; // Set to 0.3 for background music
    [SerializeField] private float sfxVolume = 0.6f; // Reduced from 1f to prevent overlapping
    [SerializeField] private float uiVolume = 0.6f; // Reduced from 0.8f to prevent overlapping with SFX
    
    [Header("Sound Cooldowns")]
    [SerializeField] private float merchantSoundCooldown = 1.5f; // Prevent merchant sounds from overlapping

    private float lastMerchantSoundTime = 0f; // Track when last merchant sound was played

    private void Awake()
    {
        Debug.Log("SoundManager Awake() called");
        
        // Singleton pattern - only set instance if none exists
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("SoundManager Instance set to: " + Instance);
            
            // Validate audio sources are assigned in inspector
            ValidateAudioSources();
            
            // Configure audio sources
            ConfigureAudioSources();
            
            Debug.Log("SoundManager initialized successfully! Instance: " + Instance);
        }
        else if (Instance != this)
        {
            Debug.LogWarning("Another SoundManager already exists! Destroying this duplicate.");
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Start playing background music if enabled
        if (playBackgroundMusicOnStart && sceneBackgroundMusic != null)
        {
            StartBackgroundMusic();
        }
    }


    private void ValidateAudioSources()
    {
        if (musicSource == null)
        {
            Debug.LogError("Music Source not assigned in Inspector! Please assign an AudioSource component.");
        }
        else
        {
            Debug.Log($"Music Source assigned: {musicSource.name}");
        }
        
        if (sfxSource == null)
        {
            Debug.LogError("SFX Source not assigned in Inspector! Please assign an AudioSource component.");
        }
        else
        {
            Debug.Log($"SFX Source assigned: {sfxSource.name}");
        }
        
        if (uiSource == null)
        {
            Debug.LogError("UI Source not assigned in Inspector! Please assign an AudioSource component.");
        }
        else
        {
            Debug.Log($"UI Source assigned: {uiSource.name}");
        }
    }

    private void ConfigureAudioSources()
    {
        Debug.Log("=== CONFIGURING AUDIO SOURCES ===");
        
        // Music source (looping, lower volume)
        if (musicSource != null)
        {
            musicSource.loop = true;
            musicSource.volume = musicVolume * masterVolume;
            musicSource.playOnAwake = false;
            Debug.Log($"Music Source - Loop: {musicSource.loop}, Volume: {musicSource.volume}, PlayOnAwake: {musicSource.playOnAwake}");
        }

        // SFX source (one-shot, full volume)
        if (sfxSource != null)
        {
            sfxSource.loop = false;
            sfxSource.volume = sfxVolume * masterVolume;
            sfxSource.playOnAwake = false;
            Debug.Log($"SFX Source - Loop: {sfxSource.loop}, Volume: {sfxSource.volume}, PlayOnAwake: {sfxSource.playOnAwake}");
        }

        // UI source (one-shot, medium volume)
        if (uiSource != null)
        {
            uiSource.loop = false;
            uiSource.volume = uiVolume * masterVolume;
            uiSource.playOnAwake = false;
            Debug.Log($"UI Source - Loop: {uiSource.loop}, Volume: {uiSource.volume}, PlayOnAwake: {uiSource.playOnAwake}");
        }
        
        Debug.Log("=== AUDIO SOURCES CONFIGURED ===");
    }

    #region UI Sounds
    public void PlayButtonHover()
    {
        Debug.Log("PlayButtonHover called, buttonHoverSound: " + (buttonHoverSound != null ? buttonHoverSound.name : "NULL"));
        PlayUISound(buttonHoverSound);
    }

    public void PlayButtonClick()
    {
        Debug.Log("PlayButtonClick called, buttonClickSound: " + (buttonClickSound != null ? buttonClickSound.name : "NULL"));
        PlayUISound(buttonClickSound);
    }

    public void PlayTabSwitch()
    {
        Debug.Log("PlayTabSwitch called, tabSwitchSound: " + (tabSwitchSound != null ? tabSwitchSound.name : "NULL"));
        PlayUISound(tabSwitchSound);
    }

    public void PlayPopupOpen()
    {
        Debug.Log("PlayPopupOpen called, popupOpenSound: " + (popupOpenSound != null ? popupOpenSound.name : "NULL"));
        PlayUISound(popupOpenSound);
    }

    public void PlayPopupClose()
    {
        Debug.Log("PlayPopupClose called, popupCloseSound: " + (popupOpenSound != null ? popupCloseSound.name : "NULL"));
        PlayUISound(popupCloseSound);
    }
    #endregion

    #region Transaction Sounds
    public void PlayBuySuccess()
    {
        Debug.Log("PlayBuySuccess called, buySuccessSound: " + (buySuccessSound != null ? buySuccessSound.name : "NULL"));
        PlaySFXSound(buySuccessSound);
    }

    public void PlayBuyFail()
    {
        Debug.Log("PlayBuyFail called, buyFailSound: " + (buyFailSound != null ? buyFailSound.name : "NULL"));
        PlaySFXSound(buyFailSound);
    }

    public void PlaySellSuccess()
    {
        Debug.Log("PlaySellSuccess called, sellSuccessSound: " + (sellSuccessSound != null ? sellSuccessSound.name : "NULL"));
        PlaySFXSound(sellSuccessSound);
    }

    public void PlayNotEnoughGold()
    {
        Debug.Log("PlayNotEnoughGold called, notEnoughGoldSound: " + (notEnoughGoldSound != null ? notEnoughGoldSound.name : "NULL"));
        PlaySFXSound(notEnoughGoldSound);
    }

    public void PlayInventoryFull()
    {
        Debug.Log("PlayInventoryFull called, inventoryFullSound: " + (inventoryFullSound != null ? inventoryFullSound.name : "NULL"));
        PlaySFXSound(inventoryFullSound);
    }
    #endregion

    #region Inventory Sounds
    // Removed undefined sound methods
    #endregion

    #region Game Feel Sounds
    // Removed undefined sound methods
    #endregion

    #region Merchant Sounds
    public void PlayMerchantGreeting()
    {
        if (CanPlayMerchantSound())
        {
            Debug.Log("PlayMerchantGreeting called, merchantGreetingSound: " + (merchantGreetingSound != null ? merchantGreetingSound.name : "NULL"));
            PlaySFXSound(merchantGreetingSound);
            lastMerchantSoundTime = Time.time;
        }
        else
        {
            Debug.Log("Merchant greeting sound blocked by cooldown");
        }
    }

    public void PlayMerchantGoodbye()
    {
        if (CanPlayMerchantSound())
        {
            Debug.Log("PlayMerchantGoodbye called, merchantGoodbyeSound: " + (merchantGoodbyeSound != null ? merchantGoodbyeSound.name : "NULL"));
            PlaySFXSound(merchantGoodbyeSound);
            lastMerchantSoundTime = Time.time;
        }
        else
        {
            Debug.Log("Merchant goodbye sound blocked by cooldown");
        }
    }

    // Removed undefined merchantHaggleSound method

    public void PlayMerchantLaugh()
    {
        if (CanPlayMerchantSound())
        {
            Debug.Log("PlayMerchantLaugh called, merchantLaughSound: " + (merchantLaughSound != null ? merchantLaughSound.name : "NULL"));
            PlaySFXSound(merchantLaughSound);
            lastMerchantSoundTime = Time.time;
        }
        else
        {
            Debug.Log("Merchant laugh sound blocked by cooldown");
        }
    }

    public void PlayMerchantDisappointed()
    {
        if (CanPlayMerchantSound())
        {
            Debug.Log("PlayMerchantDisappointed called, merchantDisappointedSound: " + (merchantDisappointedSound != null ? merchantDisappointedSound.name : "NULL"));
            PlaySFXSound(merchantDisappointedSound);
            lastMerchantSoundTime = Time.time;
        }
        else
        {
            Debug.Log("Merchant disappointed sound blocked by cooldown");
        }
    }
    
    private bool CanPlayMerchantSound()
    {
        return Time.time - lastMerchantSoundTime >= merchantSoundCooldown;
    }
    
    public void StopMerchantSounds()
    {
        if (sfxSource != null && sfxSource.isPlaying)
        {
            sfxSource.Stop();
            Debug.Log("Stopped currently playing merchant sounds");
        }
    }
    
    public void ResetMerchantSoundCooldown()
    {
        lastMerchantSoundTime = 0f;
        Debug.Log("Reset merchant sound cooldown");
    }
    #endregion

    #region Background Music
    public void StartBackgroundMusic()
    {
        if (sceneBackgroundMusic != null && musicSource != null)
        {
            musicSource.clip = sceneBackgroundMusic;
            musicSource.loop = true;
            musicSource.volume = 0f; // Start silent for fade in
            musicSource.Play();
            
            // Start fade in coroutine
            StartCoroutine(FadeInBackgroundMusic());
            
            Debug.Log("Started playing background music: " + sceneBackgroundMusic.name);
        }
        else
        {
            Debug.LogWarning("Cannot start background music - clip or musicSource is null");
        }
    }

    public void StopBackgroundMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            StartCoroutine(FadeOutBackgroundMusic());
        }
    }

    public void PauseBackgroundMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Pause();
            Debug.Log("Background music paused");
        }
    }

    public void ResumeBackgroundMusic()
    {
        if (musicSource != null && musicSource.clip != null)
        {
            musicSource.UnPause();
            Debug.Log("Background music resumed");
        }
    }

    public void SetBackgroundMusicVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume * musicVolume * masterVolume;
        }
    }

    public bool IsBackgroundMusicPlaying()
    {
        return musicSource != null && musicSource.isPlaying;
    }

    public AudioClip GetCurrentBackgroundMusic()
    {
        return musicSource != null ? musicSource.clip : null;
    }

    private System.Collections.IEnumerator FadeInBackgroundMusic()
    {
        float targetVolume = musicVolume * masterVolume;
        float currentVolume = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < backgroundMusicFadeInTime)
        {
            elapsedTime += Time.deltaTime;
            currentVolume = Mathf.Lerp(0f, targetVolume, elapsedTime / backgroundMusicFadeInTime);
            musicSource.volume = currentVolume;
            yield return null;
        }

        musicSource.volume = targetVolume;
        Debug.Log("Background music fade in completed");
    }

    private System.Collections.IEnumerator FadeOutBackgroundMusic()
    {
        float startVolume = musicSource.volume;
        float elapsedTime = 0f;
        float fadeOutTime = 1f; // 1 second fade out

        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeOutTime);
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume; // Restore original volume
        Debug.Log("Background music stopped");
    }
    #endregion

    #region Helper Methods
    private void PlayUISound(AudioClip clip)
    {
        Debug.Log("PlayUISound called - clip: " + (clip != null ? clip.name : "NULL") + ", uiSource: " + (uiSource != null ? "EXISTS" : "NULL"));
        if (clip != null && uiSource != null)
        {
            uiSource.PlayOneShot(clip);
            Debug.Log("Playing UI sound: " + clip.name);
        }
        else
        {
            Debug.LogWarning("Cannot play UI sound - clip or uiSource is null");
        }
    }

    private void PlaySFXSound(AudioClip clip)
    {
        Debug.Log("PlaySFXSound called - clip: " + (clip != null ? clip.name : "NULL") + ", sfxSource: " + (sfxSource != null ? "EXISTS" : "NULL"));
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
            Debug.Log("Playing SFX sound: " + clip.name);
        }
        else
        {
            Debug.LogWarning("Cannot play SFX sound - clip or sfxSource is null");
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
        Debug.Log("=== SOUND SYSTEM TEST ===");
        Debug.Log("SoundManager Instance: " + (Instance != null ? "EXISTS" : "NULL"));
        Debug.Log("Music Source: " + (musicSource != null ? "EXISTS" : "NULL"));
        Debug.Log("SFX Source: " + (sfxSource != null ? "EXISTS" : "NULL"));
        Debug.Log("UI Source: " + (uiSource != null ? "EXISTS" : "NULL"));
        
        if (buttonClickSound != null)
        {
            Debug.Log("Button click sound assigned: " + buttonClickSound.name);
            PlayButtonClick();
            Debug.Log("Button click sound played successfully!");
        }
        else
        {
            Debug.LogWarning("No button click sound assigned!");
        }
        
        // Test a simple beep sound
        if (sfxSource != null)
        {
            Debug.Log("Testing SFX source with a simple beep...");
            // Create a simple beep sound
            AudioClip beep = AudioClip.Create("TestBeep", 44100, 1, 44100, false);
            sfxSource.PlayOneShot(beep);
            Debug.Log("Test beep played!");
        }
        
        Debug.Log("=== END SOUND TEST ===");
    }

    // Test method specifically for merchant sounds
    public void TestMerchantSounds()
    {
        Debug.Log("=== MERCHANT SOUNDS TEST ===");
        
        // Test each merchant sound
        Debug.Log("Testing PlayMerchantGreeting...");
        PlayMerchantGreeting();
        
        Debug.Log("Testing PlayMerchantGoodbye...");
        PlayMerchantGoodbye();
        
        Debug.Log("Testing PlayMerchantLaugh...");
        PlayMerchantLaugh();
        
        Debug.Log("Testing PlayMerchantDisappointed...");
        PlayMerchantDisappointed();
        
        Debug.Log("=== END MERCHANT SOUNDS TEST ===");
    }

    // Test method specifically for background music
    public void TestBackgroundMusic()
    {
        Debug.Log("=== BACKGROUND MUSIC TEST ===");
        
        if (sceneBackgroundMusic != null)
        {
            Debug.Log($"Testing background music: {sceneBackgroundMusic.name}");
            
            if (IsBackgroundMusicPlaying())
            {
                Debug.Log("Background music is currently playing - testing pause/resume...");
                PauseBackgroundMusic();
                
                // Wait a moment then resume
                StartCoroutine(TestPauseResume());
            }
            else
            {
                Debug.Log("Starting background music...");
                StartBackgroundMusic();
            }
        }
        else
        {
            Debug.LogWarning("No background music assigned! Please assign in inspector.");
        }
        
        Debug.Log("=== END BACKGROUND MUSIC TEST ===");
    }

    private System.Collections.IEnumerator TestPauseResume()
    {
        yield return new WaitForSeconds(1f);
        ResumeBackgroundMusic();
    }

    // Comprehensive audio system diagnostic
    public void DiagnoseAudioSystem()
    {
        Debug.Log("=== AUDIO SYSTEM DIAGNOSTIC ===");
        
        // Check Unity Audio System
        Debug.Log($"Unity Audio Enabled: {AudioListener.pause == false}");
        Debug.Log($"Audio Listener Count: {FindObjectsOfType<AudioListener>().Length}");
        
        // Check AudioSources
        Debug.Log($"Music Source: {(musicSource != null ? "EXISTS" : "NULL")}");
        if (musicSource != null)
        {
            Debug.Log($"Music Source - Enabled: {musicSource.enabled}, Volume: {musicSource.volume}, Mute: {musicSource.mute}");
        }
        
        Debug.Log($"SFX Source: {(sfxSource != null ? "EXISTS" : "NULL")}");
        if (sfxSource != null)
        {
            Debug.Log($"SFX Source - Enabled: {sfxSource.enabled}, Volume: {sfxSource.volume}, Mute: {sfxSource.mute}");
        }
        
        Debug.Log($"UI Source: {(uiSource != null ? "EXISTS" : "NULL")}");
        if (uiSource != null)
        {
            Debug.Log($"UI Source - Enabled: {uiSource.enabled}, Volume: {uiSource.volume}, Mute: {uiSource.mute}");
        }
        
        // Check Volume Settings
        Debug.Log($"Master Volume: {masterVolume}");
        Debug.Log($"Music Volume: {musicVolume}");
        Debug.Log($"SFX Volume: {sfxVolume}");
        Debug.Log($"UI Volume: {uiVolume}");
        
        // Check Audio Clips
        Debug.Log($"Button Click Sound: {(buttonClickSound != null ? buttonClickSound.name : "NULL")}");
        Debug.Log($"Merchant Greeting Sound: {(merchantGreetingSound != null ? merchantGreetingSound.name : "NULL")}");
        Debug.Log($"Scene Background Music: {(sceneBackgroundMusic != null ? sceneBackgroundMusic.name : "NULL")}");
        
        // Check Background Music Status
        Debug.Log($"Background Music Playing: {IsBackgroundMusicPlaying()}");
        Debug.Log($"Background Music Auto-Start: {playBackgroundMusicOnStart}");
        Debug.Log($"Background Music Fade-In Time: {backgroundMusicFadeInTime}s");
        
        // Test basic audio playback
        if (sfxSource != null)
        {
            Debug.Log("Testing basic audio playback with generated beep...");
            try
            {
                // Create a simple test tone
                int sampleRate = 44100;
                int frequency = 440; // A4 note
                float duration = 0.1f;
                int samples = (int)(sampleRate * duration);
                
                float[] data = new float[samples];
                for (int i = 0; i < samples; i++)
                {
                    data[i] = Mathf.Sin(2 * Mathf.PI * frequency * i / sampleRate);
                }
                
                AudioClip testClip = AudioClip.Create("TestTone", samples, 1, sampleRate, false);
                testClip.SetData(data, 0);
                
                sfxSource.PlayOneShot(testClip);
                Debug.Log("Test tone played successfully!");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error playing test tone: {e.Message}");
            }
        }
        
        Debug.Log("=== END AUDIO DIAGNOSTIC ===");
    }
    #endregion
} 