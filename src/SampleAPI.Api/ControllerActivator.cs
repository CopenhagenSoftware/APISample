using CopenhagenSoftware.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace CopenhagenSoftware.Api
{
    ///<summary>
    ///This class is responsible for creating an instance of a controller in response to a
    ///request. This allows us to manually specify how any dependencies are resolved for each
    ///controller and provides a compiler error if we fail to configure one.
    ///It takes an <c>IServiceProvider</c> parameter to allow access to any types
    ///that are automatically registered with the built in container.
    ///</summary>
    public sealed class ControllerActivator : IControllerActivator
    {
        public IServiceProvider Services { get; }

        public ControllerActivator(IServiceProvider services)
        {
            Services = services;
        }

        public object Create(ControllerContext context) =>
            CreateController(context.ActionDescriptor.ControllerTypeInfo as Type);

        public object CreateController(Type type)
        {
            switch (type.Name)
            {
                case nameof(SampleController):
                    return new SampleController(Services.GetRequiredService<ILogger<SampleController>>());
                default:
                    throw new Exception($"Can't create a {type.Name}");
            }
        }

        public void Release(ControllerContext context, object controller) { }
    }
}