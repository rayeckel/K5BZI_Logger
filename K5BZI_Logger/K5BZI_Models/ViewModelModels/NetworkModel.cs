using System;
using System.Collections.ObjectModel;
using System.Net;
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
            TextMessage = new TextMessage();
            TextMessages = new ObservableCollection<TextMessage>();
            NetworkAddresses = new ObservableCollection<IPAddress>();
        }

        #region Properties

        public TextMessage TextMessage { get; set; }

        public ObservableCollection<TextMessage> TextMessages { get; set; }

        public ObservableCollection<IPAddress> NetworkAddresses { get; set; }

        public IPAddress ActiveAddress { get; set; }

        #endregion

        #region Commands

        private ICommand _sendMessageCommand;
        public ICommand SendMessageCommand
        {

            get
            {
                return _sendMessageCommand ?? (_sendMessageCommand =
                    new DelegateCommand<object>(SendMessageAction, _ => { return true; }));
            }
        }

        public Action<object> SendMessageAction { get; set; }

        #endregion
    }
}
