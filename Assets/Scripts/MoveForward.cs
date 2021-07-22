using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class/script controls the enemies' movements.
public class MoveForward : MonoBehaviour
{
    public float speed;

    private GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.isGameActive == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
       
    }
}
