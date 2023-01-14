using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamDemo
{
    /// <summary>
    /// 管理地图的创建以及格子数据的维护
    /// </summary>
    public class MapManager
    {
        private Block[,,] m_blocks;

        /// <summary>
        /// 创建地图
        /// </summary>
        /// <param name="x">长</param>
        /// <param name="y">宽</param>
        /// <param name="z">高</param>
        /// <param name="obj">模型</param>
        public void CreateMap(int x, int y, int z, GameObject obj)
        {
            m_blocks = new Block[x, y, z];
            for (int layer = 0; layer < m_blocks.GetLength(2); layer++)
            {
                for (int indexX = 0; indexX < m_blocks.GetLength(1); indexX++)
                {
                    for (int indexY = 0; indexY < m_blocks.GetLength(2); indexY++)
                    {
                        m_blocks[indexX, indexY, layer] = new Block(indexX, indexY, layer, obj);
                    }
                }
            }
        }

        public Block GetBlock(int x, int y, int z)
        {
            return m_blocks[x, y, z];
        }
    }
}


