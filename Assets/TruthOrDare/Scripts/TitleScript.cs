using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

//------------------------------------------------------------------------------
//Title screen that will load up the next scene when the use interacts with it
//------------------------------------------------------------------------------
public class TitleScript : MonoBehaviour 
{
	void Update () 
	{
        //If the player taps on the screen or clicks the mouse on the screen then load the next scene
        if((Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonDown(0)))
		{
            SceneManager.LoadScene("MenuScene");
		}

		if(Application.platform == RuntimePlatform.Android)
		{
			if(Input.GetKey(KeyCode.Escape))//If the player presses the back button on Android then quit the app
			{
				Application.Quit();
			}
		}
	}
}
