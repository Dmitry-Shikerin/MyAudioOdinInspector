using Sources.MVPPassiveView.Controllers.Interfaces.ControllerLifetimes;
using Sources.MVPPassiveView.Presentations.Interfaces.PresentationsInterfaces.Views.Constructors;
using Sources.MyAudios.Presentations.Views.Implementation.Types;
using Sources.MyAudios.Presentations.Views.Interfaces;
using Sources.Volumes.Domain.Models.Implementation;

namespace Sources.MyAudios.Infrastructure.Services.AudioService.Interfaces
{
    public interface IAudioService : IInitialize, IDestroy, IConstruct<Volume>
    {
        void Play(AudioSourceId id);
        IUiAudioSource Play(AudioGroupId audioClipId);
        void PlayAsync(AudioGroupId audioGroupId);
        void Stop(AudioGroupId audioGroupId);
    }
}