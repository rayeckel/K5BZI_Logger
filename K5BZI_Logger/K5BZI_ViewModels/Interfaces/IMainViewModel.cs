﻿using K5BZI_Models;
using K5BZI_Models.Main;

namespace K5BZI_ViewModels.Interfaces
{
    public interface IMainViewModel
    {
        MainModel Model { get; }

        void SelectEvent(Event selectedEvent);

        void CreateNewLog(Event newEvent);

        void UpdateCurrentOperator(Operator currentOperator);
    }
}
