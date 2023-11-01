﻿using K5BZI_Models;
using K5BZI_Models.ViewModelModels;

namespace K5BZI_ViewModels.Interfaces
{
    public interface IOperatorsViewModel
    {
        OperatorModel OperatorModel { get; }

        EditOperatorModel EditOperator { get; }

        void AddOperator();

        void PopulateEventOperators(Event eventModel);

        void UpdateOperator(Operator operatorObj, bool isEvent);
    }
}
