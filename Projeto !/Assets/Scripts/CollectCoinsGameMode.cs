using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Assets/Gamemodes/collect")]

public class CollectCoinsGameMode : MonoBehaviour, IGameMode<int>
{
    public int coinsToWin;
    public GameState gameState;

    public void UpdateWinStace(int value)
    {
        
    }

}