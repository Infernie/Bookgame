using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

//-----------------------------------------------------------------
//The main menu script, constains all the logic for the MenuScene
//-----------------------------------------------------------------
public class MenuScript : MonoBehaviour 
{
	public GameObject choicePanel, infoPanel;//Panels that display choices and information
	public Text infoText;//The textbox that is on the infoPanel
    //Reference to the button that allows the player to play with only their custom
    //truthes and dares
	public Button onlyCustomsButton;
    private const string CUSTOM_TRUTHS_COUNTER_KEY = "CustomTruthsCounter";//The PlayerPrefs key that stores the number of custom truths
    private const string CUSTOM_DARES_COUNTER_KEY = "CustomDaresCounter";//The PlayerPrefs key that stores the number of custom dares

	void Update () 
	{
		if(Application.platform == RuntimePlatform.Android)
		{
			if(Input.GetKey(KeyCode.Escape))//If pressing the back button on android then return to the previous panel
			{
				GoBack();
			}
		}
        //if the player doesn't have any custom truths made or custom dares made then disable the customs only button
        if(PlayerPrefs.GetInt(CUSTOM_TRUTHS_COUNTER_KEY) <= 0 || PlayerPrefs.GetInt(CUSTOM_DARES_COUNTER_KEY) <= 0)
		{
			onlyCustomsButton.interactable = false;
		}
		else
		{
			onlyCustomsButton.interactable = true;
		}
	}

	public void GoBack ()
	{
        //if showing the choice panel then set it to false (allowing the main menu to show
        if(choicePanel.activeSelf)
		{
            choicePanel.SetActive(false);
			infoPanel.SetActive(false);
		}
		else//else quit the application
		{
			Application.Quit();
		}
	}

	public void PlayGame()
	{
        //Show the choice panel 
        choicePanel.SetActive (true);
	}

	public void PlayWithAllTruthsAndDares()
	{
        //Play truth or dare using both custom and provided truths and dares
		PlayerPrefs.SetInt ("OnlyCustoms", 0);
        SceneManager.LoadScene("GameScene");
	}

	public void PlayWithOnlyCustomTruthsAndDares()
	{
        //Play truth or dare using only custom truths and dares provided by the player
		PlayerPrefs.SetInt ("OnlyCustoms", 1);
        SceneManager.LoadScene("GameScene");
	}

    //Sets the infoPanel text to the appropriate string and shows the infoPanel
	public void InfoOnlyCustom()
	{
        //if the player presses the only customs info button (the button with a ? in it beside the only customs button) and does not have enough 
        //custom truths or custom dares then display a message to them letting them know that they need to add more
        if(PlayerPrefs.GetInt(CUSTOM_TRUTHS_COUNTER_KEY) <= 0 || PlayerPrefs.GetInt(CUSTOM_DARES_COUNTER_KEY) <= 0)
		{
			infoText.text = "You must add more custom truths and dares, you must have at least one truth and one dare to play this mode!";
		}
		else
		{
			infoText.text = "Play a game with only your Custom truths and dares!";
		}
		infoPanel.SetActive (true);
	}

    //Sets the infoPanel text and shows the infoPanel
	public void InfoAll()
	{
		infoText.text = "Play a game with all truths and dares, including your custom ones!";
		infoPanel.SetActive (true);
	}

    //Turns off the infoPanel
	public void Ok()
	{
		infoPanel.SetActive (false);
	}

    //Loads the CustomizeScene
	public void Customize()
	{
        SceneManager.LoadScene("CustomizeScene");
	}
}
