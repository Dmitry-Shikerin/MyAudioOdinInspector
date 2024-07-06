using System;
using Sources.MyAudios.Infrastructure.Factories.Interfaces;
using Sources.MyAudios.Infrastructure.Services.Spawners.Interfaces;
using Sources.MyAudios.Presentations.Views.Implementation;
using Sources.ObjectPools.Interfaces.Generic;

namespace Sources.MyAudios.Infrastructure.Services.Spawners.Implementation
{
    public class AudioSourceSpawner : IAudioSourceSpawner
    {
        private readonly IObjectPool<UiAudioSource> _audioSourcePool;
        private readonly IAudioContainerFactory _audioContainerFactory;

        public AudioSourceSpawner(
            IObjectPool<UiAudioSource> audioSourcePool, 
            IAudioContainerFactory audioContainerFactory)
        {
            _audioSourcePool = audioSourcePool ?? throw new ArgumentNullException(nameof(audioSourcePool));
            _audioContainerFactory = audioContainerFactory ??
                                     throw new ArgumentNullException(nameof(audioContainerFactory));
        }

        public UiAudioSource Spawn()
        {
            UiAudioSource uiAudioSource = SpawnFromPool() ?? _audioContainerFactory.Create();
            uiAudioSource.Show();
            
            return uiAudioSource;
        }
        
        private UiAudioSource SpawnFromPool()
        {
            UiAudioSource uiAudioSource = _audioSourcePool.Get<UiAudioSource>();

            if (uiAudioSource == null)
                return null;
            
            return _audioContainerFactory.Create(uiAudioSource);
        }
    }
}