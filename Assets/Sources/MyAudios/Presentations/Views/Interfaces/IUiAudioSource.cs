using System;
using Cysharp.Threading.Tasks;
using Sources.MVPPassiveView.Presentations.Interfaces.PresentationsInterfaces.Views;
using Sources.MyAudios.Domain.Groups;
using Sources.MyAudios.Presentations.Views.Implementation.Types;
using UnityEngine;

namespace Sources.MyAudios.Presentations.Views.Interfaces
{
    public interface IUiAudioSource : IView
    {
        AudioSourceId AudioSourceId { get; }
        bool IsPlaying { get; }

        UniTask PlayAsync(Action callback = null, AudioGroup audioGroup = null);
        void StopPlayAsync();
        void Play();
        IUiAudioSource SetVolume(float volume);
        IUiAudioSource SetClip(AudioClip clip);
        void Pause();
        void UnPause();
    }
}