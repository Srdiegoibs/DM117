using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [SerializeField] 
    private LevelControle _levelControle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _levelControle.CaerrgaLevel("TelaVitoria");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
