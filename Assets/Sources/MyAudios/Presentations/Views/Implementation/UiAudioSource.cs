using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sources.Core.Domain.Constants;
using Sources.MVPPassiveView.Presentations.Implementation.Views;
using Sources.MyAudios.Domain.Groups;
using Sources.MyAudios.Presentations.Views.Implementation.Types;
using Sources.MyAudios.Presentations.Views.Interfaces;
using Sources.ObjectPools.Implementation.Destroyers;
using Sources.ObjectPools.Interfaces.Destroyers;
using UnityEngine;

namespace Sources.MyAudios.Presentations.Views.Implementation
{
    [RequireComponent(typeof(AudioSource))]
    public class UiAudioSource : View, IUiAudioSource
    {
        [DisplayAsString(false)] [HideLabel] 
        [SerializeField] private string _lebel = UiConstant.UiAudioSourceLabel;
        [SerializeField] private AudioSourceId _audioSourceId;
        
        private float _currentTime;
        
        private IPODestroyerService _destroyerService = new PODestroyerService();
        private AudioSource _audioSource;
        private CancellationTokenSource _cancellationTokenSource;
        
        public AudioSourceId AudioSourceId => _audioSourceId;
        public bool IsPlaying => _audioSource.isPlaying;

        private void Awake() =>
            _audioSource = GetComponent<AudioSource>();

        private void OnEnable() =>
            _cancellationTokenSource = new CancellationTokenSource();

        private void OnDisable() =>
            _cancellationTokenSource.Cancel();

        public override void Destroy() =>
            _destroyerService.Destroy(this);

        public async UniTask PlayAsync(Action callback, AudioGroup audioGroup = null)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            
            if (_audioSource == null)
                return;
            
            try
            {
                _audioSource.Play();

                while (CanPlay() == false && _cancellationTokenSource.Token.IsCancellationRequested == false)
                {
                    audioGroup?.SetCurrentTime(_audioSource.time);
                    await UniTask.Yield();
                }
                
                callback?.Invoke();
            }
            catch (OperationCanceledException)
            {
            }
        }
        
        public void StopPlayAsync() =>
            _cancellationTokenSource.Cancel();

        public void Play() =>
            _audioSource.Play();

        public IUiAudioSource SetVolume(float volume)
        {
            _audioSource.volume = volume;

            return this;
        }

        public IUiAudioSource SetClip(AudioClip clip)
        {
            if (_audioSource == null)
                return null;
            
            _audioSource.clip = clip;
            
            return this;
        }

        public void Pause()
        {
            _currentTime = _audioSource.time;
            _audioSource.Pause();
        }

        public void UnPause()
        {
            _audioSource.time = _currentTime;
            _audioSource.UnPause();
        }

        private bool CanPlay() =>
            _audioSource != null && _audioSource.clip.length <= _audioSource.time + 0.025f;
    }
}