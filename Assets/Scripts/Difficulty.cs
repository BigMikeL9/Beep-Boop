using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class/script controls the difficulty of the game.
public class Difficulty : MonoBehaviour
{
    private Button button;
    private GameManager gameManagerScript;

    public int difficulty;


    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDifficulty()
    {
        Debug.Log(button.gameObject.name + " mode has been chosen");
        gameManagerScript.StartGame(difficulty);
    }
}
