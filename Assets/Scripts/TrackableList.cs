using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;


/// <summary>
/// Ryan Hall & Arif Khan| 21:40 - 00:41 |  18/04/2018 - 19/04/2018 
/// </summary>
public class TrackableList : MonoBehaviour
{
    List<string> cardTypeLet = new List<string>();
    List<int> cardTypeNum = new List<int>();

    string cardName;
    string cardTypeTemp;    //store type in string before parsing
    int cardTempNum;        //after parse move into temporary integer
    bool isNum;             //boolean for try parse
    int indexLet;
    int indexNum;

    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        // Get the Vuforia StateManager
        StateManager sm = TrackerManager.Instance.GetStateManager();

        // Query the StateManager to retrieve the list of
        // currently 'active' trackables 
        //(i.e. the ones currently being tracked by Vuforia)
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();

        // Iterate through the list of active trackables
        foreach (TrackableBehaviour card in activeTrackables)
        {
            //Debug.Log("Tracking: " + card.TrackableName);

            if (cardName != card.TrackableName.ToString())  //if stored card name is not equal to the tracked name being read
            {
                cardName = card.TrackableName.ToString();                   //turns trackable name into string and stores it in card name
                cardTypeTemp = cardName[cardName.Length - 1].ToString();    //gets last char off of cardname string and converts char into string to store in to cardtypetemp

                isNum = int.TryParse(cardTypeTemp, out cardTempNum);    //try parse cardtemp, if false add to letter list, if true add to number list
                if (isNum == true)
                {
                    Debug.Log("Card is a number" + cardTypeNum.Count);
                    cardTypeNum.Add(cardTempNum);
                }
                else
                {
                    Debug.Log("Card is a letter" + cardTypeLet.Count);
                    cardTypeLet.Add(cardTypeTemp);
                }


                indexNum = cardTypeNum.Count;
                indexLet = cardTypeLet.Count;
                //if statements for length of lists to be more than 1 & if top 2 cards are the same type
                if (cardTypeNum.Count >= 2 && cardTypeNum[--indexNum] == cardTypeNum[indexNum])
                {
                    Debug.Log("Snap numbers" + cardTypeNum[--indexNum] + cardTypeNum[indexNum]);    //output to debug
                }

                if (cardTypeLet.Count >= 2 && cardTypeLet[--indexLet] == cardTypeLet[indexLet])
                {
                    Debug.Log("Snap letters" + cardTypeLet[--indexLet] + cardTypeLet[indexLet]);   //output to debug
                }
            }
        }
    }
}