using PropertyChanged;

namespace K5BZI_Models.EntityModels
{
    [AddINotifyPropertyChangedInterface]
    public class TextMessage
    {

        public string Message { get; set; }
    }
}
