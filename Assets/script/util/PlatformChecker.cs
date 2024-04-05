using UnityEngine;

public class PlatformChecker : MonoBehaviour
{
    void Start()
    {
        #if UNITY_ANDROID || UNITY_IOS
            // Activate this GameObject if the platform is Android or iOS
            gameObject.SetActive(true);
        #else
            // Deactivate this GameObject for other platforms
            gameObject.SetActive(false);
        #endif
    }
}
