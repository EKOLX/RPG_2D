using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreValue = default;
    [SerializeField] private Button buttonStartOver = default;

    private void Start()
    {
        buttonStartOver.onClick.AddListener(() => SceneManager.LoadScene(0));
        scoreValue.text = PlayerPrefs.GetInt(K.score).ToString();
    }

}
