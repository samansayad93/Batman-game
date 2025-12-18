using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Light[] _mainLights;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightLow()
    {
        foreach (var _mainLight in _mainLights)
            _mainLight.intensity = 0.2f;
    }

    public void LightHigh()
    {
        foreach (var _mainLight in _mainLights)
            _mainLight.intensity = 1.0f;
    }
}
