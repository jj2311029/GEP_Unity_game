using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Button_Script : MonoBehaviour
{
    public bool bCheck_Box = false;

    public InputField User_name;
    public Text Code_name;
    

    /*public void Input_Field_Test()
    {
        Debug.Log("�Է��� �߽��ϴ�");

        if (Name_Input.text == "code")
        {
            Debug.Log("�����ϴ� ID �Դϴ�");

        }
        else
        {

            Debug.Log("�����ϴ� ID �Դϴ�");

        }

    }*/

    /*public void Check_Box_Test()
    {

        if (bCheck_Box == false)
        {
            Debug.Log("Check Box Cliked 1" + bCheck_Box);
            bCheck_Box = true;

        }
        else
        {
            Debug.Log("Check Box Cliked 2" + bCheck_Box);
            bCheck_Box = false;

        }

    }*/

    public void Lobby_to_ingame()
    {
        SceneManager.LoadScene("Game Scene");
        Debug.Log("change");
    }
    
    public void Exit_game()
    {
        Application.Quit();
    }
    public void UserName()
    {
        Debug.Log(User_name.text);
    }
}
