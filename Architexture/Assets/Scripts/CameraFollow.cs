using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public bool finishedExpandingCam = false;
    public float leftBound;
    public float rightBound;

    public SoundEffect bridge;
    public SoundEffect stairs;
    public SoundEffect ladder;
    public SoundEffect launchpad;
    public SoundEffect boing;
    public SoundEffect steps;
    public SoundEffect hotToGo;
    public SoundEffect checkpoint;
    public SoundEffect invalid;
    public SoundEffect zoomOut;
    public AudioSource speaker;


    [System.Serializable]
    public struct SoundEffect
    {
        public AudioClip audioClip;
        [Range(0.0f, 5.0f)]
        public float volume;
        public bool loop;
        public bool oneShot;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y >= 77f || player.transform.position.x <= -23.5f || (player.transform.position.x >= leftBound && player.transform.position.x <= rightBound))
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 3f, -10f);
        }
        else if (player.transform.position.x < leftBound)
        {
            transform.position = new Vector3(leftBound, player.transform.position.y + 3f, -10f);
        }
        else if (player.transform.position.x > rightBound)
        {
            transform.position = new Vector3(rightBound, player.transform.position.y + 3f, -10f);
        }
    }

    public void PlaySound(SoundEffect soundEffect)
    {
        speaker.clip = soundEffect.audioClip;
        speaker.loop = soundEffect.loop;
        speaker.volume = soundEffect.volume;
        if (soundEffect.oneShot)
        {
            speaker.PlayOneShot(speaker.clip, speaker.volume);
        }
        else
        {
            speaker.Play();
        }
    }

    public void Expand()
    {
        StartCoroutine(ExpandCamera());
    }

    IEnumerator ExpandCamera()
    {
        PlaySound(zoomOut);
        for (int i = 0; i < 50; i++)
        {
            GetComponent<Camera>().orthographicSize += .1f;
            yield return new WaitForSeconds(.1f);
        }

        finishedExpandingCam = true;
        yield return null;
    }
}
