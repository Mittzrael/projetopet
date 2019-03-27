using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Waste : MonoBehaviour
{
    public void OnMouseDown()
    {
        ///Mudar valores da higiene
        ChangeValues();
        ///Provavelmente invocará uma animação
        PlayAnimation();
        Destroy(transform.parent.gameObject);
        Save();
    }

    public virtual void ChangeValues()
    {

    }

    public virtual void PlayAnimation()
    {

    }

    public virtual void Save()
    {

    }
}
