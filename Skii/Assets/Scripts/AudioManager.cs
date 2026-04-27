using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField]private AudioClip collisionSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        audioSource= GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        Obstacle.OnPlayerHit += PlayCollisionSound;
    }

    // Update is called once per frame
    private void PlayCollisionSound()
    {
        audioSource.PlayOneShot(collisionSound);
    }
}
