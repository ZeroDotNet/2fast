﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Microsoft.Services.Store.Engagement;
using Prism.Logging;

namespace Project2FA.UWP
{
    //https://github.com/windows-toolkit/WindowsCommunityToolkit/blob/master/Microsoft.Toolkit.Uwp.SampleApp/TrackingManager.cs
    public static class TrackingManager
    {
        private static readonly StoreServicesCustomEventLogger _logger;
        private static bool _debugMode;

        static TrackingManager()
        {
            try
            {
                _debugMode = System.Diagnostics.Debugger.IsAttached;
                _logger = StoreServicesCustomEventLogger.GetDefault();
            }
            catch
            {
                // Ignoring error
            }
        }

        public static void TrackExceptionUnhandled(string method, Exception ex)
        {
            try
            {
                if (!_debugMode)
                {
                    _logger.Log($"crit {method}-{ex.Message}-{ex.StackTrace}");
                }
            }
            catch
            {
                // Ignore error
            }
        }

        public static void TrackException(string method,Exception ex)
        {
            try
            {
                if (!_debugMode)
                {
                    _logger.Log($"ex {method}-{ex.Message}-{ex.StackTrace}");
                }
            }
            catch
            {
                // Ignore error
            }
        }

        public static void TrackExceptionCatched(string method, Exception ex)
        {
            try
            {
                if (!_debugMode)
                {
                    _logger.Log($"exc {method}-{ex.StackTrace}");
                    //_logger.Log($"exception - catched - {ex.Message} - {ex.InnerException} - {ex.StackTrace}");
                }
            }
            catch
            {
                // Ignore error
            }
        }
    }
}
