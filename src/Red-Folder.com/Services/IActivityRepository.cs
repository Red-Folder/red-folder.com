﻿using Red_Folder.com.Models.Activity;

namespace Red_Folder.com.Services
{
    public interface IActivityRepository
    {
        Weekly Weekly(int year, int weekNumber);
    }
}