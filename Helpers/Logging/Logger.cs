﻿using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using Clio.Utilities;
using Deep.Helpers;
using Deep.Properties;
using ff14bot;
using ff14bot.Enums;
using ff14bot.Helpers;
using rLogging = ff14bot.Helpers.Logging;

namespace Deep.Logging
{
    internal static class Logger
    {

        internal static string Name => Constants.Lang == Language.Chn ? "深层迷宫" : "DeepDive";
        private static string Prefix => $"[{Name}] ";

        [StringFormatMethod("format")]
        internal static void Error(string message, params object[] args)
        {
            Log(LogColors.Error, message, args);
        }

        private static void Log(Color c, string message, params object[] args)
        {
            var str = Resources.ResourceManager.GetString(message);
            if (string.IsNullOrEmpty(str))
                rLogging.Write(c, Prefix + string.Format(message, args));
            else
                rLogging.Write(c, Prefix + string.Format(str, args));
        }

        [StringFormatMethod("format")]
        internal static void Info(string message, params object[] args)
        {
            Log(LogColors.Info, message, args);
        }

        [StringFormatMethod("format")]
        internal static void Verbose(string format, params object[] args)
        {
            if (Settings.Instance.VerboseLogging)
            {
                Log(LogColors.Verbose, format, args);
            }
            else
            {
                rLogging.WriteToFileSync(LogLevel.Verbose, format, args);
            }
        }


        [StringFormatMethod("format")]
        internal static void Warn(string format, params object[] args)
        {
            Log(LogColors.Warn, format, args);
        }
    }
}