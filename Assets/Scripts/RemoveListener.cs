using UnityEngine;
using UnityEngine.UI;
public class RemoveListener : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
