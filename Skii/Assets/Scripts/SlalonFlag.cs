using UnityEngine;

public class SlalonFlag : MonoBehaviour
{
    private enum Direction { Left, Right };
    [SerializeField] private Direction direction;
    [SerializeField] private Material goodMat, badMat;
    public static event GameManager.TimerEvent RacePenalty;
    private bool flagPassed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerControl.player != null && 
            PlayerControl.player.position.z < transform.position.z && flagPassed==false)
        {
            Direction passingDirection = Direction.Right;
            if(PlayerControl.player.position.x < transform.position.x) passingDirection = Direction.Left;
            flagPassed = true;
            Debug.Log("Player passed on: " + passingDirection);
            MeshRenderer renderer = GetComponent<MeshRenderer>();

            if (passingDirection == direction)
            {
                renderer.material=goodMat;
                Debug.Log("passed on correct side");
            }
            else
            {
                renderer.material=badMat;
                RacePenalty.Invoke();
                Debug.Log("passed on incorrect side");
            }
        }
    }
}
