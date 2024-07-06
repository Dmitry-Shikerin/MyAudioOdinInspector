using System;
using Sirenix.OdinInspector;
using Sources.MyAudios.Domain.Groups;
using Sources.MyAudios.Presentations.Views.Implementation.Types;
using Sources.Utills.Dictionaries;

namespace Sources.MyAudios.Domain.Dictionaries
{
    [Serializable] [DictionaryDrawerSettings(KeyLabel = "Id",ValueLabel = "AudioGroup")]
    public class AudioClipGroupDictionary : SerializedDictionary<AudioGroupId, AudioGroup>
    {
    }
}