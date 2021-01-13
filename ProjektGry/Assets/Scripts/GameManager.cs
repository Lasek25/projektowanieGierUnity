using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject win;

    public void EndGame()
    {
        FindObjectOfType<AudioManager>().Stop();
        gameOver.SetActive(true);
    }

    public void WinGame()
    {
        FindObjectOfType<AudioManager>().Stop();
        win.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().gameObject.SetActive(false);
    }
}
