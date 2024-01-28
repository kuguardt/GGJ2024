using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        PlayerConfigurationManager.instance.inConfigRoom = true;
        PlayerConfigurationManager.instance.ResetConfigs();
    }
}
