using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamDemo
{
    public class AudioManager
    {
        private AudioClip[] jumpAudio = new AudioClip[5];
        private AudioClip[] fallAudio = new AudioClip[2];
        private AudioClip[] gameBGM = new AudioClip[3];
        private AudioClip explodeAudio;
        private AudioClip gameoverAudio;
        private AudioSource bgmAudioSource;
        private AudioSource seAudioSource;

        public void Init()
        {
            GameObject bgmObject = new GameObject("BgmObject");
            bgmAudioSource = bgmObject.AddComponent<AudioSource>();
            GameObject seObject = new GameObject("seObject");
            seAudioSource = seObject.AddComponent<AudioSource>();

            for (int i = 0; i < jumpAudio.Length; i++)
            {
                jumpAudio[i] = Resources.Load<AudioClip>("Audio/SE/跳跃" + (i + 1));
            }
            for (int i = 0; i < fallAudio.Length; i++)
            {
                fallAudio[i] = Resources.Load<AudioClip>("Audio/SE/坠落" + (i + 1));
            }
            explodeAudio = Resources.Load<AudioClip>("Audio/SE/爆炸");
            gameoverAudio = Resources.Load<AudioClip>("Audio/SE/坠落狗叫");

            gameBGM[0] = Resources.Load<AudioClip>("Audio/BGM/音乐主界面");
            gameBGM[1] = Resources.Load<AudioClip>("Audio/BGM/音乐-轻松");
            gameBGM[2] = Resources.Load<AudioClip>("Audio/BGM/音乐-崩溃");

            PlayBGM(0);
        }

        public AudioClip GetJumpAudio(int index)
        {
            if (index == -1)
            {
                return jumpAudio[Random.Range(0, jumpAudio.Length - 1)];
            }
            return jumpAudio[index];
        }

        public AudioClip GetFallAudio(int index)
        {
            if (index == -1)
            {
                return fallAudio[Random.Range(0, fallAudio.Length - 1)];
            }
            return fallAudio[index];
        }

        public AudioClip GetExplodeAudio()
        {
            return explodeAudio;
        }

        public AudioClip GetGameOverAudio()
        {
            return gameoverAudio;
        }

        public void PlayBGM(int index)
        {
            AudioClip clip;
            if (index == -1)
            {
                clip = gameBGM[Random.Range(0, gameBGM.Length - 1)];
            }
            clip = gameBGM[index];

            bgmAudioSource.clip = clip;
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
        }

        public void PlayCommonSE(AudioClip clip)
        {
            seAudioSource.PlayOneShot(clip);
        }
    }
}

