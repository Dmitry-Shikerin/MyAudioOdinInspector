﻿using System;
using Sirenix.OdinInspector;
using Sources.Core.Domain.Constants;
using Sources.MVPPassiveView.Presentations.Interfaces.PresentationsInterfaces.Views.Constructors;
using Sources.MyAudios.Domain.Dictionaries;
using Sources.MyAudios.Domain.Groups;
using Sources.MyAudios.Presentations.Views.Implementation.Types;
using Sources.Volumes.Domain.Models.Implementation;
using UnityEditor;
using UnityEngine;

namespace Sources.MyAudios.Domain.Configs
{
    [CreateAssetMenu(fileName = "AudioServiceDataBase", menuName = "Configs/AudioServiceDataBase", order = 51)]
    public class AudioServiceDataBase : ScriptableObject, IConstruct<Volume>
    {
        [DisplayAsString(false)] [HideLabel] 
        [SerializeField] private string _labele = UiConstant.AudioServiceDataBaseLabel;

        [BoxGroup("Settings")]
        [SerializeField] private int _poolCount = 3;
        [BoxGroup("Settings")] [Range(0, 1)] [OnValueChanged("ChangeVolume")]
        [SerializeField] private float _volume = 0.5f;

        [TabGroup("Groups")] [Space(10f)]
        [SerializeField] private AudioClipGroupDictionary _audioClipGroups = new AudioClipGroupDictionary();
        
        [Space(10f)]
        [SerializeField] private AudioGroupId _audioGroupId;

        private Volume _volumeModel;

        public AudioClipGroupDictionary AudioGroups => _audioClipGroups;
        public int PoolCount => _poolCount;
        public float Volume => _volume;

        public void Construct(Volume volume) =>
            _volumeModel = volume ?? throw new ArgumentNullException(nameof(volume));
        
        private void ChangeVolume()
        {
            if (_volumeModel == null)
                return;
            
            _volumeModel.MusicVolume = _volume;
            _volumeModel.SoundsVolume = _volume;
        }
        
        public void RemoveGroup(AudioGroup phrase)
        {
            AssetDatabase.RemoveObjectFromAsset(phrase);
            _audioClipGroups.Remove(phrase.Id);
            AssetDatabase.SaveAssets();
        }
        
        [Button(ButtonSizes.Large)]
        private void CreateGroup()
        {
#if UNITY_EDITOR
            if (_audioClipGroups.ContainsKey(_audioGroupId))
                return;
            
            AudioGroup audioGroup = CreateInstance<AudioGroup>();
            
            AssetDatabase.AddObjectToAsset(audioGroup, this);
            AssetDatabase.Refresh();
            
            _audioClipGroups.Add(_audioGroupId, audioGroup);
            audioGroup.SetDataBase(this);
            audioGroup.SetId(_audioGroupId);
            audioGroup.name = _audioGroupId + "_AudioGroup";

            AssetDatabase.SaveAssets();
#endif
        }
    }
}