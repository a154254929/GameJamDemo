using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int Columm = 6;
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
        this.Columm = column;
        this.Row = row;
        this.Layer = layer;
    }

    public void CreateBlocks()
    {
        blocks = new List<List<List<Blocks>>>();
        int count = 0;
        if (BlockPrefab != null)
        {
            for (int i = 0; i < Layer; ++i)
            {
                List<List<Blocks>> currentLayer = new List<List<Blocks>>();
                for (int j = 0; j < Row; ++j)
                {
                    List<Blocks> currentRow = new List<Blocks>();
                    for (int k = 0; k < Columm; ++k)
                    {
                        GameObject currentGO = Instantiate(BlockPrefab, new Vector3((float)k, (float)(i + 0.5), (float)j), Quaternion.identity, this.transform);
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
}
