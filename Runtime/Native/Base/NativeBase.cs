#nullable enable
using System;
using System.Collections.Generic;
using System.Threading;
using AffiseAttributionLib.Extensions;
using AffiseAttributionLib.Native.Base;
using AffiseAttributionLib.Utils;
using SimpleJSON;
using UnityEngine;
#if UNITY_ANDROID && !UNITY_EDITOR
using AffiseAttributionLib.Native.Android;
#endif

#if UNITY_IOS && !UNITY_EDITOR
using AffiseAttributionLib.Native.IOS;
#endif

namespace AffiseAttributionLib.Native
{
    internal abstract class NativeBase
    {
        internal readonly struct CallbackData
        {
            public object Callback { get; }
            public SynchronizationContext Context { get; }
            public int ThreadId { get; }

            private CallbackData(object callback, SynchronizationContext context, int threadId)
            {
                Callback = callback;
                Context = context;
                ThreadId = threadId;
            }

            public CallbackData(object callback) : this(
                callback: callback, 
                context: SynchronizationContext.Current,
                threadId: Thread.CurrentThread.ManagedThreadId)
            {
            }

            public bool IsManagedThread => ThreadId == Thread.CurrentThread.ManagedThreadId;
        }
        
        private const string UUID = "callback_uuid";
        private const string TAG = "callback_tag";

        private readonly INative? _native = null;

        private readonly Dictionary<string, CallbackData> _callbacksOnce = new();
        
        private readonly Dictionary<string, Dictionary<string, CallbackData>> _callbacksGroup = new();
        
        private readonly Dictionary<AffiseApiMethod, CallbackData> _callbacks = new();

        protected NativeBase()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            _native = new NativeAndroid();
#endif
#if UNITY_IOS && !UNITY_EDITOR
            _native = new NativeIOS();
#endif
            _native?.SetCallback(AffiseCallback);
        }

        private void NativeHandleDeeplink(string? url)
        {
            if (string.IsNullOrWhiteSpace(url)) return;
            _native?.NativeHandleDeeplink(url);
        }

        protected void InitialLink()
        {
            NativeHandleDeeplink(Application.absoluteURL);
        }

        protected abstract void HandleCallback(AffiseApiMethod api, object callback, JSONNode? json, string? tag);

        protected T? Native<T>(AffiseApiMethod api, object? data, string? uuid = null)
        {
            var apiName = api.ToMethod();
            if (apiName is null) return default;
            var json = new JSONObject
            {
                [apiName] = data?.ToJsonNode(),
                [UUID] = uuid
            };
            if (_native is null) return default;
            return _native.Native<T>(apiName, json.ToString());
        }

        protected T? Native<T>(AffiseApiMethod api)
        {
            if (_native is null) return default;
            var apiName = api.ToMethod();
            if (apiName is null) return default;
            return _native.Native<T>(apiName);
        }

        protected void Native(AffiseApiMethod api, object? data)
        {
            var apiName = api.ToMethod();
            if (apiName is null) return;
            var json = new JSONObject
            {
                [apiName] = data?.ToJsonNode(),
            };
            _native?.Native(apiName, json.ToString());
        }

        protected void Native(AffiseApiMethod api)
        {
            var apiName = api.ToMethod();
            if (apiName is null) return;
            _native?.Native(apiName);
        }

        protected JSONObject? NativeMap(AffiseApiMethod api)
        {
            try
            {
                var data = Native<string>(api);
                return JSON.Parse(data).AsObject;
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

        protected JSONArray? NativeList(AffiseApiMethod api)
        {
            try
            {
                var data = Native<string>(api);
                return JSON.Parse(data).AsArray;
            }
            catch (Exception)
            {
                // ignored
            }
            return null;
        }

        protected void NativeCallbackOnce(AffiseApiMethod api, object callback, object? data = null)
        {
            var apiName = api.ToMethod();
            if (apiName is null) return;
            var uuid = Uuid.Generate();
            var json = new JSONObject
            {
                [apiName] = data?.ToJsonNode(),
                [UUID] = uuid
            };
            _callbacksOnce[uuid] = new CallbackData(callback);
            _native?.Native(apiName, json.ToString());
        }
        
        protected void NativeCallbackGroup(AffiseApiMethod api, Dictionary<string, CallbackData> callbackGroup, object? data = null)
        {
            var apiName = api.ToMethod();
            if (apiName is null) return;
            var uuid = Uuid.Generate();
            var json = new JSONObject
            {
                [apiName] = data?.ToJsonNode(),
                [UUID] = uuid
            };
            _callbacksGroup[uuid] = callbackGroup;
            _native?.Native(apiName, json.ToString());
        }
        
        protected void NativeCallback(AffiseApiMethod api, object callback, object? data = null)
        {
            var apiName = api.ToMethod();
            if (apiName is null) return;
            var json = new JSONObject
            {
                [apiName] = data?.ToJsonNode(),
            };
            _callbacks[api] = new CallbackData(callback);
            _native?.Native(apiName, json.ToString());
        }

        protected void NativeCallbackOnly(AffiseApiMethod api, object callback)
        {
            _callbacks[api] = new CallbackData(callback);
        }

        private void AffiseCallback(string apiName, string data)
        {
            var api = apiName.ToAffiseApiMethod();
            if (api is null) return;
            var (uuid, tag, json) = GetCallbackValue(data, api);
            if (uuid is null)
            {
                GetCallback(api, callback =>
                {
                    OnCallbackCall(api, callback, json, tag);
                });
            }
            else if (!string.IsNullOrWhiteSpace(tag))
            {
                GetCallbackGroup(uuid, tag, callback =>
                {
                    OnCallbackCall(api, callback, json, tag);
                });
            }
            else
            {
                GetCallbackOnce(uuid, callback =>
                {
                    OnCallbackCall(api, callback, json, tag);
                });
            }
        }

        private void OnCallbackCall(AffiseApiMethod? api, CallbackData callbackData, JSONNode? json, string? tag)
        {
            if (api is null)  return;
            
            // if (callbackData.IsManagedThread)
            // {
            //     // Don't use this. Closure capture cause as bug, native methods return null.
            //     HandleCallback((AffiseApiMethod)api, callbackData.Callback, json, tag);
            // }
            // else
            // {
                callbackData.Context.Post(_ =>
                {
                    HandleCallback((AffiseApiMethod)api, callbackData.Callback, json, tag);
                }, null);
            // }
        }

        private Tuple<string?, string?, JSONNode?> GetCallbackValue(string data, AffiseApiMethod? api)
        {
            try
            {
                var json = JSON.Parse(data);
                var uuid = json[UUID];
                var tag = json[TAG];
                var apiName = api?.ToMethod();
                if (apiName is null)
                {
                    return new Tuple<string?, string?, JSONNode?>(uuid, tag, null);
                }

                var value = json[apiName];
                return new Tuple<string?, string?, JSONNode?>(uuid, tag, value);
            }
            catch (Exception)
            {
                // ignored
            }
            return new Tuple<string?, string?, JSONNode?>(null, null, null);
        }

        private void GetCallbackOnce(string? uuid, Action<CallbackData> action)
        {
            if (uuid is null) return;
            if (!_callbacksOnce.ContainsKey(uuid)) return;
            action.Invoke(_callbacksOnce[uuid]);
            _callbacksOnce.Remove(uuid);
        }
        
        private void GetCallbackGroup(string? uuid, string? tag, Action<CallbackData> action)
        {
            if (uuid is null) return;
            if (!_callbacksGroup.ContainsKey(uuid)) return;
            var callbackTag = _callbacksGroup[uuid];
            if (tag is not null && callbackTag.ContainsKey(tag))
            {
                action.Invoke(callbackTag[tag]);
            }
            _callbacksGroup.Remove(uuid);
        }
        
        private void GetCallback(AffiseApiMethod? api, Action<CallbackData> action)
        {
            if (api is null) return;
            var key = (AffiseApiMethod)api;
            if (!_callbacks.ContainsKey(key))
            {
                return;
            }
            action.Invoke(_callbacks[key]);
        }
    }
}