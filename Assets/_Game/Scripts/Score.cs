using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private bool move;

    private Vector2 randomVector;

    void Update()
    {
        if (!move) return;
        transform.Translate(randomVector * Time.deltaTime);        
    }

    public void StartMotion(int Scoreincrease)
    {
        transform.localPosition = Vector2.zero;
        GetComponent<Text>().text = "+" + Scoreincrease;
        randomVector = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
        move = true;
        GetComponent<Animation>().Play();
    }

    public void StopMotion()
    {
        move = false; 
    }
}
