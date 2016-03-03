using UnityEngine;
using System.Collections;

public class LoadSaveButton : MonoBehaviour {

    public GameSave save;

    public void LoadSave()
    {
        GameSave.current = save;
        if(GameSave.current != null)
        {
            Application.LoadLevel(1);
        }
    }
}
