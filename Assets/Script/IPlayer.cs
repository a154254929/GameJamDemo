using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class IPlayer : MonoBehaviour
{
    public GameObject PlayerPrefab = null;
    public AudioClip ForwardAudioClip;
    public AudioClip BackwardAudioClip;
    public AudioClip LeftAudioClip;
    public AudioClip RightAudioClip;
    public AudioSource Audio = null;
    public MapManager MapMgr;
    public GameObject BombPrefab = null;

    private int column = 0;
    private int row = 0;
    private int layer = 0;
    // 移动方向
    private MoveDirection direction = MoveDirection.None;
    private int x = 0;
    private int y = 0;
    private int z = 0;
    private float height = 0.1f;
    // Start is called before the first frame update
    public void Start()
    {
        Audio = this.gameObject.AddComponent<AudioSource>();
        string assetPathName = "Config/MoveAudioData";
        MoveAudio moveAudioAsset = Resources.Load<MoveAudio>(assetPathName);
        if (moveAudioAsset != null)
        {
            ForwardAudioClip = moveAudioAsset.ForwardAudio;
            BackwardAudioClip = moveAudioAsset.BackwardAudio;
            LeftAudioClip = moveAudioAsset.LeftAudio;
            RightAudioClip = moveAudioAsset.RightAudio;
            Debug.LogWarning("Audio Load Success");
        }
        assetPathName = "Prefab/BombPrefab";
        BombPrefab = Resources.Load<GameObject>(assetPathName);
    }

    // Update is called once per frame
    public void Update()
    {
    }

    public void CreateBomb()
    {
        GameObject gameObject = Instantiate(BombPrefab, this.gameObject.transform.position, Quaternion.identity);
        Bomb bomb = gameObject.AddComponent<Bomb>();
        bomb.MapMgr = MapMgr;
        bomb.SepPos(x, y, z);
    }

    public void SetDirection(MoveDirection dir)
    {
        switch (dir)
        {
            case MoveDirection.Forward:
                if (ForwardAudioClip != null)
                {
                    Audio.clip = ForwardAudioClip;
                    Audio.Play();
                }
                break;
            case MoveDirection.BackWard:
                if (BackwardAudioClip != null)
                {
                    Audio.clip = BackwardAudioClip;
                    Audio.Play();
                }
                break;
            case MoveDirection.Left:
                if (LeftAudioClip != null)
                {
                    Audio.clip = LeftAudioClip;
                    Audio.Play();
                }
                break;
            case MoveDirection.Right:
                if (RightAudioClip != null)
                {
                    Audio.clip = RightAudioClip;
                    Audio.Play();
                }
                break;
        }
        direction = dir;
    }

    public void Move()
    {
        int nextX = x;
        int nextY = y;
        int nextZ = z;
        switch (direction)
        {
            case MoveDirection.Forward:
                nextX = Math.Min(nextX + 1, column - 1);
                break;
            case MoveDirection.BackWard:
                nextX = Math.Max(nextX - 1, 0);
                break;
            case MoveDirection.Left:
                nextZ = Math.Min(nextZ + 1, row - 1);
                break;
            case MoveDirection.Right:
                nextZ = Math.Max(nextZ - 1, 0);
                break;
        }
        bool canMoveNextStep = !MapMgr.HaveBlock(nextX, nextY, nextZ);
        if(canMoveNextStep)
        {
            while(nextY - 1 >= 0 && !MapMgr.HaveBlock(nextX, nextY - 1, nextZ))
            {
                nextY -= 1;
            }
            x = nextX;
            y = nextY;
            z = nextZ;
        }
        if (direction != MoveDirection.None) this.gameObject.transform.position = new Vector3(x, y + height, z);
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
    
    public void SetHeight(float hieght)
    {
        this.height = height;
    }
}
