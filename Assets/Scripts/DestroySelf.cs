


using UnityEngine;
using System.Collections;


public class DestroySelf : MonoBehaviour
{
    public float Delay = 3f;
    //Demora en segundos antes de destruir el objeto del juego

    void Start ()
    {
        Destroy (gameObject, Delay);
    }
}
