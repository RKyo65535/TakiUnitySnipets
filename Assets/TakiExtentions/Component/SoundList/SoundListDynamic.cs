// このファイルは自動生成されたものです
using UnityEngine;
namespace Taki.SoundManager
{
    public partial class SoundList : ScriptableObject
    {
        public AudioInformation GetSoundClip(SoundKind kind)
        {
            switch(kind)
            {
            case SoundKind.SampleSound1:
                return audios[0];
            default:
                return default;
            }
        }
     }
    public enum SoundKind
    {
        SampleSound1,
    }
}
