﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebAppProject.Startup))]
namespace WebAppProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
