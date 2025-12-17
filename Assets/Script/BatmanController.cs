using UnityEngine;

public class BatmanController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5.0f;
    [SerializeField]
    private float _rotateSpeed = 20.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 vmovement = Vector3.forward * moveVertical * _moveSpeed * Time.deltaTime;
        float hmovement = moveHorizontal * _rotateSpeed * Time.deltaTime;
        transform.Translate(vmovement);
        transform.Rotate(0, hmovement, 0);
    }
}
