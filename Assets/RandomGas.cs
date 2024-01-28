using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGas : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private GameObject Blank;

    [SerializeField]private GameObject Fart;

    [SerializeField]private float minRate = 0f;
    [SerializeField]private float maxRate = 3f;
    void Start()
    {
        UnlimitedFart();
        
        
    }
    

    private void UnlimitedFart()
    {
        Invoke("RandomGasPlay", Random.Range(minRate, maxRate));
        AudioManager.instance.PlaySound("Fart" + RandomGasSound(1, 3));
    }

    void RandomGasPlay()
    {
        if (GetComponent<PlayerMovement>() == null || GetComponent<PlayerMovement>().movementInput.magnitude == 0)
        {
            Debug.Log(Fart);
            var blank = Instantiate(Blank, transform);
            Instantiate(Fart, blank.transform);
            blank.transform.parent = null;
            Destroy(blank,2f);
        }
        UnlimitedFart();
    }
    
    private int RandomGasSound(int min, int max)
    {
        return Random.Range(min, max + 1);
    }
}
