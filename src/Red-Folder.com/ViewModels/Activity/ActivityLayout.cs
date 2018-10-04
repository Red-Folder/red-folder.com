using Red_Folder.com.Models.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Red_Folder.com.ViewModels.Activity
{
    public class ActivityLayout<T> where T: IActivity
    {
        private T _activity;
        private string _layoutId;

        public ActivityLayout(T activity, string layoutId)
        {
            _activity = activity;
            _layoutId = layoutId;
        }

        public T Activity
        {
            get => _activity;
        }

        public bool Display
        {
            // TODO ... need to defer to the activity
            get => true;
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
