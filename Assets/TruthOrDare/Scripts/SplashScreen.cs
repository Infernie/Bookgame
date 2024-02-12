using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

//----------------------------------------------------------------------------
//Simple splashscreen script that displays a loading text until ready
//to advance to the next scene
//----------------------------------------------------------------------------
public class SplashScreen : MonoBehaviour 
{
	public Text loadingText;//Text box used to display the loading text
	private float timer;//Timer used to count how much time has passed
    private const float SPLASH_SCREEN_DURATION = 5.0f;//Time to wait until advancing to the next scene
	private bool inProgress;//Used to prevent the AnimateText coroutine from running multiple times at once
    private const string LOADING_TEXT_STRING = "Loading...";//Text to write in the loadingText textbox

	void Start () 
	{
        //Start the coroutine to animate the loadingText text color
        StartCoroutine (AnimateTextColor(LOADING_TEXT_STRING, Color.red, Color.blue));
	}
	
	void Update () 
	{
		timer += Time.deltaTime;//Increment the timer
		if(!inProgress)//if the coroutine is not currently running then run it
            StartCoroutine (AnimateTextColor(LOADING_TEXT_STRING, Color.red, Color.blue));
        if(timer >= SPLASH_SCREEN_DURATION)//if the timer value is greater than the SPLASH_SCREEN_DURATION, move to the next scene
		{
            SceneManager.LoadScene("TitleScene");
		}
	}

    //changes the color of the text to endingColor after half of stirngToWrite is written
    IEnumerator AnimateTextColor(string stringToWrite, Color startingColor, Color endingColor)
	{ 
		inProgress = true;
		int i = 0; 
        string str = string.Empty; 
        while( i < stringToWrite.Length )//while the entire string hasn't been written
		{ 
            if(i<stringToWrite.Length*0.5)//if writting the first half of stringToWrite then use the startingColor
			{
                loadingText.color = startingColor;
			}
			else//else use the endingColor
			{
                loadingText.color = endingColor;
			}
            str += stringToWrite[i++];//Add the character to str
			loadingText.text = str;//Set the text of loadingText to = str
            yield return new WaitForSeconds(0.2F);//wait 0.2 seconds to add the next character;
		}
		inProgress = false;//Set inProgress to false, allowing the coroutine to be called again
	}
}
