using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public GameObject PlayerPrefab = null;
    public AudioClip ForwardAudioClip;
    public AudioClip BackwardAudioClip;
    public AudioClip LeftAudioClip;
    public AudioClip RightAudioClip;
    public AudioSource audioShource = null;

    private int column = 0;
    private int row = 0;
    private int layer = 0;
    // ÒÆ¶¯·½Ïò
    private enum MoveDirection
    {
        None,
        Forward,
        BackWard,
        Left,
        Right
    }
    private MoveDirection direction = MoveDirection.None;
    private int x = 0;
    private int y = 0;
    private int z = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioShource = this.gameObject.AddComponent<AudioSource>();
        string assetPathName = "Config/MoveAudioData";
        MoveAudio moveAudioAsset = Resources.Load<MoveAudio>(assetPathName);
        if(moveAudioAsset != null)
        {
            ForwardAudioClip = moveAudioAsset.ForwardAudio;
            BackwardAudioClip = moveAudioAsset.BackwardAudio;
            LeftAudioClip = moveAudioAsset.LeftAudio;
            RightAudioClip = moveAudioAsset.RightAudio;
            Debug.LogWarning("Audio Load Success");
        }
}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SetDirection(MoveDirection.Forward);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SetDirection(MoveDirection.BackWard);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            SetDirection(MoveDirection.Left);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            SetDirection(MoveDirection.Right);
        }
    }

    void SetDirection(MoveDirection dir)
    {
        switch (dir)
        {
            case MoveDirection.Forward:
                if(ForwardAudioClip != null)
                {
                    audioShource.clip = ForwardAudioClip;
                    audioShource.Play();
                }
                break;
            case MoveDirection.BackWard:
                if (BackwardAudioClip != null)
                {
                    audioShource.clip = BackwardAudioClip;
                    audioShource.Play();
                }
                break;
            case MoveDirection.Left:
                if (LeftAudioClip != null)
                {
                    audioShource.clip = LeftAudioClip;
                    audioShource.Play();
                }
                break;
            case MoveDirection.Right:
                if (RightAudioClip != null)
                {
                    audioShource.clip = RightAudioClip;
                    audioShource.Play();
                }
                break;
        }
        direction = dir;
    }

    public void Move()
    {
        switch (direction)
        {
            case MoveDirection.Forward:
                x = Math.Min(x + 1, column - 1);
                break;
            case MoveDirection.BackWard:
                x = Math.Max(x - 1, 0);
                break;
            case MoveDirection.Left:
                z = Math.Min(z + 1, row - 1);
                break;
            case MoveDirection.Right:
                z = Math.Max(z - 1, 0);
                break;
        }
        if (direction != MoveDirection.None) this.gameObject.transform.position = new Vector3(x, y, z);
    }

    public void SetPosition(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public void SetColumnRowLayer(int column, int row, int layer)
    {
        this.column = column;
        this.row = row;
        this.layer = layer;
    }
}
