using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int healths;
    public int speed;
    public int levelUnlocked;
    public int[] stars = new int[8];

    public int audios;
    public float volume;

    public GameData()
    {
        healths = 1;
        speed = 5;
        levelUnlocked = 1;
        audios = 1;
        volume = 0f;
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i] = 0;
        }
    }
}
