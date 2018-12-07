using ClearChoice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClearChoice.Filters
{
	public class AutenticacaoTipo
	{

		public class AutorizacaoTipo : AuthorizeAttribute
		{
			private TiposUsuarios[] tiposAutorizados;

			public AutorizacaoTipo(TiposUsuarios[] tiposUsuarioAutorizados)
			{
				tiposAutorizados = tiposUsuarioAutorizados;
			}

			public override void OnAuthorization(AuthorizationContext filterContext)
			{
				bool autorizado = tiposAutorizados.Any(t => filterContext.HttpContext.User.IsInRole(t.ToString()));

				if (!autorizado)
				{
					filterContext.Controller.TempData["ErroAutorizacao"] = "Você não tem permissão para acessar essa página";

					filterContext.Result = new RedirectResult("Main");
				}
			}
		}
	}
}