using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Sales
{
    public class PlaceOrderHandler :
        IHandleMessages<PlaceOrder>
    {
        static ILog log = LogManager.GetLogger<PlaceOrderHandler>();
        /// <summary>
        /// 处理PlaceOrder的消息 然后推送到 context
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            log.Info($"接收到订单, OrderId = {message.OrderId}");

            // This is normally where some business logic would occur

            // Uncomment to test throwing exceptions
            //throw new Exception("BOOM");

            var orderPlaced = new OrderPlaced
            {
                OrderId = message.OrderId
            };
            return context.Publish(orderPlaced);
        }
    }
}