using System;
namespace CQRSDemo.Fulfillment.Presentation.MQ {
    public interface IMessageQueue<T> {
        void Send(T message);
        bool TryReceive(out T message);
    }
}
