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
        private Vector3 mapSize;
        /// <summary>
        /// 创建地图
        /// </summary>
        /// <param name="x">长</param>
        /// <param name="y">宽</param>
        /// <param name="z">高</param>
        /// <param name="obj">模型</param>
        public void CreateMap(int x, int y, int z, GameObject obj)
        {
            mapSize.Set(x, y, z);
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

        public Vector3 TryMove(Vector3 pos, MoveDirection direction)
        {
            Vector3 nextMoveMent = Vector3.zero;
            //判断是否可以移动，朝当前的方向移动一步
            switch (direction)
            {
                case MoveDirection.None:
                    break;
                case MoveDirection.Forward:
                    nextMoveMent.Set(0, 1, 0);
                    break;
                case MoveDirection.BackWard:
                    nextMoveMent.Set(0, -1, 0);
                    break;
                case MoveDirection.Left:
                    nextMoveMent.Set(-1, 0, 0);
                    break;
                case MoveDirection.Right:
                    nextMoveMent.Set(1, 0, 0);
                    break;
            }

            Vector3 nextPosition = pos + nextMoveMent;
            //判断目标位置是否合法
            bool nextPosValid = true;
            //高度小于目标点
            if (GetHeigh(pos.x, pos.y) < GetHeigh(nextPosition.x, nextPosition.y))
            {
                nextPosValid = false;
            }
            //超出范围
            if (IsOutBound(nextPosition))
            {
                nextPosValid = false;
            }

            if (nextPosValid)
                return nextPosition;
            else
                return pos;
        }

        /// <summary>
        /// 根据二维坐标，获取高度
        /// </summary>
        /// <returns></returns>
        public int GetHeigh(float x, float y)
        {
            return 1;
        }

        /// <summary>
        /// 是否查出范围
        /// </summary>
        /// <returns></returns>
        public bool IsOutBound(Vector3 targetPos)
        {
            return false;
        }
    }
}


