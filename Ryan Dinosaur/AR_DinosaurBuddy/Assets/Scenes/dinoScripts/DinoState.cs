using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoState : MonoBehaviour
{
    void Idle()
    {
        GetComponent<Animator>().SetTrigger("Idle");
    }
}
