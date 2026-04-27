using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private InputAction move;
    [SerializeField] private float rotSpeed = 30, speed = 20;
    private Rigidbody rb;
    [SerializeField] private bool grounded = true;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Vector3 pushbackForce;
    [SerializeField] private bool disabledControl=false;
    [SerializeField] private float disableTime = 1;
    private float lastCollissionTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Obstacle.OnPlayerHit += TakeDamage;
    }

    void TakeDamage()
    {
        rb.AddForce(pushbackForce);
        disabledControl = true;
        Debug.Log("PLAYER GOT HURT");
        lastCollissionTime= Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.timeSinceLevelLoad > lastCollissionTime + disableTime)
            disabledControl = false;
        grounded = Physics.Linecast(transform.position,
            transform.position + Vector3.down, groundMask);

        Color lineCol = grounded ? Color.green : Color.red;

        Debug.DrawLine(transform.position,
            transform.position + Vector3.down, lineCol);
        if (grounded&& !disabledControl)
        {
            rb.AddForce(transform.forward * speed * Time.fixedDeltaTime);
            Vector2 moveInput = move.ReadValue<Vector2>();
           
            transform.Rotate(0, -moveInput.x * rotSpeed * Time.fixedDeltaTime, 0);
            float turnAngle = Mathf.Abs(180 - transform.localEulerAngles.y);
            float speedMult = Mathf.Cos(turnAngle * Mathf.Deg2Rad);
           
        }
    }
}
