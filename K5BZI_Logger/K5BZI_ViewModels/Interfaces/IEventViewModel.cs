﻿using K5BZI_Models.ViewModelModels;

namespace K5BZI_ViewModels.Interfaces
{
    public interface IEventViewModel
    {
        SelectEventModel Model { get; }

        EditEventModel EditModel { get; }
    }
}
