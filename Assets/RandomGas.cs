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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UnlimitedFart()
    {
        Invoke("RandomGasPlay", Random.Range(minRate, maxRate));
    }

    void RandomGasPlay()
    {
        if (GetComponent<PlayerMovement>().movementInput.magnitude == 0)
        {
            Debug.Log(Fart);
            var blank = Instantiate(Blank, transform.);
            Instantiate(Fart, blank.transform);
            blank.transform.parent = null;
            Destroy(blank,2f);
        }
        UnlimitedFart();
    }
    
}
