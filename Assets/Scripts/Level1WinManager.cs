using UnityEngine;
using System.Collections;

public class Level1WinManager : LevelWinManager
{
    public override bool WinCondition()
    {
        return BirdArrives && GetKey && DogArrives && FishArrives;
    }
}
