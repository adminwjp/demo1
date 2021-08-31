using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Config.GrpcService
{
    public class ConfigService : Config.ConfigBase
    {

        private readonly ILogger<ConfigService> _logger;
        public ConfigService(ILogger<ConfigService> logger)
        {
            _logger = logger;
        }

        public override Task<ResultReply> Register(ServiceRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ResultReply
            {
                Node = "success",
                Code = 200,
                Success = true
            });
        }

        public override Task<AddressReply> Get(NameRequest request, ServerCallContext context)
        {
            return Task.FromResult(new AddressReply
            {
                Address = ""
            });
        }

        public override Task<AddressReply> GetAddress(ServiceRequest request, ServerCallContext context)
        {
            return Task.FromResult(new AddressReply
            {
                Address = ""
            });
        }

        public override Task<ServiceReply> GetAll(NoceRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ServiceReply
            {

            });
        }

    }
}
