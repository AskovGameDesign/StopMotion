using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public static GameManager Instance { get; set; }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    // Use this for initialization
    void Awake()
    {

        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    public void Greetings(GameObject from)
    {
        Debug.Log("The Game Manager said hello to " + from.name);
    }
}