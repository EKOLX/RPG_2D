using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartOverManager : MonoBehaviour
{
    [SerializeField] private Button buttonStartOver = default;

    private void Start()
    {
        buttonStartOver.onClick.AddListener(() => SceneManager.LoadScene(1));
    }

}
