using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace SGGApp.Api.Custom
{
    public class CustomDocs : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller == null)
            {
                return;
            }

            foreach (object attrib in controller.Attributes)
            {
                if (attrib.GetType() == typeof(RouteAttribute))
                {
                    RouteAttribute route = (RouteAttribute)attrib;
                    if (string.IsNullOrEmpty(route.Name) == false)
                    {
                        controller.ControllerName = route.Name;
                    }
                }
            }
        }
    }
}
