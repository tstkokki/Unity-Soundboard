using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class QuitGame : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(CloseGame);
    }

    void CloseGame()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
