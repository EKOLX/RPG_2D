using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player = default;
    [SerializeField] private TextMeshProUGUI scoreLabel = default;
    [SerializeField] private HitPoints hitPoints = default;

    private void Start()
    {
        player.onPickUp += PlayerPickedUp;
    }

    private void PlayerPickedUp(int value)
    {
        scoreLabel.text = value.ToString();
    }

    private void OnDestroy()
    {
        player.onPickUp -= PlayerPickedUp;
    }
}
