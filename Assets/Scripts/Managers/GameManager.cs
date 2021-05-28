using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player = default;
    [SerializeField] private TextMeshProUGUI scoreLabel = default;
    [SerializeField] private TextMeshProUGUI hpLabel = default;
    [SerializeField] private HitPoints hitPoints = default;

    private void Start()
    {
        player.onPickUp += PlayerPickedUp;
        player.onHPChange += PlayerHPChanged;

        hpLabel.text = hitPoints.value.ToString();
    }

    private void PlayerPickedUp(int value)
    {
        scoreLabel.text = value.ToString();
    }

    private void PlayerHPChanged(int value)
    {
        hpLabel.text = value.ToString();
    }

    private void OnDestroy()
    {
        player.onPickUp -= PlayerPickedUp;
    }
}
