using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfGameScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Text>().text = "Waves Survived \n" + StaticVariables.survivedRounds;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
