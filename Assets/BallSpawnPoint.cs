using UnityEngine;

public class BallSpawnPoint : MonoBehaviour
{
    public GameObject[] balls;
    void Start()
    {
        Instantiate(balls[0].gameObject, transform.parent);
    }
}
