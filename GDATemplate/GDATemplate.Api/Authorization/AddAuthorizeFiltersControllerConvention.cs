using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace GDATemplate.Api.Authorization
{
    public class AddAuthorizeFiltersControllerConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            switch (controller.ControllerName)
            {
                case "Admin":
                    controller.Filters.Add(new AuthorizeFilter("admin"));
                    break;

                default:
                    break;
            }
        }
    }
}
