using UnityEngine;

/// <summary>
/// Manages global game systems such as scene lighting
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Array of main scene lights controlled by the GameManager
    /// </summary>
    private Light[] _mainLights;

    /// <summary>
    /// Initializes and caches all Light components tagged as "Light" in the scene
    /// </summary>
    void Start()
    {
        GameObject[] lights = GameObject.FindGameObjectsWithTag("Light");
        if (lights == null || lights.Length == 0)
        {
            Debug.LogError("No GameObjects with tag 'Light' found");
        }

        _mainLights = new Light[lights.Length];

        for (int i = 0; i < lights.Length; i++)
        {
            _mainLights[i] = lights[i].GetComponent<Light>();
            if (_mainLights[i] == null)
            {
                Debug.LogError("Light component not found on GameObject with tag 'Light'");
            }
        }
    }

    /// <summary>
    /// Reduces the intensity of all main lights to create a dark or stealth atmosphere
    /// </summary>
    public void LightLow()
    {
        foreach (var _mainLight in _mainLights)
        {
            _mainLight.intensity = 0.2f;
        }
    }

    /// <summary>
    /// Restores the intensity of all main lights to full brightness
    /// </summary>
    public void LightHigh()
    {
        foreach (var _mainLight in _mainLights)
        {
            _mainLight.intensity = 1.0f;
        }
    }
}
