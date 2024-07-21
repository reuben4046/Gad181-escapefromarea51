using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SavedInformationRP", menuName = "ScriptableObjects/SavedInformationRP", order = 1)]
public class SavedInformationRP : ScriptableObject
{
    //this script saves the player's time survived in the game
    public string timeSurvived;
}
