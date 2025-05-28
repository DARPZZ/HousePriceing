using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace HousePriceing.Helpers
{
    public class GenericValueChangedMessage<T> : ValueChangedMessage<T>
    {
        public GenericValueChangedMessage(T value) : base(value) { }
    }
}
