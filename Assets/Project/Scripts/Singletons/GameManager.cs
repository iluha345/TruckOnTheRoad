using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject _GameEnd;
    [SerializeField] private TMP_Text _text;


    public GameStateEnum GameState;
    public enum GameStateEnum
    { 
        Play,
        Paused
    }


    private void Start()
    {
        Instance = this;
        GameState = GameStateEnum.Paused;
    }

    public void EndGame(bool isWin = false)
    {
        GameState = GameStateEnum.Paused;
        if (!isWin)
            _text.text = "You loose";
        else
            _text.text = "You win!";
        _GameEnd.SetActive(true);
    }

}