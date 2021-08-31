#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using System.Linq;
using System.Reflection;
using Autofac;
using FluentValidation;
using MediatR;
using Product.Application.Services.Commands;
using Product.Application.Services.DomainEventHandlers;
using Product.Application.Services.Validations;

namespace Product.Application.Services.AutofacModules
{
    public class MediatorModule : Utility.Infrastructure.AutofacModules.MediatorModule
    {
        protected override void DefaultLoad(ContainerBuilder builder)
        { 
            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(NumberChanngeCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            builder.RegisterAssemblyTypes(typeof(NumberChangeDomainEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));

            // Register the Command's Validators (Validators based on FluentValidation library)
            builder
                .RegisterAssemblyTypes(typeof(NumberChanngeCommandValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();
            base.DefaultLoad(builder);
        }
    }
}
#endif