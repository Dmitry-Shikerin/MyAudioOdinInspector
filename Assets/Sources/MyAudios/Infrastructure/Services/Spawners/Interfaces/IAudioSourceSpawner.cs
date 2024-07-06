using Sources.MyAudios.Presentations.Views.Implementation;

namespace Sources.MyAudios.Infrastructure.Services.Spawners.Interfaces
{
    public interface IAudioSourceSpawner
    {
        UiAudioSource Spawn();
    }
}