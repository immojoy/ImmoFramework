
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;


namespace ImmoFramework.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("ImmoFramework/Component/ImmoFramework Resource")]
    public sealed class IFResourceComponent : IFComponent
    {
        private IFResourceModule m_ResourceModule;


        #region Unity Callbacks
        protected override void Awake()
        {
            base.Awake();

            m_ResourceModule = IFModuleEntry.GetModule<IFResourceModule>();
            if (m_ResourceModule == null)
            {
                Debug.LogError("Invalid resource module.");
                return;
            }
        }

        private void Start()
        {

        }
        #endregion


        public void LoadAssetAsyncWithCallbacks<T>(string assetAddress, IFLoadAssetSuccessCallback successCallback, object data) where T : UnityEngine.Object
        {
            m_ResourceModule.LoadAssetAsyncWithCallbacks<T>(assetAddress, successCallback, data);
        }


        public async Task<T> LoadAssetAsync<T>(string assetAddress) where T : UnityEngine.Object
        {
            return await m_ResourceModule.LoadAssetAsync<T>(assetAddress);
        }


        public void UnloadAllAssets()
        {
            m_ResourceModule.UnloadAllAssets();
        }


        public bool IsAssetLoaded(string assetAddress)
        {
            return m_ResourceModule.IsAssetLoaded(assetAddress);
        }


        public void ReleaseAsset(string assetAddress)
        {
            m_ResourceModule.ReleaseAsset(assetAddress);
        }
    }
}
