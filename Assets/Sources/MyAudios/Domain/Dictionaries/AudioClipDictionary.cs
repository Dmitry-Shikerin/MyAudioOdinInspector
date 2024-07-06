using System;
using Sirenix.OdinInspector;
using Sources.MyAudios.Presentations.Views.Implementation.Types;
using Sources.Utills.Dictionaries;
using UnityEngine;

namespace Sources.MyAudios.Domain.Dictionaries
{
    [Serializable] [DictionaryDrawerSettings(KeyLabel = "Id",ValueLabel = "AudioClip")]
    public class AudioClipDictionary : SerializedDictionary<AudioClipId, AudioClip>
    {
    }
}