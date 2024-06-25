using UnityEngine;

public class Planet : MonoBehaviour
{
    public int level;
    public MeshRenderer mesh;

    private bool _isCrash;
    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && _isCrash == false)
        {
            _isCrash = true;
            BallSpawnPoint.Instance.ReturnSpawnerPosition();
            BallSpawnPoint.Instance.InstantiatePlanet();
        }
        else if (collision != null)
        {
            if (collision.gameObject.GetComponent<Planet>() && collision.gameObject.GetComponent<Planet>().level == this.level)
            {
                Destroy(collision.gameObject);
                level++;
                mesh.material = BallSpawnPoint.Instance.materials[level];
                this.transform.localScale = new Vector3(5 + 2 * level, 5 + 2 * level, 5 + 2 * level);
            }
        }
    }
}
