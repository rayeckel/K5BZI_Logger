using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using K5BZI_Models.Base;
using K5BZI_Models.EntityModels;
using Prism.Commands;
using PropertyChanged;

namespace K5BZI_Models.ViewModelModels
{
    [AddINotifyPropertyChangedInterface]
    public class NetworkModel : BaseViewModel
    {
        public NetworkModel()
        {
            SearchingNetworkVisibility = Visibility.Collapsed;
            SendMessageVisibility = Visibility.Visible;
            TextMessage = new TextMessage();
            TextMessages = new ObservableCollection<TextMessage>();
            NetworkAddresses = new ObservableCollection<HostData>();
        }

        #region Properties

        public HostData NetworkData { get; set; }

        public TextMessage TextMessage { get; set; }

        public ObservableCollection<TextMessage> TextMessages { get; set; }

        public ObservableCollection<HostData> NetworkAddresses { get; set; }

        public HostData ActiveHost { get; set; }

        public Visibility SearchingNetworkVisibility { get; set; }

        public Visibility RescanNetworkVisibility { get; set; }

        public Visibility SendMessageVisibility { get; set; }

        #endregion

        #region Commands

        private ICommand _sendMessageCommand;
        public ICommand SendMessageCommand
        {

            get
            {
                return _sendMessageCommand ?? (_sendMessageCommand =
                    new DelegateCommand(SendMessageAction));
            }
        }

        public Action SendMessageAction { get; set; }

        private ICommand _cancelSearchCommand;
        public ICommand CancelSearchCommand
        {

            get
            {
                return _cancelSearchCommand ?? (_cancelSearchCommand =
                    new DelegateCommand(CancelSearchAction));
            }
        }

        public Action CancelSearchAction { get; set; }

        private ICommand _rescanCommand;
        public ICommand RescanCommand
        {

            get
            {
                return _rescanCommand ?? (_rescanCommand =
                    new DelegateCommand(RescanAction));
            }
        }

        public Action RescanAction { get; set; }

        private ICommand _editMessageCommand;
        public ICommand EditMessageCommand
        {

            get
            {
                return _editMessageCommand ?? (_editMessageCommand =
                    new DelegateCommand(EditMessageAction));
            }
        }

        public Action EditMessageAction { get; set; }

        #endregion
    }
}
