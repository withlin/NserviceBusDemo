using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Billing
{
    public class OrderPlacedHandler :
        IHandleMessages<OrderPlaced>
    {
        static ILog log = LogManager.GetLogger<OrderPlacedHandler>();
        //处理订单的消息
        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            log.Info($"接收到 OrderPlaced, OrderId为 = {message.OrderId} - 正在刷卡...");

            var orderBilled = new OrderBilled
            {
                OrderId = message.OrderId
            };
            return context.Publish(orderBilled);
        }
    }
}