

using UnityEngine;
using System.Collections;


public class DisableTriggerOnPlayerExit : MonoBehaviour
{

    public void OnTriggerExit (Collider other)
    {
        if (other.gameObject.CompareTag ("Player"))
        { //    Cuando el jugador sale del área de activación
            GetComponent<Collider> ().isTrigger = false; // Desactiva trigger
        }
    }
}
