﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Foodie.Web.Startup))]
namespace Foodie.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
