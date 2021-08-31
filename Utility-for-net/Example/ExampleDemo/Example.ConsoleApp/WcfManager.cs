using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wcf
{
    [ServiceContract]
    public interface IWcfService
    {
        [OperationContract]
        [System.ServiceModel.Web.WebGet(RequestFormat = System.ServiceModel.Web.WebMessageFormat.Json, ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        int Add();
    }
    public class WcfService : IWcfService
    {
        int i = 0;
        public int Add()
        {
            System.ServiceModel.OperationContext.Current.GetCallbackChannel<IWcfService>();
            Interlocked.Increment(ref i);
            return i;
        }
    }
    public class WcfHost : IDisposable
    {
        private ServiceHost _serviceHost;
        public const string BaseAddress = "net.pipe://localhost";
        private const string ServiceAddress = "wcf";
        private static readonly Type ServiceType = typeof(WcfService);
        private static readonly Type ContractType = typeof(IWcfService);
        private readonly Binding WcfBinding = new NetNamedPipeBinding();
        public WcfHost()
        {
            this._serviceHost = new ServiceHost(ServiceType, new Uri[] { new Uri(WcfHost.BaseAddress) });
            this._serviceHost.AddServiceEndpoint(ContractType, WcfBinding, ServiceAddress);
        }
        public void Open()
        {
            Console.WriteLine("服务开始启动");
            this._serviceHost.Open();
            Console.WriteLine("服务已启动");
        }
        public void Dispose()
        {
            if (_serviceHost != null)
            {
                ((IDisposable)_serviceHost).Dispose();
                Console.WriteLine("服务已停止");
            }
        }
    }
    [ServiceContract]
    public interface IProxyService
    {
        [OperationContract]
        int Add();
    }
    public class WcfProxy : ClientBase<IWcfService>, IProxyService
    {
        private readonly static Binding WcfBinding = new NetNamedPipeBinding();
        public WcfProxy() : base(WcfBinding, new EndpointAddress(WcfHost.BaseAddress + "/wcf"))
        //base(WcfHost.WcfBinding, new EndpointAddress(WcfHost.BaseAddress+"/wcf"))
        {
        }
        public int Add()
        {
            return Channel.Add();
        }
    }


    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        [FaultContract(typeof(ServiceExceptionInfo))]
        string GetData(int value);
    }

    public class MyService : IMyService
    {
        public string GetData(int value)
        {
            var ex = new InvalidOperationException("Invalid operation...");
            throw new FaultException<ServiceExceptionInfo>(new ServiceExceptionInfo(ex));
        }
    }

    [DataContract]
    public class ServiceExceptionInfo
    {
        [DataMember]
        public string ExceptionType { get; set; }

        [DataMember]
        public string Message { get; set; }
        public ServiceExceptionInfo(Exception ex)
        {
            this.ExceptionType = ex.GetType().AssemblyQualifiedName;
            this.Message = ex.Message;
        }
    }

    public class MyServiceHost : ServiceHost
    {
        public MyServiceHost(Type serviceType, params Uri[] baseAddresses) : base(serviceType, baseAddresses)
        { }

        protected override void OnOpening()
        {
            base.OnOpening();
            foreach (var endpoint in this.Description.Endpoints)
            {
                string ns = endpoint.Contract.Namespace.TrimEnd('/');
                foreach (var op in endpoint.Contract.Operations)
                {
                    if (!op.Faults.Any(it => it.DetailType == typeof(ServiceExceptionInfo)))
                    {
                        FaultDescription fault = new FaultDescription($"{ns}/{op.Name}_ServiceExceptionInfo");
                        fault.DetailType = typeof(ServiceExceptionInfo);

                        op.Faults.Add(fault);
                    }
                }
            }
        }
    }

    public class MyServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new MyServiceHost(serviceType, baseAddresses);
        }
    }

}
