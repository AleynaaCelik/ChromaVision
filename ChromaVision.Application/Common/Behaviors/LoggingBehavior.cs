using ChromaVision.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : notnull
    {
        private readonly IAppLogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(IAppLogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogInformation($"Handling {requestName}");

            var stopwatch = Stopwatch.StartNew();
            var response = await next();
            stopwatch.Stop();

            _logger.LogInformation($"Handled {requestName} in {stopwatch.ElapsedMilliseconds}ms");

            return response;
        }
    }
}
