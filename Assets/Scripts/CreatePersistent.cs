using UnityEngine;
using System.IO;
public class CreatePersistent : MonoBehaviour
{
    string path = "/Sounds";
    private void Awake()
    {
        //Check if Sounds folder exists
        if (!Directory.Exists(Application.persistentDataPath + path))
        {
            Directory.CreateDirectory(Application.persistentDataPath + path);
        }
        // Check if the Audio folder exists
        if (!Directory.Exists(Application.persistentDataPath + path + "/Audio"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + path + "/Audio");
        }
        // Check if the Action folder exists
        if (!Directory.Exists(Application.persistentDataPath + path + "/Action"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + path + "/Action");
        }
        // Check if the Creatures folder exists
        if (!Directory.Exists(Application.persistentDataPath + path + "/Creatures"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + path + "/Creatures");
        }

    }

    public void OpenPersistent()
    {
        if (Directory.Exists(Application.persistentDataPath + path))
        {
            Application.OpenURL(Application.persistentDataPath + path);
        }
    }
}
