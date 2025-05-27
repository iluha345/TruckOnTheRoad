using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameObject _startCanvas;
    [SerializeField] private GameObject _startCamera;
    [SerializeField] private GameObject _gameCamera;
    public void StartGame()
    {
        _startCanvas.SetActive(false);
        _gameCamera.SetActive(true);
        _startCamera.SetActive(false);
        GameManager.Instance.GameState = GameManager.GameStateEnum.Play;
    }
}