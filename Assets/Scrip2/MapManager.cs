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
        private Vector3Int m_mapSize;
        private GameObject m_mapRoot;

        /// 方块是否下落
        //private bool isBlockDrop = false;

        public Transform GetRootTramsform()
        {
            return m_mapRoot.transform;
        }
        /// <summary>
        /// 创建地图
        /// </summary>
        /// <param name="x">长</param>
        /// <param name="y">宽</param>
        /// <param name="z">高</param>
        /// <param name="obj">模型</param>
        public void CreateMap(Vector3Int mapSize, GameObject obj)
        {
            m_mapRoot = new GameObject("Map");
            m_mapSize = mapSize;
            m_blocks = new Block[m_mapSize.x, m_mapSize.y, m_mapSize.z];
            for (int layer = 0; layer < m_blocks.GetLength(2); layer++)
            {
                for (int indexX = 0; indexX < m_blocks.GetLength(0); indexX++)
                {
                    for (int indexY = 0; indexY < m_blocks.GetLength(1); indexY++)
                    {
                        m_blocks[indexX, indexY, layer] = new Block(indexX, indexY, layer, obj, m_mapRoot.transform);
                    }
                }
            }
        }

        public Block GetBlock(Vector3Int pos)
        {
            if (IsOutBound(pos))
            {
                return null;
            }

            return m_blocks[pos.x, pos.y, pos.z];
        }

        public Vector3Int GetNextPosition(Vector3Int curPos, MoveDirection direction)
        {
            Vector3Int nextMoveMent = Vector3Int.zero;
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

            return curPos + nextMoveMent;
        }

        public Vector3Int TryMove(Vector3Int curPos, MoveDirection direction)
        {
            bool canMove = true;
            Vector3Int nextPosition = GetNextPosition(curPos, direction);

            //超出范围
            if (IsOutBound(nextPosition))
            {
                canMove = false;
            }
            if (GetHeight(curPos.x, curPos.y) < GetHeight(nextPosition.x, nextPosition.y))
            {
                canMove = false;
            }

            ////如果方块会自动下落
            //if (isBlockDrop)
            //{
            //    //高度小于目标点
            //    if (GetHeight(curPos.x, curPos.y) < GetHeight(nextPosition.x, nextPosition.y))
            //    {
            //        canMove = false;
            //    }
            //}
            //else
            //    canMove = GetBlock(nextPosition.x, nextPosition.y, nextPosition.z).IsActive;

            if (canMove)
                return nextPosition;
            else
                return curPos;
        }

        /// <summary>
        /// 根据二维坐标，获取高度
        /// </summary>
        /// <returns></returns>
        public int GetHeight(int x, int y)
        {
            for (int layer = 0; layer < m_blocks.GetLength(2); layer++)
            {
                if (!m_blocks[x, y, layer].IsActive)
                {
                    return layer;
                }
            }
            return 0;
        }

        /// <summary>
        /// 是否查出范围
        /// </summary>
        /// <returns></returns>
        public bool IsOutBound(Vector3Int targetPos)
        {
            return targetPos.x < 0 || targetPos.x >= m_mapSize.x||
                targetPos.y < 0 || targetPos.y >= m_mapSize.y||
                targetPos.z < 0 || targetPos.z >= m_mapSize.z;
        }

        /// <summary>
        /// 爆炸，以参数位置为中心，十字形消除周围格子，随后更新所有玩家位置
        /// </summary>
        /// <param name="pos"></param>
        public void Explode(Vector3Int pos)
        {
            Block Left = GetBlock(GetNextPosition(pos, MoveDirection.Left));
            Block Right = GetBlock(GetNextPosition(pos, MoveDirection.Right));
            Block Down = GetBlock(GetNextPosition(pos, MoveDirection.Down));

            if (Left != null)
            {
                Left.IsActive = false;
            }
            if (Right != null)
            {
                Right.IsActive = false;
            }
            if (Down != null)
            {
                Down.IsActive = false;
            }
        }
    }
}


