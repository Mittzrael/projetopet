using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine;

public class VideoManager : MonoBehaviour {

    public static VideoManager instance = null;
    public VideoPlayer videoPlayer;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(this != instance)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator PlayVideo (RawImage image)
    {
        image.texture = videoPlayer.texture;
        videoPlayer.Prepare();
        yield return new WaitUntil (()=> videoPlayer.isPrepared);
        videoPlayer.Play();
    }

    public IEnumerator PlayVideo(RawImage image, VideoClip video)
    {
        videoPlayer.clip = video;
        videoPlayer.Prepare();
        yield return new WaitUntil(() => videoPlayer.isPrepared);
        image.texture = videoPlayer.texture;
        videoPlayer.Play();
    }

    public IEnumerator PlayVideo(VideoClip video)
    {
        videoPlayer.clip = video;
        videoPlayer.Prepare();
        yield return new WaitUntil(() => videoPlayer.isPrepared);
        GameObject.Find("Tela").GetComponent<RawImage>().texture = videoPlayer.texture;
        videoPlayer.Play();
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
    }

    public void ReplayVideo()
    {
        videoPlayer.Stop();
        videoPlayer.Play();
    }



}
