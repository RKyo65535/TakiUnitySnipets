using Assets.TakiExtension.Singleton;
 using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioLoop : MonoBehaviourSingleton<AudioLoop>
{
    static AudioSource thisObj;
    public static AudioSource I => thisObj;

    AudioSource audioSource;
    [SerializeField] int loopStartPoint;
    [SerializeField] int loopEndPoint;

    protected override void LateAwake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (audioSource.timeSamples > loopEndPoint)
        {
            audioSource.timeSamples -= (loopEndPoint - loopStartPoint);
        }
    }
}