

using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    
    //Parametros del jugador
    [Range (1, 2)]
    public int playerNumber = 1;
    //Indica si es el jugador 1 o 2
    public float moveSpeed = 5f;
    public bool canDropBombs = true;
    //Habilita par que el jugador pueda tirar bombas
    public bool canMove = true;
    //Habilita que el jugador pueda moverse
    public bool dead = false;
    public GlobalStateManager globalManager;
    private int bombs = 2;
    

    //Prefabs
    public GameObject bombPrefab;

    //Cached components
    private Rigidbody rigidBody;
    private Transform myTransform;
    private Animator animator;

    // Use this for initialization
    void Start ()
    {
      
        rigidBody = GetComponent<Rigidbody> ();
        myTransform = transform;
        animator = myTransform.Find ("PlayerModel").GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateMovement ();
    }

    private void UpdateMovement ()
    {
        animator.SetBool ("Walking", false);

        if (!canMove)
        { //Regresa si el jugador no se move
            return;
        }

        //Dependiendo del número de jugador, use diferentes entradas para mover
        if (playerNumber == 1)
        {
            UpdatePlayer1Movement ();
        } else
        {
            UpdatePlayer2Movement ();
        }
    }

    
    /// Actualiza el movimiento y la rotación del jugador 1 usando las teclas WASD y lanza bombas usando Space

    private void UpdatePlayer1Movement ()
    {
        if (Input.GetKey (KeyCode.W))
        { //Movimiento para arriba
            rigidBody.velocity = new Vector3 (rigidBody.velocity.x, rigidBody.velocity.y, moveSpeed);
            myTransform.rotation = Quaternion.Euler (0, 0, 0);
            animator.SetBool ("Walking", true);
        }

        if (Input.GetKey (KeyCode.A))
        { //Movimiento para la e¿izquierda
            rigidBody.velocity = new Vector3 (-moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler (0, 270, 0);
            animator.SetBool ("Walking", true);
        }

        if (Input.GetKey (KeyCode.S))
        { //Movimiento para abajo
            rigidBody.velocity = new Vector3 (rigidBody.velocity.x, rigidBody.velocity.y, -moveSpeed);
            myTransform.rotation = Quaternion.Euler (0, 180, 0);
            animator.SetBool ("Walking", true);
        }

        if (Input.GetKey (KeyCode.D))
        { //Movimiento para la derecha
            rigidBody.velocity = new Vector3 (moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler (0, 90, 0);
            animator.SetBool ("Walking", true);
        }

        if (canDropBombs && Input.GetKeyDown (KeyCode.Space))
        { //Soltar bomba
            DropBomb ();
        }
    }

   
    ///  Actualiza el movimiento y la rotación del jugador 2 usando las teclas de flecha y lanza bombas usando Enter o Return 
  
    private void UpdatePlayer2Movement ()
    {
        if (Input.GetKey (KeyCode.UpArrow))
        { //Movimiento para arriba
            rigidBody.velocity = new Vector3 (rigidBody.velocity.x, rigidBody.velocity.y, moveSpeed);
            myTransform.rotation = Quaternion.Euler (0, 0, 0);
            animator.SetBool ("Walking", true);
        }

        if (Input.GetKey (KeyCode.LeftArrow))
        { //Movimiento para la izquiera 
            rigidBody.velocity = new Vector3 (-moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler (0, 270, 0);
            animator.SetBool ("Walking", true);
        }

        if (Input.GetKey (KeyCode.DownArrow))
        { //Movimiento para abajo
            rigidBody.velocity = new Vector3 (rigidBody.velocity.x, rigidBody.velocity.y, -moveSpeed);
            myTransform.rotation = Quaternion.Euler (0, 180, 0);
            animator.SetBool ("Walking", true);
        }

        if (Input.GetKey (KeyCode.RightArrow))
        { //Movimiento para la derecha
            rigidBody.velocity = new Vector3 (moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler (0, 90, 0);
            animator.SetBool ("Walking", true);
        }

        if (canDropBombs && (Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.Return)))
        { //Dejar caer una bomba. Para las bombas del Jugador 2, permite tanto el ingreso numérico como la tecla de retorno o los jugadores
            DropBomb ();
        }
    }

    /// Suelta una bomba debajo del jugador
    
    private void DropBomb ()
    {
        if (bombPrefab)
        {//Verificar si la bomba prefabricada está asignada primero
                
            Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(myTransform.position.x),
            bombPrefab.transform.position.y, Mathf.RoundToInt(myTransform.position.z)),
            bombPrefab.transform.rotation);
        }
    }

    public void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("Explosion"))
        {
            Debug.Log ("P" + playerNumber + " Haz explotado con la bomba!");
            dead = true; // 1
            globalManager.PlayerDied(playerNumber); // 2
            Destroy(gameObject); // 3
        }
    }
}
