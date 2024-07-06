using Sources.MyAudios.Presentations.Views.Implementation;

namespace Sources.MyAudios.Infrastructure.Factories.Interfaces
{
    public interface IAudioContainerFactory
    {
        UiAudioSource Create(UiAudioSource uiAudioSource);

        UiAudioSource Create();
    }
}