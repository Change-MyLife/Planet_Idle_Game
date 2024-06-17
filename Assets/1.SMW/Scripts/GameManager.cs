using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float _score;
    public Text AAAA;

    private void Awake()
    {
        Instance = this;
    }

    public void AddScore()
    {
        _score++;
        AAAA.text = _score.ToString();
    }
}
