using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int Column = 6;
    public int Row = 6;
    public int Layer = 10;
    public GameObject BlockPrefab = null;
    private List<List<List<Blocks>>> blocks;
    // Start is called before the first frame update

    void Start()
    {
        this.transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetColumnRowLayer(int column, int row, int layer)
    {
        this.Column = column;
        this.Row = row;
        this.Layer = layer;
    }

    public void CreateBlocks()
    {
        blocks = new List<List<List<Blocks>>>();
        int count = 0;
        if (BlockPrefab != null)
        {
            for (int i = 0; i < Column; ++i)
            {
                List<List<Blocks>> currentLayer = new List<List<Blocks>>();
                for (int j = 0; j < Layer; ++j)
                {
                    List<Blocks> currentRow = new List<Blocks>();
                    for (int k = 0; k < Row; ++k)
                    {
                        GameObject currentGO = Instantiate(BlockPrefab, new Vector3((float)i, (float)(j + 0.5), (float)k), Quaternion.identity, this.transform);
                        currentGO.name = string.Format("Block{0}", count);
                        currentRow.Add(currentGO.AddComponent<Blocks>());
                        count++;
                    }
                    currentLayer.Add(currentRow);
                }
                blocks.Add(currentLayer);
            }
        }
    }

    public void OnBombExplod(int x, int y, int z)
    {
        if(x + 1 < Column && y - 1 >= 0) blocks[x + 1][y - 1][z].OnBombExplod();
        if(x - 1 >= 0 && y - 1 >= 0) blocks[x -1][y - 1][z].OnBombExplod();
        if(y - 1 >= 0) blocks[x][y - 1][z].OnBombExplod();
        if(z + 1 < Row && y - 1 >= 0) blocks[x][y - 1][z + 1].OnBombExplod();
        if(z - 1 >= 0 && y - 1 >= 0) blocks[x][y - 1][z - 1].OnBombExplod();
    }

    public bool HaveBlock(int x, int y, int z)
    {
        if(x < Column && x >= 0 && y < Layer && y >= 0 && z < Row && z >= 0)
        {
            return !blocks[x][y][z].GetIsDestroy();
        }
        return false;
    }
}
