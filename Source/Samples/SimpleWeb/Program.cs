﻿using System.IO;
using System.Security.Claims;
using System.Security.Principal;
using Bifrost.Security;
using Microsoft.AspNetCore.Hosting;

namespace SimpleWeb
{
   public class PrincipalResolver : ICanResolvePrincipal
    {
        public IPrincipal Resolve()
        {
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim("Name", "Unknown"));
            var principal = new ClaimsPrincipal(identity);

            return principal;
        }
    }    
    public class Program
    {
        public static void Main(string[] args)
        {
            GenericPrincipal.ClaimsPrincipalSelector = () =>
             {
                 var identity = new ClaimsIdentity();
                 identity.AddClaim(new Claim("Name", "Unknown"));
                 var principal = new ClaimsPrincipal(identity);
                 return principal;
             };

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}