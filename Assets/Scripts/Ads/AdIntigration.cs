using UnityEngine;
using UnityEngine.Advertisements;

public class AdIntigration : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId = "YOUR_ANDROID_GAME_ID";
    [SerializeField] bool _testMode = true;
    private string _gameId;

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_ANDROID
        _gameId = _androidGameId;
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        //Debug.Log("Unity Ads initialization complete.");
        // Optionally notify other scripts that initialization is complete
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        //Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
