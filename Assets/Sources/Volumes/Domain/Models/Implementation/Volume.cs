using System;
using Sources.Volumes.Domain.Models.Interfaces;

namespace Sources.Volumes.Domain.Models.Implementation
{
    public class Volume : IVolume
    {
        private float _musicVolume = 0.1f;
        private float _soundsVolume = 0.1f;

        public Volume(string id)
        {
            Id = id;
        }

        public event Action MusicVolumeChanged;
        public event Action SoundsVolumeChanged;

        public string Id { get; }
        public Type Type => GetType();

        public float SoundsVolume
        {
            get => _soundsVolume;
            set
            {
                _soundsVolume = value;
                SoundsVolumeChanged?.Invoke();
            }
        }

        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = value;
                MusicVolumeChanged?.Invoke();
            }
        }

        public bool IsSoundsMuted { get; }
        public bool IsMusicMuted { get; }
    }
}