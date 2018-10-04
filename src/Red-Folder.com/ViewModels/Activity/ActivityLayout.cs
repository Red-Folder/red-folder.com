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

        public ActivityLayout(T activity)
        {
            _activity = activity;
        }

        public T Activity
        {
            get => _activity;
        }

        public bool Display
        {
            get => true;
        }
    }
}
