using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using DeliveryCo.Services;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.ServiceLocatorAdapter;
using Microsoft.Practices.ServiceLocation;
using System.Configuration;
using Common;
using System.Messaging; 
using DeliveryCo.Process.SubscriptionService;
using DeliveryCo.Business.Components; 

namespace DeliveryCo.Process
{
    class Program
    {
        private static global::Common.SubscriberServiceHost mHost;
        
        private const String cAddress = "net.msmq://localhost/private/DeliveryCoQueueTransacted";
        private const String cMexAddress = "net.tcp://localhost:9019/DeliveryCoQueueTransacted/mex";

        static void Main(string[] args)
        {
            ResolveDependencies();
            HostSubscriberService();
            SubscribeForEvents();
            HostServices();

        }

        private static void HostServices()
        {
            using (ServiceHost lHost = new ServiceHost(typeof(DeliveryService)))
            {
                lHost.Open();
                Console.WriteLine("Delivery Service started. Press Q to quit");
                while (Console.ReadKey().Key != ConsoleKey.Q) ;
            }
        }


     
        private static void ResolveDependencies()
        {

            UnityContainer lContainer = new UnityContainer();
            UnityConfigurationSection lSection
                    = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            lSection.Containers["containerOne"].Configure(lContainer);
            UnityServiceLocator locator = new UnityServiceLocator(lContainer);
            ServiceLocator.SetLocatorProvider(() => locator);
        }

        private static void HostSubscriberService()
        {
            mHost = new SubscriberServiceHost(typeof(SubscriberService), cAddress, cMexAddress, true, ".\\private$\\DeliveryCoQueueTransacted");
        }

        private static void SubscribeForEvents()
        {
            SubscriptionServiceClient lClient = new SubscriptionServiceClient();
            lClient.Subscribe("DeliveryRequest", cAddress);
        }
    }
}
