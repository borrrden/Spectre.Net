using Spectre.Net.Api;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Spectre.Models
{
    public sealed class UserDefaults : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public static readonly UserDefaults Current = new UserDefaults();

        private SpectreAlgorithmVersion _algorithmVersion = SpectreAlgorithmVersion.Current;
        private SpectreResultType _defaultPasswordType = SpectreResultType.Long;
        private bool _hiddenPasswords;

        public SpectreAlgorithmVersion AlgorithmVersion
        {
            get => _algorithmVersion;
            set
            {
                if (_algorithmVersion != value)
                {
                    _algorithmVersion = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool HiddenPasswords
        {
            get => _hiddenPasswords;
            set
            {
                if (_hiddenPasswords != value)
                {
                    _hiddenPasswords = value;
                    OnPropertyChanged();
                }
            }
        }

        public SpectreResultType DefaultPasswordType
        {
            get => _defaultPasswordType;
            set
            {
                if (_defaultPasswordType != value)
                {
                    _defaultPasswordType= value;
                    OnPropertyChanged();
                }
            } 
        } 

        private UserDefaults() { }

        public void ReadDefaults(UserData userData)
        {
            _algorithmVersion = (SpectreAlgorithmVersion)userData.Algorithm;
            _hiddenPasswords = userData.HidePasswords;
            _defaultPasswordType = (SpectreResultType)userData.DefaultType;
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
