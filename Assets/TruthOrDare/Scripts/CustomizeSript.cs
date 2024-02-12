using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

//----------------------------------------------------------
//Script used for allowing the player to add their 
//own custom truths and dares to the game
//----------------------------------------------------------
public class CustomizeSript : MonoBehaviour 
{
	public InputField addField;//The text field used for typing in a truth or dare
	public GameObject addPanel, areYouSurePanel;
	public Button deleteTruth, deleteDare;//The buttons for deleting a custom truth or dare
	public Text areYouSurePanelText, infoText;
	private bool addTruth = false;//If true then a truth will be added, if false a dare will be added
    private bool removeTruth = false;//If true then a truth will be removed, if false then a dare will be removed
    private const string CUSTOM_TRUTHS_COUNTER_KEY = "CustomTruthsCounter";
    private const string CUSTOM_DARES_COUNTER_KEY = "CustomDaresCounter";

	void Update () 
	{
		if(Application.platform == RuntimePlatform.Android)
		{
			if(Input.GetKey(KeyCode.Escape))
			{
				GoBack();
			}
		}

        if(PlayerPrefs.GetInt(CUSTOM_TRUTHS_COUNTER_KEY) > 0)//if the player has at least one custom truth then enable the deleteTruth button
		{
			deleteTruth.interactable = true;
		}
		else
		{
			deleteTruth.interactable = false;
		}

        if(PlayerPrefs.GetInt(CUSTOM_DARES_COUNTER_KEY) > 0)//if the player has at least one custom dare then enable the deleteDare button
		{
			deleteDare.interactable = true;
		}
		else
		{
			deleteDare.interactable = false;
		}
	}

    //Return to the main menu
	public void GoBack()
	{
        SceneManager.LoadScene("MenuScene");
	}

	public void CreateTruth()
	{
        //Change the text to blue, set the text string, set addTruth to true, and show the addPanel
		infoText.color = Color.blue;
		infoText.text = "Please type in a custom truth!\n(Max 150 characters)";
		addTruth = true;
		addPanel.SetActive (true);
	}

	public void CreateDare()
	{
        //Change the text to red, set the text string, set addTruth to false, and show the addPanel
		infoText.color = Color.red;
		infoText.text = "Please type in a custom dare!\n(Max 150 characters)";
		addTruth = false;
		addPanel.SetActive (true);
	}

    //Get the last custom truth that was added
    private string GetCustomTruthThatWasLastAdded()
    {
        return PlayerPrefs.GetString("CustomTruth" + (PlayerPrefs.GetInt(CUSTOM_TRUTHS_COUNTER_KEY) - 1));
    }

    //Get the last custom dare that was added
    private string GetCustomDareThatWasLastAdded()
    {
        return PlayerPrefs.GetString("CustomDare" + (PlayerPrefs.GetInt(CUSTOM_DARES_COUNTER_KEY) - 1));
    }

    //Delete the last Custom truth that was added
	public void DeleteLastTruth()
	{
        //Set the areYouSurePanelText to show a warning and include the truth that will be removed, set removeTruth to true
        areYouSurePanelText.text = "Are your sure you want to remove \"" + GetCustomTruthThatWasLastAdded() + "\"?";
		areYouSurePanel.SetActive (true);
		removeTruth = true;
	}

    //Delete the last custom dare that was added
	public void DeleteLastDare()
	{
        //Set the areYouSurePanelText to show a warning and include the dare that will be removed, set removeTruth to false
        areYouSurePanelText.text = "Are your sure you want to remove \"" + GetCustomDareThatWasLastAdded() + "\"?";
		areYouSurePanel.SetActive (true);
		removeTruth = false;
	}

    //Called when the player confirms the removal of a truth or dare
	public void Yes()
	{
		if(removeTruth)
		{
            //Reduce the custom truths counter and delete the key for the last truth
            PlayerPrefs.SetInt(CUSTOM_TRUTHS_COUNTER_KEY, PlayerPrefs.GetInt(CUSTOM_TRUTHS_COUNTER_KEY) - 1);
            PlayerPrefs.DeleteKey("CustomTruth" + (PlayerPrefs.GetInt (CUSTOM_TRUTHS_COUNTER_KEY)));
		}
		else
		{
            //Reduce the custom dares counter and delete the key for the last dare
            PlayerPrefs.SetInt(CUSTOM_DARES_COUNTER_KEY, PlayerPrefs.GetInt(CUSTOM_DARES_COUNTER_KEY) - 1);
            PlayerPrefs.DeleteKey("CustomDare" + (PlayerPrefs.GetInt (CUSTOM_DARES_COUNTER_KEY)));
		}
		areYouSurePanel.SetActive (false);
	}

    //Called when the player cancels the removal of a truth or dare
	public void No()
	{
		areYouSurePanel.SetActive (false);
	}

    //called when the player confirms adding a truth or drare
	public void OnEditDone()
	{
		if(addTruth)
		{
            //Increase the custom truths counter and add the text to PlayerPrefs
            PlayerPrefs.SetString("CustomTruth" + PlayerPrefs.GetInt(CUSTOM_TRUTHS_COUNTER_KEY), addField.text);
            PlayerPrefs.SetInt(CUSTOM_TRUTHS_COUNTER_KEY, PlayerPrefs.GetInt(CUSTOM_TRUTHS_COUNTER_KEY) + 1);
		}
		else
		{
            //Increase the custom dares counter and add the text to PlayerPrefs
            PlayerPrefs.SetString("CustomDare" + PlayerPrefs.GetInt(CUSTOM_DARES_COUNTER_KEY), addField.text);
            PlayerPrefs.SetInt(CUSTOM_DARES_COUNTER_KEY, PlayerPrefs.GetInt(CUSTOM_DARES_COUNTER_KEY) + 1);
		}
        //Set the text back to blank and hide the addPanel
		addField.text = "";
		addPanel.SetActive (false);
	}

    //If the player cancels adding a custom truth or dare
	public void CancelOnEditDone()
	{
        //Set the text back to blank and hide the addPanel
		addField.text = "";
		addPanel.SetActive (false);
	}
}
