using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonUpdatePage : MonoBehaviour
{
    public static int recipePage = 0;
    private Book book;

    public delegate void ClickAction();
    public static event ClickAction selectRecipe;


    public void  getCurrentPage()
    {
        //Debug.Log("aaaaaa");
        recipePage = GameObject.Find("Book").GetComponent<Book>().getCurrentPage();
        //Debug.Log(recipePage);
        selectRecipe();

    }


    public void fooBar()
    {
        Debug.Log("aa");

    }


}
