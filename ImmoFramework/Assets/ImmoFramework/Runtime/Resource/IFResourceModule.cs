
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ImmoFramework.Runtime
{
    public sealed class IFResourceModule : IFModule
    {
        private readonly Dictionary<string, AsyncOperationHandle> m_LoadedHandles = new();


        public override void Update(float elapsedSeconds, float realElapsedSeconds)
        {

        }


        public override void Shutdown()
        {

        }


        /// <summary>
        /// Asynchronously loads an asset from the specified address.
        /// </summary>
        public async Task<T> LoadAssetAsync<T>(string assetAddress) where T : UnityEngine.Object
        {
            if (string.IsNullOrEmpty(assetAddress))
            {
                throw new ArgumentException("Asset address cannot be null or empty.", nameof(assetAddress));
            }

            if (m_LoadedHandles.TryGetValue(assetAddress, out AsyncOperationHandle existingHandle))
            {
                if (existingHandle.Result is T result)
                {
                    return result;
                }
            }

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetAddress);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                m_LoadedHandles[assetAddress] = handle;
                return handle.Result;
            }
            else
            {
                throw new Exception($"Failed to load asset at address: {assetAddress}");
            }
        }


        /// <summary>
        /// Synchronously loads an asset from the specified address.
        /// </summary>
        public T LoadAsset<T>(string assetAddress) where T : UnityEngine.Object
        {
            return LoadAssetAsync<T>(assetAddress).GetAwaiter().GetResult();
        }


        /// <summary>
        /// Unloads the asset at the specified address.
        /// </summary>
        public void UnloadAsset(string assetAddress)
        {
            if (m_LoadedHandles.TryGetValue(assetAddress, out AsyncOperationHandle handle))
            {
                Addressables.Release(handle);
                m_LoadedHandles.Remove(assetAddress);
            }
        }


        /// <summary>
        /// Unloads all loaded assets.
        /// </summary>
        public void UnloadAllAsset()
        {
            foreach (var handle in m_LoadedHandles.Values)
            {
                Addressables.Release(handle);
            }
            m_LoadedHandles.Clear();
        }


        /// <summary>
        /// Checks if the asset at the specified address is loaded.
        /// </summary>
        public bool IsAssetLoaded(string assetAddress)
        {
            return m_LoadedHandles.ContainsKey(assetAddress);
        }
    }
}