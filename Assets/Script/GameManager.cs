using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float MoveTimeInterval = 1.0f;
    public GameObject PlayerPrefab = null;
    public GameObject BlockPrefab = null;

    private int playerNum = 1;
    private float lastMoveTime = 0.0f;
    private List<Player> players = new List<Player>();
    private CreatorBlocks creatorBlocksComp;
    // Start is called before the first frame update
    void Start()
    {
        GameObject blockGenerator = new GameObject("BlockGenerator");
        creatorBlocksComp = blockGenerator.AddComponent<CreatorBlocks>();
        creatorBlocksComp.transform.position = new Vector3(0, 0, 0);
        creatorBlocksComp.BlockPrefab = BlockPrefab;
        creatorBlocksComp.CreateBlocks();
        string assetPathName = "Config/MapSizeData";
        MapSize mapSizeData = Resources.Load<MapSize>(assetPathName);
        creatorBlocksComp.SetColumnRowLayer(mapSizeData.Column, mapSizeData.Row, mapSizeData.Layer);
        lastMoveTime = 0.0f;
        if(PlayerPrefab != null) {
            for(int i = 0; i < playerNum; ++i)
            {
                GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
                mainCamera.transform.position = new Vector3(-1, mapSizeData.Layer + 2, -1);
                GameObject currentGO = Instantiate(PlayerPrefab, new Vector3(0, mapSizeData.Layer + 1, 0), Quaternion.identity, this.transform);
                Player playerCom = currentGO.AddComponent<Player>();
                playerCom.SetColumnRowLayer(mapSizeData.Column, mapSizeData.Row, mapSizeData.Layer);
                playerCom.SetPosition(0, mapSizeData.Layer + 1, 0);
                players.Add(playerCom);
                mainCamera.transform.LookAt(currentGO.transform);
                mainCamera.GetComponent<Camera>().orthographic = true;
                //currentGO.transform.SetParent(mainCamera.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        lastMoveTime += Time.deltaTime;
        if(lastMoveTime >= MoveTimeInterval)
        {
            Player currentPlayer;
            for(int i = 0; i < players.Count; ++i)
            {
                currentPlayer = players[i];
                currentPlayer.Move();
            }
            lastMoveTime -= MoveTimeInterval;
        }
    }
}
