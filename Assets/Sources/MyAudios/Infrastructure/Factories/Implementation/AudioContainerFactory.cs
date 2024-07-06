using System;
using Sources.MyAudios.Domain.Constant;
using Sources.MyAudios.Infrastructure.Factories.Interfaces;
using Sources.MyAudios.Presentations.Views.Implementation;
using Sources.ObjectPools.Implementation.Objects;
using Sources.ObjectPools.Interfaces.Generic;

namespace Sources.MyAudios.Infrastructure.Factories.Implementation
{
    public class AudioContainerFactory : PoolableObjectFactory<UiAudioSource>, IAudioContainerFactory
    {
        private readonly IObjectPool<UiAudioSource> _uiAudioSourcePool;

        public AudioContainerFactory(IObjectPool<UiAudioSource> uiAudioSourcePool) 
            : base(uiAudioSourcePool)
        {
            _uiAudioSourcePool = uiAudioSourcePool ?? throw new ArgumentNullException(nameof(uiAudioSourcePool));
        }

        public UiAudioSource Create(UiAudioSource uiAudioSource)
        {
            return uiAudioSource;
        }
        
        public UiAudioSource Create()
        {
            UiAudioSource uiAudioSource = CreateView(AudioSourceConst.PrefabPath);
            
            return uiAudioSource;
        }
    }
}