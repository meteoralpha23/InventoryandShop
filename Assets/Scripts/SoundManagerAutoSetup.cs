using UnityEngine;

public class SoundManagerAutoSetup : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnRuntimeMethodLoad()
    {
        // Check if SoundManager already exists
        if (SoundManager.Instance == null)
        {
            Debug.Log("SoundManager not found. Creating one automatically...");
            
            // Create SoundManager GameObject
            GameObject soundManagerGO = new GameObject("SoundManager");
            soundManagerGO.AddComponent<SoundManager>();
            
            Debug.Log("SoundManager created automatically!");
        }
        else
        {
            Debug.Log("SoundManager already exists!");
        }
    }
} 