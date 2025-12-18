using System.Collections;
using UnityEngine;

/// <summary>
/// Represents the possible states of Batman
/// </summary>
public enum BatmanState
{
    Normal,
    Alert,
    Stealth
}

/// <summary>
/// Controls Batman movement, state, lights, alarm, and interactions
/// </summary>
public class BatmanController : MonoBehaviour
{
    private GameObject _redLight;
    private GameObject _blueLight;
    private AudioSource _alarmAudioSource;
    private GameObject _batSignal;

    [SerializeField]
    private AudioClip _alarmSound;

    private GameManager _gameManager;

    [SerializeField]
    private float _moveSpeed = 6.0f;
    [SerializeField]
    private float _rotateSpeed = 18.0f;

    private BatmanState _currentState;

    private Coroutine _blinkCoroutine;

    /// <summary>
    /// Initializes references, sets default state, and disables lights and signals at start
    /// </summary>
    void Start()
    {
        _redLight = transform.Find("RedLight").gameObject;
        if (_redLight == null)
        {
            Debug.LogError("RedLight not found");
        }

        _blueLight = transform.Find("BlueLight").gameObject;
        if (_blueLight == null)
        {
            Debug.LogError("BlueLight not found");
        }

        _alarmAudioSource = transform.GetComponent<AudioSource>();
        if (_alarmAudioSource == null)
        {
            Debug.LogError("AudioSource not found");
        }

        _batSignal = transform.Find("BatSignal").gameObject;
        if (_batSignal == null)
        {
            Debug.LogError("BatSignal not found");
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("GameManager not found");
        }

        _alarmAudioSource.clip = _alarmSound;
        _currentState = BatmanState.Normal;

        _redLight.SetActive(false);
        _blueLight.SetActive(false);
        _batSignal.SetActive(false);
    }

    /// <summary>
    /// Called every frame to handle movement and state changes
    /// </summary>
    void Update()
    {
        HandleMovement();
        HandleState();
    }

    /// <summary>
    /// Handles user input for switching Batman states and toggling the Bat-Signal
    /// </summary>
    void HandleState()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            _currentState = BatmanState.Normal;
            HandleLightsandAlarm();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            _currentState = BatmanState.Stealth;
            HandleLightsandAlarm();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentState = BatmanState.Alert;
            HandleLightsandAlarm();
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            _batSignal.SetActive(!_batSignal.activeSelf);
        }
    }

    /// <summary>
    /// Handles Batman movement and rotation based on player input
    /// </summary>
    void HandleMovement()
    {
        float rotateHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 vmovement = Vector3.up * moveVertical * CalculateSpeed(1) * Time.deltaTime;
        float hmovement = rotateHorizontal * CalculateSpeed(0) * Time.deltaTime;

        transform.Translate(vmovement);
        transform.Rotate(0, 0, hmovement);
    }

    /// <summary>
    /// Calculates movement or rotation speed based on current state and input
    /// </summary>
    /// <param name="direction">
    /// 1 = movement speed, 0 = rotation speed
    /// </param>
    /// <returns>Calculated speed value</returns>
    float CalculateSpeed(int direction)
    {
        if (_currentState == BatmanState.Stealth)
        {
            return direction == 1 ? _moveSpeed / 3 : _rotateSpeed / 3;
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            return direction == 1 ? _moveSpeed * 2 : _rotateSpeed * 2;
        }

        return direction == 1 ? _moveSpeed : _rotateSpeed;
    }

    /// <summary>
    /// Controls lights and alarm behavior based on Batman's current state
    /// </summary>
    void HandleLightsandAlarm()
    {
        switch (_currentState)
        {
            case BatmanState.Normal:
                _gameManager.LightHigh();
                StopAlarm();
                break;

            case BatmanState.Alert:
                _gameManager.LightHigh();
                StartAlarm();
                break;

            case BatmanState.Stealth:
                _gameManager.LightLow();
                StopAlarm();
                break;
        }
    }

    /// <summary>
    /// Starts the alarm sound and blinking police lights
    /// </summary>
    void StartAlarm()
    {
        if (_blinkCoroutine == null)
        {
            _redLight.SetActive(true);
            _blinkCoroutine = StartCoroutine(BlinkLights());
            _alarmAudioSource.Play();
        }
    }

    /// <summary>
    /// Stops the alarm sound and disables all warning lights
    /// </summary>
    void StopAlarm()
    {
        if (_blinkCoroutine != null)
        {
            StopCoroutine(_blinkCoroutine);
            _blinkCoroutine = null;
        }

        _redLight.SetActive(false);
        _blueLight.SetActive(false);
        _alarmAudioSource.Stop();
    }

    /// <summary>
    /// Coroutine that alternates red and blue lights to simulate a police alarm
    /// </summary>
    /// <returns>IEnumerator for coroutine execution</returns>
    IEnumerator BlinkLights()
    {
        while (true)
        {
            _redLight.SetActive(!_redLight.activeSelf);
            _blueLight.SetActive(!_blueLight.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
