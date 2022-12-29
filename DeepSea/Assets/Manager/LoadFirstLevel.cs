using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFirstLevel : MonoBehaviour
{
    void Start()
    {
        // TODO: Add Main Menu, Tutorial, or similar
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
