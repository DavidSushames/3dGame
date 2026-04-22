using UnityEngine;

public class PotionController : MonoBehaviour
{
    public PlayerData playerData;
    public AudioClip potionSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (playerData.health < 100)
            {
                Debug.Log("HIT POTION");
                AudioSource.PlayClipAtPoint(potionSound, transform.position);
                Destroy(gameObject);
            }
        }
    }
    //

    // Update is called once per frame
    void Update()
    {
        
    }
}
