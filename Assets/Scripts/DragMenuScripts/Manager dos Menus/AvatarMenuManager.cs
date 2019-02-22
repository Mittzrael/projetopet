using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMenuManager : MenuManager
{
    public AvatarList avatarList;

    private void Awake()
    {
        listSize = avatarList.avatar.Length;
    }

    public void DisplayItemInfo()
    {
        
    }
}
