using System;
using System.Diagnostics;
using System.Reflection;
using PostSharp.Aspects;

namespace Orcamento.InfraStructure
{
    [Serializable]
    public sealed class CatchExceptionAttribute : OnExceptionAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            Guid guid = Guid.NewGuid();

            var logger = log4net.LogManager.GetLogger("LogInFile");

            logger.Error(args.Exception.Message);

            args.Exception =
                new BusinessException(string.Format("{0} {1} ",
                                                      guid, args.Exception.Message), args.Exception.InnerException);
            args.FlowBehavior = FlowBehavior.ThrowException;
        }
    }
}