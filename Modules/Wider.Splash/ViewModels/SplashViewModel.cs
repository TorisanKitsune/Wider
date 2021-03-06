﻿#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using Prism.Events;
using Prism.Mvvm;
using System;
using Wider.Core.Events;

namespace Wider.Splash.ViewModels
{
    public class SplashViewModel : BindableBase
    {
        #region Declarations

        private String _status;

        #endregion

        #region CTOR

        public SplashViewModel(IEventAggregator eventAggregator_) => 
            eventAggregator_.GetEvent<SplashMessageUpdateEvent>().Subscribe(e_ => UpdateMessage(e_.Message));

        #endregion

        #region Public Properties

        public String Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        #endregion

        #region Private Methods

        private void UpdateMessage(String message)
        {
            if (String.IsNullOrEmpty(message))
            {
                return;
            }

            Status = String.Concat(Environment.NewLine, message, "...") + Status;
        }

        #endregion
    }
}