using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().Play("Death");
    }

}
