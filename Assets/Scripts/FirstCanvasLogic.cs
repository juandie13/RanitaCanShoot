using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
public class FirstCanvasLogic : MonoBehaviour
{
    public int enemyCount;
    public TextMeshProUGUI text;
    public string count;
    // Start is called before the first frame update
    void Start()
    {
        enemyCount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //enemyCount = int.Parse(text.text);
        //Debug.Log(text.text);
    }
    public void Empezar()
    {
        //int.TryParse(text.text, out enemyCount);
        //enemyCount = Convert.ToInt32(text.text.ToString());
        //enemyCount = int.Parse(text.text.ToString());

        count = text.text;
        count = count.Substring(0, count.Length - 1);
        bool success = int.TryParse(count, out enemyCount);
        if (success)
        {
            Debug.Log(enemyCount); // Output: 123
        }
        else
        {
            Debug.Log("Invalid input");
        }
        Debug.Log(enemyCount);
        GameManager.Instance.ordaCount = enemyCount;
        SceneManager.LoadScene("MainScene");
    }
}
