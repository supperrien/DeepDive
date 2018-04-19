﻿using Clio.Utilities;
using Deep.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Deep.Helpers
{
    internal class WPF
    {
        /// <summary>
        /// load our Resource file that contains styles and magic.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="control"></param>
        private static void LoadResourceForWindow(string filename, UserControl control)
        {
            try
            {
                var resource = LoadAndTransformXamlFile<ResourceDictionary>(filename);
                foreach (System.Collections.DictionaryEntry res in resource)
                {
                    if (!control.Resources.Contains(res.Key))
                        control.Resources.Add(res.Key, res.Value);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error Loading Resource {0}", ex);
            }
        }

        /// <summary>
        /// loads up the window content for an xml file.
        /// </summary>
        /// <param name="xamlFilePath"></param>
        /// <returns></returns>
        public static UserControl LoadWindowContent([NotNull]string xamlFilePath)
        {

            try
            {
                var windowContent = LoadAndTransformXamlFile<UserControl>(xamlFilePath);
                //if (File.Exists(Path.Combine(Path.GetDirectoryName(xamlFilePath), "Dictionary.xaml")))
                //    LoadResourceForWindow(Path.Combine(Path.GetDirectoryName(xamlFilePath), "Dictionary.xaml"), windowContent);
                return windowContent;
            }
            catch (Exception arg)
            {
                Logger.Error("Exception loading window content! {0}", arg);
            }
            return null;
        }
        /// <summary>
        /// loads our xaml files into a type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xamlText"></param>
        /// <returns></returns>
        public static T LoadAndTransformXamlFile<T>(string xamlText)
        {
            T result;
            try
            {
                //var xamlText = File.ReadAllText(filePath);
                xamlText = Regex.Replace(xamlText, "xmlns:(.+?)=\"clr-namespace:([\\w\\d\\._]+?)\"",
                    "xmlns:$1=\"clr-namespace:$2;assembly=" + Assembly.GetCallingAssembly().GetName().Name + "\"",
                    RegexOptions.Compiled | RegexOptions.Singleline);
                xamlText = Regex.Replace(xamlText,
                    "<ResourceDictionary.MergedDictionaries>.*</ResourceDictionary.MergedDictionaries>", string.Empty,
                    RegexOptions.Compiled | RegexOptions.Singleline);
                result = (T)XamlReader.Load(new MemoryStream(Encoding.UTF8.GetBytes(xamlText)));
            }
            catch (Exception exception)
            {
                Logger.Error("Error loading/transforming XAML\n{0}", exception);
                result = default(T);
            }
            return result;
        }
    }
}
