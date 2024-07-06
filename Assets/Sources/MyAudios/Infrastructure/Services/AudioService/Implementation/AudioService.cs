using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Sources.Collectors;
using Sources.MyAudios.Domain.Configs;
using Sources.MyAudios.Domain.Groups;
using Sources.MyAudios.Infrastructure.Factories.Implementation;
using Sources.MyAudios.Infrastructure.Factories.Interfaces;
using Sources.MyAudios.Infrastructure.Services.AudioService.Interfaces;
using Sources.MyAudios.Infrastructure.Services.Spawners.Implementation;
using Sources.MyAudios.Infrastructure.Services.Spawners.Interfaces;
using Sources.MyAudios.Presentations.Views.Implementation;
using Sources.MyAudios.Presentations.Views.Implementation.Types;
using Sources.MyAudios.Presentations.Views.Interfaces;
using Sources.ObjectPools.Implementation;
using Sources.Volumes.Domain.Models.Implementation;
using UnityEngine;

namespace Sources.MyAudios.Infrastructure.Services.AudioService.Implementation
{
    public class AudioService : IAudioService
    {
        private readonly AudioServiceDataBase _audioServiceDataBase;
        private readonly Dictionary<AudioSourceId, IUiAudioSource> _audioSources;
        private readonly Dictionary<AudioGroupId, AudioGroup> _audioGroups;
        private readonly ObjectPool<UiAudioSource> _audioSourcePool;
        private readonly IAudioSourceSpawner _audioSourceSpawner;

        private Volume _volume;
        private CancellationTokenSource _audioCancellationTokenSource;

        public AudioService(
            UiCollector uiCollector,
            AudioServiceDataBase audioServiceDataBase)
        {
            _audioServiceDataBase = audioServiceDataBase ??
                                    throw new ArgumentNullException(nameof(audioServiceDataBase));

            _audioSources = uiCollector.UiAudioSources.ToDictionary(
                uiAudioSource => uiAudioSource.AudioSourceId, uiAudioSource => uiAudioSource);

            _audioGroups = audioServiceDataBase.AudioGroups;
            _audioSourcePool = new ObjectPool<UiAudioSource>();
            _audioSourcePool.SetPoolCount(_audioServiceDataBase.PoolCount);
            IAudioContainerFactory audioContainerFactory = new AudioContainerFactory(_audioSourcePool);
            _audioSourceSpawner = new AudioSourceSpawner(_audioSourcePool, audioContainerFactory);
        }

        public void Construct(Volume volume) =>
            _volume = volume ?? throw new ArgumentNullException(nameof(volume));

        public void Initialize()
        {
            if (_volume == null)
                throw new NullReferenceException(nameof(_volume));

            _audioCancellationTokenSource = new CancellationTokenSource();
            ClearStates();
            OnVolumeChanged();
            _audioServiceDataBase.Construct(_volume);
            _volume.MusicVolumeChanged += OnVolumeChanged;
        }

        public void Destroy()
        {
            _volume.MusicVolumeChanged -= OnVolumeChanged;
            _audioCancellationTokenSource.Cancel();
            ClearStates();
        }

        public void Play(AudioSourceId id)
        {
            if (_audioSources.ContainsKey(id) == false)
                throw new KeyNotFoundException(id.ToString());

            _audioSources[id].Play();
        }

        public IUiAudioSource Play(AudioGroupId audioGroupId)
        {
            UiAudioSource audioSource = _audioSourceSpawner.Spawn();
            audioSource.SetVolume(_volume.MusicVolume);

            if (_audioGroups.ContainsKey(audioGroupId) == false)
                throw new KeyNotFoundException(audioGroupId.ToString());

            return audioSource;
        }

        public async void PlayAsync(AudioGroupId audioGroupId)
        {
            if (_audioGroups.ContainsKey(audioGroupId) == false)
                throw new KeyNotFoundException(audioGroupId.ToString());

            if (_audioGroups[audioGroupId].IsPlaying)
                throw new InvalidOperationException($"Group {audioGroupId} is already playing");

            IUiAudioSource audioSource = _audioSourceSpawner.Spawn();
            audioSource.SetVolume(_volume.MusicVolume);
            _audioGroups[audioGroupId].Play();

            try
            {
                while (_audioCancellationTokenSource.Token.IsCancellationRequested == false &&
                       _audioGroups[audioGroupId].IsPlaying)
                {
                    foreach (AudioClip audioClip in _audioGroups[audioGroupId].AudioClips)
                    {
                        audioSource.SetClip(audioClip);
                        _audioGroups[audioGroupId].SetCurrentClip(audioClip);
                        await audioSource.PlayAsync(audioGroup: _audioGroups[audioGroupId]);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                audioSource.StopPlayAsync();
            }
        }

        public void Stop(AudioGroupId audioGroupId)
        {
            if (_audioGroups.ContainsKey(audioGroupId) == false)
                throw new KeyNotFoundException(audioGroupId.ToString());

            if (_audioGroups[audioGroupId].IsPlaying == false)
                return;

            _audioGroups[audioGroupId].Stop();
        }

        private void ClearStates()
        {
            foreach (AudioGroup audioGroup in _audioGroups.Values)
            {
                audioGroup.Stop();
                audioGroup.Destroy();
            }
        }

        private void OnVolumeChanged() =>
            ChangeVolume(_volume.MusicVolume);

        private void ChangeVolume(float volume)
        {
            foreach (IUiAudioSource audioSource in _audioSources.Values)
                audioSource.SetVolume(volume);

            foreach (UiAudioSource audioSource in _audioSourcePool.Collection)
                audioSource.SetVolume(volume);
        }
    }
}