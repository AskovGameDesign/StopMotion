using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour
{
    [Min(0), Tooltip("This is a tooltip, ideal insted of comments")]
    public int minValue;

    [Range(0, 200)]
    public int rangeValue;

    [Multiline(5)]
    public string desprition;

    

    [Header("Physics Settings")]
    [Range(2f, 24f)]
    [Space(20)]
    public float force;

    
        




    [SerializeField] private float privateFloat = 2f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
