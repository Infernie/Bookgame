using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public enum TOrD
{
	Truth,
	Dare
};

public class GameScript : MonoBehaviour 
{
	public GameObject choosePanel;//Initial ChoosePanel that gives the player the option to choose truth or dare
	public Text tOrDTextBox;//The textbox that displays the truth or dare
	public GameObject gamePanel;//The GamePanel that displays the player's truth or dare and provides the options to choose another truth or dare
	private List<string> allTruths, allDares;//Lists containing all the truths and dares
	private List<string> availableTruths;//List of the current truths that have not been displayed yet
	private List<string> availableDares;//List of the current dares that have not been displayed yet
	private TextAsset truthDoc;//The text asset that contains all the truths in text form
	private TextAsset dareDoc;//The text asset that contains all the dares in text form
    private const string CUSTOM_TRUTHS_COUNTER_KEY = "CustomTruthsCounter";//The PlayerPrefs key that stores the number of custom truths
    private const string CUSTOM_DARES_COUNTER_KEY = "CustomDaresCounter";//The PlayerPrefs key that stores the number of custom dares

	void Awake()
	{
		truthDoc = Resources.Load ("TruthList") as TextAsset;//Load the Truths text asset from resources
        availableTruths = new List<string> ();
		allTruths = new List<string> ();
        //if the player has not selected to use onlyCustoms then add the truths from the truths text asset to the possible truth selections
		if(PlayerPrefs.GetInt("OnlyCustoms") == 0)
		{
			StringReader reader = new StringReader (truthDoc.text);
			while(reader.Peek() >=0)
			{
				allTruths.Add(reader.ReadLine());
			}
		}
        //Add each custom truth that the player as created
        for(int i = 0; i< PlayerPrefs.GetInt(CUSTOM_TRUTHS_COUNTER_KEY); i++)
		{
			allTruths.Add (PlayerPrefs.GetString("CustomTruth" + i));
		}
        dareDoc = Resources.Load ("DareList") as TextAsset;//Load the Dares text asset from resources
        availableDares = new List<string> ();
		allDares = new List<string> ();
        //if the player has not selected to use onlyCustoms then add the dares from the dares text asset to the possible dare selections
        if(PlayerPrefs.GetInt("OnlyCustoms") == 0)
		{
			StringReader reader = new StringReader (dareDoc.text);
			while(reader.Peek() >=0)
			{
				allDares.Add(reader.ReadLine());
			}
		}
        //Add each custom dare that the player has created
        for(int i = 0; i< PlayerPrefs.GetInt(CUSTOM_DARES_COUNTER_KEY); i++)
		{
			allDares.Add (PlayerPrefs.GetString("CustomDare" + i));
		}

        //Print out all truths and dares (can be used for debugging)
//        for (int i = 0; i < allTruths.Count; i++)
//		{
//            print (allTruths[i]);
//		}
//
//        for (int i = 0; i < allDares.Count; i++)
//		{
//            print ("Dare " +allDares[i]);
//		}

	}

	void Start () 
	{
        //Add all the truths and dares to the availableTruths and availableDares list
        availableTruths.AddRange(allTruths);
        availableDares.AddRange (allDares);
	}

	void Update () 
	{
        //Return to the menu if the back button is pressed on android
		if(Application.platform == RuntimePlatform.Android)
		{
			if(Input.GetKey(KeyCode.Escape))
			{
                SceneManager.LoadScene("MenuScene");
			}
		}
	}

    //Shows a truth to the player
	public void ChoseTruth()
	{
        if(!gamePanel.activeSelf)
		    ActivateGamePanel ();
		SetTruth ();
	}

    //Shows a dare to the player
	public void ChoseDare()
	{
        if(!gamePanel.activeSelf)
		    ActivateGamePanel ();
		SetDare ();
	}

	private void SetTruth()
	{
        int num = Random.Range (0, availableTruths.Count);
        tOrDTextBox.text = availableTruths[num];
        availableTruths.RemoveAt (num);
        if(availableTruths.Count <= 0)
		{
            availableTruths.AddRange(allTruths);
		}
	}

	private void SetDare()
	{
		int num = Random.Range (0, availableDares.Count);
        tOrDTextBox.text = availableDares[num];
        availableDares.RemoveAt (num);
        if(availableDares.Count <= 0)
		{
            availableDares.AddRange(allDares);
		}
	}

	void ActivateGamePanel()
	{
		choosePanel.SetActive(false);
		gamePanel.SetActive(true);
	}
}
