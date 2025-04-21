using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : CommonButtonLogic
{

    public List<AudioClip> ShowMenusListsfx;
    private void Update()
    {
        if (Input.GetButton("PauseButton"))
        {
            int r = Random.Range(0, ShowMenusListsfx.Count);
            //AudioManager.instance.PlaySFXOnce(ShowMenusListsfx[r]);
            OnBackPressed();
        }
    }

    public void OnBackPressed()
    {
        sfxCount = Random.Range(0, sfxList.Count - 1);
        //AudioManager.instance.PlaySFXOnce(sfxList[sfxCount]);
        MenuManager.instance.changeToCustom(MenuManager.instance.lastMenuEnumOpened);
    }

    public void OnAlbumPressed()
    { 
        sfxCount = Random.Range(0, sfxList.Count - 1);
        //AudioManager.instance.PlaySFXOnce(sfxList[sfxCount]);
        MenuManager.instance.ChangeToAlbum();
    }
}
