using System;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class MoveAudio : ScriptableObject
{
    public AudioClip ForwardAudio;
    public AudioClip BackwardAudio;
    public AudioClip LeftAudio;
    public AudioClip RightAudio;
}
