using System.Diagnostics;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public PlayerData playerData;
    public AudioClip coinSound;
    public float spinSpeed = 180f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
                UnityEngine.Debug.Log("HIT COIN");
                AudioSource.PlayClipAtPoint(coinSound, transform.position);
                Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);
    }
}
