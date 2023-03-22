﻿using Stylet;
using XboxCsMgr.Client.Events;
using XboxCsMgr.XboxLive;

namespace XboxCsMgr.Client.ViewModels
{
    /// <summary>
    /// The ShellViewModel represents the ShellView which is the base window
    /// of the application. It's responsible for containing and managing other
    /// sub-views, etc.
    /// </summary>
    public class ShellViewModel : Screen, IHandle<LoadSaveDetailsEvent>
    {
        private readonly IWindowManager _windowManager;
        private IEventAggregator _events;
        private XboxLiveConfig _xblConfig => AppBootstrapper.XblConfig;

        private GameViewModel _gameView;
        public GameViewModel GameView
        {
            get => _gameView;
            set => SetAndNotify(ref this._gameView, value);
        }

        private SaveViewModel _saveView;
        public SaveViewModel SaveView
        {
            get => _saveView;
            set => SetAndNotify(ref this._saveView, value);
        }

        public ShellViewModel(IWindowManager windowManager, IEventAggregator events)
        {
            _windowManager = windowManager;
            _events = events;

            _events.Subscribe(this);
        }

        public void Handle(LoadSaveDetailsEvent message)
        {
            Screen vm;
            vm = new SaveViewModel(_events, _xblConfig, message.PackageFamilyName, message.ServiceConfigurationId);
            SaveView = (SaveViewModel)vm;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        protected override void OnViewLoaded()
        {
            base.OnViewLoaded();

            GameView = new GameViewModel(_events, _xblConfig);
        }
    }
}
