﻿#region License
// Copyright (c) 2018 Mark Kromis
// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using Microsoft.Win32;
using Prism.Events;
using System;
using System.Configuration;
using Wider.Core.Events;
using Wider.Core.Services;
using Wider.Core.Settings;

namespace Wider.Core.Settings
{
    internal class ThemeSettings : AbstractSettings, IThemeSettings
    {
        #region Event ThemeChangedEvent
        public ThemeSettings(IEventAggregator eventAggregator) => eventAggregator.GetEvent<ThemeChangeEvent>().Subscribe(NewSelectedTheme);

        private void NewSelectedTheme(ITheme theme)
        {
            SelectedTheme = theme.Name;
            Save();
        }
        #endregion

        [UserScopedSetting()]
        [DefaultSettingValue("Default")]
        public String SelectedTheme
        {
            get => (String)this["SelectedTheme"];
            set => this["SelectedTheme"] = value;
        }

        /// <summary>
        /// Try to deterimine what windows theme is applied in settings
        /// </summary>
        /// <returns></returns>
        public String GetSystemTheme()
        {
            String key = $@"{Registry.CurrentUser}\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            Object value = Registry.GetValue(key, "AppsUseLightTheme", null);
            if (value == null) { return "Default";  };
            return (Int32)value == 0 ? "Dark" : "Light";
        }
    }
}