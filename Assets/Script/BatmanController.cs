using UnityEngine;

public enum BatmanState
{
    Normal,
    Alert,
    Stealth
}

public class BatmanController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 6.0f;
    [SerializeField]
    private float _rotateSpeed = 18.0f;

    [SerializeField]
    BatmanState _currentState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentState = BatmanState.Normal;
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
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            _currentState = BatmanState.Stealth;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentState = BatmanState.Alert;
        }
    }

    void HandleMovement()
    {
        float rotateHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 vmovement = Vector3.forward * moveVertical * CalculateSpeed(1) * Time.deltaTime;
        float hmovement = rotateHorizontal * CalculateSpeed(0) * Time.deltaTime;
        transform.Translate(vmovement);
        transform.Rotate(0, hmovement, 0);
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
}
