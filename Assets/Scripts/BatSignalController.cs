using UnityEngine;

/// <summary>
/// Controls the rotation behavior of the Bat-Signal object
/// </summary>
public class BatSignalController : MonoBehaviour
{
    /// <summary>
    /// Called once per frame to rotate the Bat-Signal continuously
    /// </summary>
    void Update()
    {
        transform.Rotate(0, 0, 0.05f);
    }
}
