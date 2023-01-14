using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float MoveTimeInterval = 1.0f;
    public GameObject PlayerPrefab = null;
    public GameObject BlockPrefab = null;
    public float GenerateBombTime = 3.0f;
    public float Height = 0.1f;

    private int playerNum = 1;
    private float lastMoveTime = 0.0f;
    private float unGenerateBombTime = 0f;
    private List<IPlayer> players = new List<IPlayer>();
    private MapManager mapManager;
    // Start is called before the first frame update
    void Start()
    {
        GameObject blockGenerator = new GameObject("BlockGenerator");
        mapManager = blockGenerator.AddComponent<MapManager>();
        mapManager.transform.position = new Vector3(0, 0, 0);
        mapManager.BlockPrefab = BlockPrefab;
        mapManager.CreateBlocks();
        string assetPathName = "Config/MapSizeData";
        MapSize mapSizeData = Resources.Load<MapSize>(assetPathName);
        mapManager.SetColumnRowLayer(mapSizeData.Column, mapSizeData.Row, mapSizeData.Layer);
        lastMoveTime = 0.0f;
        unGenerateBombTime = 0.0f;
        if (PlayerPrefab != null) {
            for(int i = 0; i < playerNum; ++i)
            {
                GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
                mainCamera.transform.position = new Vector3(-1, mapSizeData.Layer + 1 + Height, -1);
                GameObject currentGO = Instantiate(PlayerPrefab, new Vector3(0, mapSizeData.Layer + Height, 0), Quaternion.identity, this.transform);
                PlayerSelf playerCom = currentGO.AddComponent<PlayerSelf>();
                playerCom.SetHeight(Height);
                playerCom.SetColumnRowLayer(mapSizeData.Column, mapSizeData.Row, mapSizeData.Layer);
                playerCom.SetPosition(0, mapSizeData.Layer , 0);
                playerCom.MapMgr = mapManager;
                players.Add(playerCom);
                mainCamera.transform.LookAt(currentGO.transform);
                mainCamera.GetComponent<Camera>().orthographic = true;
                currentGO.transform.SetParent(this.transform.parent);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        lastMoveTime += Time.deltaTime;
        if (lastMoveTime >= MoveTimeInterval)
        {
            for(int i = 0; i < players.Count; ++i)
            {
                players[i].Move();
            }
            lastMoveTime -= MoveTimeInterval;
        }
        unGenerateBombTime += Time.deltaTime;
        if (unGenerateBombTime >= GenerateBombTime)
        {
            for (int i = 0; i < players.Count; ++i)
            {
                players[i].CreateBomb();
            }
            unGenerateBombTime -= GenerateBombTime;
        }
    }
}
