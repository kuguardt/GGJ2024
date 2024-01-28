using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDoorSignCenter : MonoBehaviour
{
    public GameObject[] signs;

    public Transform[] signPositions;

    private void Start()
    {
        List<int> selectedNameIndex = new List<int>();

        while(selectedNameIndex.Count < 4)
        {
            int x = Random.Range(0, 6);
            if (!selectedNameIndex.Contains(x))
            {
                selectedNameIndex.Add(x);
            }
        }

        int currentcount = 0;

        foreach (Transform pos in signPositions)
        {
            Instantiate(signs[selectedNameIndex[currentcount]], pos);
            currentcount++;
        }
    }
}
