using System;
using UnityEngine;

namespace Taki.SoundManager
{
    [Serializable]
    public class AudioInformation
    {
        public AudioClip audio;
        public int loopStartPoint;
        public int loopEndPoint;
    }
}