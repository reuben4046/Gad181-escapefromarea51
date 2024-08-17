using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameModeInfo", menuName = "ScriptableObjects/GameModeInfo", order = 1)]
public class GameModeInfo : ScriptableObject
{
    //this controls the mode of the game, story or endless. 
    public bool isStoryMode;
}
