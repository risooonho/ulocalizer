﻿using System.ComponentModel;
using ULocalizer.Converters;

namespace ULocalizer.Classes
{
    [TypeConverter(typeof (CCulterConverter))]
    public class CCulture : CNotify
    {
        private string _displayName = string.Empty;
        private string _iso = string.Empty;
        private CCulture _parent;

        public bool IsNeutral
        {
            get { return (Parent == null); }
        }

        public CCulture Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                NotifyPropertyChanged();
            }
        }

        public string ISO
        {
            get { return _iso; }
            set
            {
                _iso = value;
                NotifyPropertyChanged();
            }
        }

        
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                NotifyPropertyChanged();
            }
        }

        public override string ToString()
        {
            return ISO;
        }
    }
}