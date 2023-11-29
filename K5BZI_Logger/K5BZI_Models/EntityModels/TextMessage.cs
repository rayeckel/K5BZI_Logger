using System;
using PropertyChanged;

namespace K5BZI_Models.EntityModels
{
    [AddINotifyPropertyChangedInterface]
    public class TextMessage
    {
        public TextMessage()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Message { get; set; }

        public string Response { get; set; }

        public void Clear()
        {
            Id = Guid.NewGuid();
            Message = string.Empty;
            Response = string.Empty;
        }

        public TextMessage Clone()
        {
            return new TextMessage
            {
                Id = Id,
                Message = Message,
                Response = Response
            };
        }
    }
}
