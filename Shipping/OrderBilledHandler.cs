using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Shipping
{
    public class OrderBilledHandler :
        IHandleMessages<OrderBilled>
    {
        static ILog log = LogManager.GetLogger<OrderBilledHandler>();
        /// <summary>
        /// 处理OrderBilled的下消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            log.Info($"接收到票据, OrderId = {message.OrderId} - 我们现在邮递吗?");
            return Task.CompletedTask;
        }
    }
}