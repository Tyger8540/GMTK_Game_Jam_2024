using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public InventoryManager inventory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Restart();
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        inventory.container.Clear();
        SceneManager.LoadScene("SampleScene");
    }
}
