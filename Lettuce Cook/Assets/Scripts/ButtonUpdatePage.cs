using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonUpdatePage : MonoBehaviour
{
    int recipePage;
    private Book book;


    public int  getCurrentPage()
    {
        recipePage = GameObject.Find("Book").GetComponent<Book>().getCurrentPage();
        return (recipePage);
    }

    

}
