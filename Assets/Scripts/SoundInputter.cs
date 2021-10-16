using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SoundInputter : MonoBehaviour
{
    public List<SoundLibrary> libraries = new List<SoundLibrary>();


    public void InputActionPressed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {

            int i;
            try
            {
                i = int.Parse(ctx.action.name);
                PlayInput(i);
            }
            catch (FormatException e)
            {
                Debug.Log(e);
            }
        }
    }

    public void PlayInput(int i)
    {

        foreach (SoundLibrary _library in libraries)
        {
            if (_library.gameObject.activeInHierarchy)
            {
                _library.PlayInput(i);
            }
        }
    }
}
