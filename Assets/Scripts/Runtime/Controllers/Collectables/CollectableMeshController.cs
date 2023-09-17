﻿using Assets.Scripts.Runtime.Data.ValueObject;
using UnityEngine;

namespace Assets.Scripts.Runtime.Controllers.Collectables
{
    public class CollectableMeshController : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;
        private CollectableMeshData _data;
        private void OnEnable()
        {
            ActivateMeshVisuals();
        }

        internal void SetMeshData(CollectableMeshData meshData)
        {
            _data = meshData;
        }

        private void ActivateMeshVisuals()
        {
            meshFilter.mesh = _data.MeshList[0];
        }

        internal void UpgradeCollectableVisual(int value)
        {
            meshFilter.mesh = _data.MeshList[value];
        }
    }
}