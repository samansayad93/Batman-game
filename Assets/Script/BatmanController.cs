using System.Collections;
using UnityEngine;

public enum BatmanState
{
    Normal,
    Alert,
    Stealth
}

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

    [SerializeField]
    private BatmanState _currentState;

    private Coroutine _blinkCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleState();
    }

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

    void HandleMovement()
    {
        float rotateHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 vmovement = Vector3.up * moveVertical * CalculateSpeed(1) * Time.deltaTime;
        float hmovement = rotateHorizontal * CalculateSpeed(0) * Time.deltaTime;
        transform.Translate(vmovement);
        transform.Rotate(0, 0, hmovement);
    }

    float CalculateSpeed(int direction)
    {
        if (_currentState == BatmanState.Stealth)
        {
            if (direction == 1)
                return _moveSpeed / 3;
            else
                return _rotateSpeed / 3;
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (direction == 1)
                return _moveSpeed * 2;
            else
                return _rotateSpeed * 2;
        }
        else
        {
            if (direction == 1)
                return _moveSpeed;
            else
                return _rotateSpeed;
        }
    }

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

    void StartAlarm()
    {
        if (_blinkCoroutine == null)
        {
            _redLight.SetActive(true);
            _blinkCoroutine = StartCoroutine(BlinkLights());
            _alarmAudioSource.Play();
        }
    }

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
