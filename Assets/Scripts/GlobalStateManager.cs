

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GlobalStateManager : MonoBehaviour
{
    private int deadPlayers = 0;
    private int deadPlayerNumber = -1; 
    public void PlayerDied (int playerNumber)
    {

    deadPlayers++; // 1

        if (deadPlayers == 1)
        { // 2
            deadPlayerNumber = playerNumber; // 3
            Invoke("CheckPlayersDeath", .3f); // 4
        }
    }
    void CheckPlayersDeath()
    {
        // 1
        if (deadPlayers == 1)
        {
            
            if (deadPlayerNumber == 1)
            {
                Debug.Log("El Jugador 2 es el ganador!");
                SceneManager.LoadScene("Game");
            }
            else
            {
                Debug.Log("El jugador 1 es el ganador!");
                SceneManager.LoadScene("Game");
            }
          
        }
        else
        {
            Debug.Log("El juego a terinado!");
     
        }
    }
}
