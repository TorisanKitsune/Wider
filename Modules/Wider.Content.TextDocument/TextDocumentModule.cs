﻿using Wider.Content.TextDocument.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Wider.Content.TextDocument.ViewModels;
using Wider.Content.Services;
using Wider.Interfaces.Services;
using Prism.Events;
using Wider.Interfaces.Events;

namespace Wider.Content.TextDocument
{
    public class TextDocumentModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        private IEventAggregator EventAggregator => _container.Resolve<IEventAggregator>();

        public TextDocumentModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            // Note splash page
            EventAggregator.GetEvent<SplashMessageUpdateEvent>()
                .Publish(new SplashMessageUpdateEvent { Message = "Loading TextDocument Module" });

            // Register container types
            _container.RegisterType<TextViewModel>();
            _container.RegisterType<TextModel>();
            _container.RegisterType<TextView>();
            _container.RegisterType<AllFileHandler>();

            //Register a default file opener
            IContentHandlerRegistry registry = _container.Resolve<IContentHandlerRegistry>();
            registry.Register(_container.Resolve<AllFileHandler>());
        }
    }
}