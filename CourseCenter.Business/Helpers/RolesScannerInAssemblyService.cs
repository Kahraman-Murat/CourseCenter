using CourseCenter.DTO.DTOs.RoleDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CourseCenter.Business.Helpers
{
    public class RolesScannerInAssemblyService
    {
        public List<InfoControllerDto> GetAllControllersAndActionsWithAuthorizeRoles()
        {
            var controllerList = new List<InfoControllerDto>();

            // Tüm assembly'leri tara
            var controllers = Assembly.GetExecutingAssembly()
                                      .GetTypes()
                                      .Where(type => typeof(Controller).IsAssignableFrom(type)); // Controller türetilmiş tüm sınıflar

            foreach (var controller in controllers)
            {
                var controllerInfo = new InfoControllerDto
                {
                    ControllerName = controller.Name.Replace("Controller", ""), // Controller ismi
                    Actions = new List<InfoActionDto>()
                };

                // Controller seviyesinde Authorize attribute varsa alalım
                var controllerAuthorizeAttributes = controller.GetCustomAttributes<AuthorizeAttribute>(true).ToList();
                string[] controllerRoles = controllerAuthorizeAttributes.SelectMany(a => a.Roles?.Split(',') ?? new string[] { }).ToArray();

                // Action seviyesindeki methodları tara
                var actions = controller.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                                        .Where(m => m.IsPublic && !m.IsDefined(typeof(NonActionAttribute)));

                foreach (var action in actions)
                {
                    var actionInfo = new InfoActionDto
                    {
                        ActionName = action.Name,
                        AuthorizeRoles = new List<string>()
                    };

                    // Action'a ait Authorize attribute'leri bul
                    var authorizeAttributes = action.GetCustomAttributes<AuthorizeAttribute>(true).ToList();

                    // Eğer action'da Authorize yoksa controller'daki Authorize'a bakarız
                    if (authorizeAttributes.Any())
                    {
                        actionInfo.AuthorizeRoles.AddRange(authorizeAttributes.SelectMany(a => a.Roles?.Split(',') ?? new string[] { }));
                    }
                    else if (controllerRoles.Any()) // Action'da yoksa Controller seviyesindeki rolleri al
                    {
                        actionInfo.AuthorizeRoles.AddRange(controllerRoles);
                    }

                    controllerInfo.Actions.Add(actionInfo);
                }

                controllerList.Add(controllerInfo);
            }

            return controllerList;
        }

        public List<string> GetAllDefinedRoles(Assembly assembly)
        {
            // Tüm rolleri benzersiz olarak HashSet te sakla
            HashSet<string> roles = new HashSet<string>();

            // Tüm controller'ları tara
            var controllers = assembly.GetTypes().Where(type => typeof(ControllerBase).IsAssignableFrom(type));
            foreach (var controller in controllers)
            {
                // Controller seviyesindeki [Authorize] attribute'ünü al
                var controllerAuthorizeAttributes = controller.GetCustomAttributes<AuthorizeAttribute>(true);
                foreach (var attr in controllerAuthorizeAttributes)
                    if (!string.IsNullOrEmpty(attr.Roles))
                        // Rolleri virgülle ayrılmış olarak al ve HashSet'e ekle
                        foreach (var role in attr.Roles.Split(','))
                            roles.Add(role.Trim());

                // Her bir action'ı tara
                var actions = controller.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
                foreach (var action in actions)
                {
                    // Action seviyesindeki [Authorize] attribute'ünü al
                    var actionAuthorizeAttributes = action.GetCustomAttributes<AuthorizeAttribute>(true);
                    foreach (var attr in actionAuthorizeAttributes)
                        if (!string.IsNullOrEmpty(attr.Roles))
                            // Rolleri virgülle ayrılmış olarak al ve HashSet'e ekle
                            foreach (var role in attr.Roles.Split(','))
                                roles.Add(role.Trim());
                }
            }

            // HashSet'i List'e çevir benzersiz rolleri döndür
            return roles.ToList();
        }
    }
}
