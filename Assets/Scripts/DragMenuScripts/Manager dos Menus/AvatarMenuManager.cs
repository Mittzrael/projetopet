using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarMenuManager : MenuManager
{
    public AvatarList avatarList;

    private void Awake()
    {
        listSize = avatarList.avatar.Length;
    }

    public override void DisplayItemInfo(int i)
    {
        itemInstance.GetComponent<RawImage>().texture = avatarList.avatar[i].itemImage;
    }
}
