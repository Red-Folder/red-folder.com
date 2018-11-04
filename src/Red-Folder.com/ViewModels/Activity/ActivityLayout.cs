using Red_Folder.com.Models.Activity;
using System;

namespace Red_Folder.com.ViewModels.Activity
{
    public class ActivityLayout<T> where T: IActivity
    {
        private T _activity;
        private string _layoutId;
        private Func<T, bool> _toDisplay;

        public ActivityLayout(T activity, string layoutId, Func<T, bool> toDisplay)
        {
            _activity = activity;
            _layoutId = layoutId;
            _toDisplay = toDisplay;
        }

        public T Activity
        {
            get => _activity;
        }

        public bool Display
        {
            get => _toDisplay(_activity);
        }

        public string LayoutId
        {
            get
            {
                return _layoutId;
            }
        }
    }
}
