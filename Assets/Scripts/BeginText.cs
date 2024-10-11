using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeginText : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMP_Text introductionField;
    void Start()
    {
        StartCoroutine(Introduction());
    }
    IEnumerator Introduction()
    {
        introductionField.enabled = true;
        introductionField.text = "Welcome to Space 4 8. \n Move your ship with the arrows or WASD. \n Shoot with SPACE. \n Gather pickups and cycle with 'Left CTR'.  \n  Use pickups with 'E'.";
        yield return new WaitForSeconds(5f);
        introductionField.enabled = false;
    }
}
