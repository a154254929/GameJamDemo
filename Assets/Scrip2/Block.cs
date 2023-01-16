﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamDemo
{
    /// <summary>
    /// 格子类，负责格子的模型，创建or销毁
    /// </summary>
    public class Block
    {
        private MapManager manager = GameManager.Instance.mapManager;
        public bool IsActive
        {
            set
            {
                if (m_obj != null)
                {
                    m_obj.SetActive(value);
                }
            }
            get
            {
                return m_obj.activeSelf;
            }
        }
        private GameObject m_obj;
        private MeshRenderer m_meshRender;
        private Vector3Int m_position = Vector3Int.zero;
        private MaterialPropertyBlock prop = new MaterialPropertyBlock();

        public Block(int x, int y, int z, GameObject obj, Transform parent)
        {
            m_position.Set(x, y, z);
            Vector3 createPos = new Vector3(x, z, y);
            if (obj != null)
            {
                m_obj = GameObject.Instantiate(obj, createPos, Quaternion.identity, parent);
                m_meshRender = m_obj.transform.Find("Block").GetComponent<MeshRenderer>();
                m_meshRender.GetPropertyBlock(prop);
                GameManager gameMgr = GameManager.Instance;
                prop.SetFloat("_Top", gameMgr.gameConfig.StepColors.Length);
                List<Vector4> vects = new List<Vector4>();
                for(int i = 0; i < gameMgr.gameConfig.StepColors.Length; ++i)
                {
                    vects.Add((Vector4)gameMgr.gameConfig.StepColors[i]);
                }
                prop.SetVectorArray("_StepColors", vects);
                m_meshRender.SetPropertyBlock(prop);
            }
        }

        public Vector3 GetObjTransPosition()
        {
            return m_obj.transform.position;
        }

        public void SetParent(float parency)
        {
            m_meshRender.GetPropertyBlock(prop);
            prop.SetFloat("_Alpha", parency);
            m_meshRender.SetPropertyBlock(prop);
        }
    }
}


