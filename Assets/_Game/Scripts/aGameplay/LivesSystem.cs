using UnityEngine;
using UnityEngine.UI;

public class LivesSystem : MonoBehaviour
{
    [SerializeField]
    private Text liveText;

    private int livesRemaining = 20;
    private GameObject UIBallOver;

    private void Awake()
    {
        EventsContainer.ShouldLoseLife += OnShouldLoseLife;
    }

    public void OnShouldLoseLife()
    {
        if (livesRemaining == 0)
        { 
            return;
        }

        livesRemaining--;

        if (livesRemaining == 0)
        {
            EventsContainer.InvokeLivesLost();
            UIBallOver.SetActive(true);
        }
    }
}