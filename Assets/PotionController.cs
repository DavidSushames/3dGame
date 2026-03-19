using UnityEngine;

public class PotionController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("HIT POTION");
            Destroy(gameObject);
        }
    }
    //

    // Update is called once per frame
    void Update()
    {
        
    }
}
