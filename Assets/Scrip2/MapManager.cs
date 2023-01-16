using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
                case MoveDirection.Up:
                    nextMoveMent.Set(0, 0, 1);
                    break;
                case MoveDirection.Down:
                    nextMoveMent.Set(0, 0, -1);
                    break;
                case MoveDirection.ForwardLeft:
                    nextMoveMent.Set(-1, 1, 0);
                    break;
                case MoveDirection.ForwardRight:
                    nextMoveMent.Set(1, 1, 0);
                    break;
                case MoveDirection.BackWardLeft:
                    nextMoveMent.Set(-1, -1, 0);
                    break;
                case MoveDirection.BackWardRight:
                    nextMoveMent.Set(1, -1, 0);
                    break;
            }

            return curPos + nextMoveMent;
        }

        public Vector3Int TryMove(Vector3Int curPos, MoveDirection direction, out bool gameOver)
        {
            gameOver = false;
            Vector3Int nextPosition = GetNextPosition(curPos, direction);
            Vector3Int result = nextPosition;
            //是否超出边界
            if (IsOutBound(nextPosition))
            {
                result = curPos;
            }
            //目标位置上方没有物体才能移动
            Vector3Int nextAbovePos = nextPosition + new Vector3Int(0, 0, 1);
            if (GetBlock(nextAbovePos) != null && GetBlock(nextAbovePos).IsActive)
            {
                result = curPos;
            }
            //计算目标点的高度
            bool haveBlock = true;
            result = GetActiveBlockPos(result, out haveBlock);
            if (!haveBlock)
            {
                gameOver = true;
            }

            return result;
        }

        /// <summary>
        /// 在指定位置从上到下找一个显示的方块的位置
        /// </summary>
        /// <returns></returns>
        public Vector3Int GetActiveBlockPos(Vector3Int pos, out bool haveBlock)
        {
            for (int layer = pos.z; layer >= 0; layer--)
            {
                if (m_blocks[pos.x, pos.y, layer].IsActive)
                {
                    haveBlock = true;
                    return new Vector3Int(pos.x, pos.y, layer);
                }
            }

            haveBlock = false;
            return new Vector3Int(pos.x, pos.y, 0);
        }

        /// <summary>
        /// 是否查出范围
        /// </summary>
        /// <returns></returns>
        public bool IsOutBound(Vector3Int targetPos)
        {
            return targetPos.x < 0 || targetPos.x >= m_mapSize.x ||
                targetPos.y < 0 || targetPos.y >= m_mapSize.y ||
                targetPos.z < 0 || targetPos.z >= m_mapSize.z;
        }

        /// <summary>
        /// 获取炸弹周围会被炸掉的格子
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public List<Block> GetExplodedBlockList(Vector3Int pos)
        {
            if (pos.z >= m_mapSize.z - 1)
                return null;

            List<Block> list = new List<Block>();
            pos = pos + new Vector3Int(0, 0, 1);

            Block Left = GetBlock(GetNextPosition(pos, MoveDirection.Left));
            Block Right = GetBlock(GetNextPosition(pos, MoveDirection.Right));
            Block Forward = GetBlock(GetNextPosition(pos, MoveDirection.Forward));
            Block BackWard = GetBlock(GetNextPosition(pos, MoveDirection.BackWard));
            Block ForwardLeft = GetBlock(GetNextPosition(pos, MoveDirection.ForwardLeft));
            Block ForwardRight = GetBlock(GetNextPosition(pos, MoveDirection.ForwardRight));
            Block BackWardLeft = GetBlock(GetNextPosition(pos, MoveDirection.BackWardLeft));
            Block BackWardRight = GetBlock(GetNextPosition(pos, MoveDirection.BackWardRight));

            if (Left != null)
                list.Add(Left);
            if (Right != null)
                list.Add(Right);
            if (Forward != null)
                list.Add(Forward);
            if (BackWard != null)
                list.Add(BackWard);
            if (ForwardLeft != null)
                list.Add(ForwardLeft);
            if (ForwardRight != null)
                list.Add(ForwardRight);
            if (BackWardLeft != null)
                list.Add(BackWardLeft);
            if (BackWardRight != null)
                list.Add(BackWardRight);

            return list;
        }

        /// <summary>
        /// 隐藏格子，防止挡住玩家视线。
        /// 半透明层数1
        /// 透全明层数2
        /// </summary>
        /// <param name="layer"></param>
        public void HideUpLayer(int curLayer)
        {
            int topLayerIndex = m_blocks.GetLength(2) - 1;
            int bottomLayerIndex = curLayer + 2;
            float alpha = 1;
            for (int layer = topLayerIndex; layer >= bottomLayerIndex; layer--)
            {
                if (layer >= curLayer + 1)
                {
                    alpha = 0.1f;
                }
                if (layer >= curLayer + 3)
                {
                    alpha = 0;
                }

                for (int indexX = 0; indexX < m_blocks.GetLength(0); indexX++)
                {
                    for (int indexY = 0; indexY < m_blocks.GetLength(1); indexY++)
                    {
                        m_blocks[indexX, indexY, layer].SetTransparent(alpha);
                    }
                }
            }
        }
    }
}


