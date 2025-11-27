
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
        private readonly Dictionary<string, int> m_ReferenceCounts = new();
        private readonly Dictionary<string, List<IFLoadAssetSuccessCallback>> m_OngoingCallbacks = new();


        public override void Update(float elapsedSeconds, float realElapsedSeconds)
        {

        }


        public override void Shutdown()
        {

        }


        /// <summary>
        /// Asynchronously loads an asset from the specified address with callbacks.
        /// </summary>
        public void LoadAssetAsyncWithCallbacks<T>(string assetAddress, IFLoadAssetSuccessCallback successCallback, object data) where T : UnityEngine.Object
        {
            if (string.IsNullOrEmpty(assetAddress))
            {
                throw new ArgumentException("Asset address cannot be null or empty.", nameof(assetAddress));
            }

            if (m_LoadedHandles.TryGetValue(assetAddress, out AsyncOperationHandle existingHandle))
            {
                if (existingHandle.Result is T result)
                {
                    m_ReferenceCounts[assetAddress]++;
                    successCallback?.Invoke(assetAddress, result, data);
                    return;
                }
            }

            // Check for ongoing loading of the same asset to avoid duplication
            if (m_OngoingCallbacks.TryGetValue(assetAddress, out List<IFLoadAssetSuccessCallback> callbacks))
            {
                callbacks.Add(successCallback);
                return;
            }
            
            // Start new load operation
            m_OngoingCallbacks[assetAddress] = new List<IFLoadAssetSuccessCallback> { successCallback };

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetAddress);
            handle.Completed += operation =>
            {
                if (m_OngoingCallbacks.TryGetValue(assetAddress, out List<IFLoadAssetSuccessCallback> callbacks))
                {
                    if (operation.Status == AsyncOperationStatus.Succeeded)
                    {
                        m_LoadedHandles[assetAddress] = handle;
                        m_ReferenceCounts[assetAddress] = 1;

                        foreach (var callback in callbacks)
                        {
                            callback?.Invoke(assetAddress, operation.Result, data);
                        }
                    }
                    else
                    {
                        foreach (var callback in callbacks)
                        {
                            callback?.Invoke(assetAddress, null, data);
                        }
                    }
                }
                
                m_OngoingCallbacks.Remove(assetAddress);
            };
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
                    m_ReferenceCounts[assetAddress]++;
                    return result;
                }
            }

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetAddress);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                m_LoadedHandles[assetAddress] = handle;
                m_ReferenceCounts[assetAddress] = 1;

                return handle.Result;
            }
            else
            {
                throw new Exception($"Failed to load asset at address: {assetAddress}");
            }
        }


        /// <summary>
        /// Unloads all loaded assets.
        /// </summary>
        public void UnloadAllAssets()
        {
            foreach (var handle in m_LoadedHandles.Values)
            {
                Addressables.Release(handle);
            }
            m_LoadedHandles.Clear();
        }


        /// <summary>
        /// Releases the asset at the specified address.
        /// </summary>
        /// <param name="assetAddress">Asset address</param>
        public void ReleaseAsset(string assetAddress)
        {
            if (m_LoadedHandles.TryGetValue(assetAddress, out AsyncOperationHandle handle))
            {
                if (m_ReferenceCounts.ContainsKey(assetAddress))
                {
                    m_ReferenceCounts[assetAddress]--;
                    if (m_ReferenceCounts[assetAddress] <= 0)
                    {
                        Addressables.Release(handle);
                        m_LoadedHandles.Remove(assetAddress);
                        m_ReferenceCounts.Remove(assetAddress);
                    }
                }
            }
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