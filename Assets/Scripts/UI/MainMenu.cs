using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Inventory myBag;
    private void Start()
    {
        myBag.itemList.Clear();
        InventoryManager.RefreshItem();
    }
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene("OpeningManga");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
