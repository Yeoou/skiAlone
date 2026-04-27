using UnityEngine;

public class ObstacleDestroy : Obstacle
{
    private void OnCollisionEnter(Collision collision)
    {
        base.OnCollision(collision);
        Destroy(gameObject);
    }

}
