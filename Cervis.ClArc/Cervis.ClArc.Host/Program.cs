/*
 * Copyright (c) 2023 Cervis GmbH
 *
 * This file is part of the Cervis.ClArc framework.
 *
 * All rights reserved. Unauthorized copying of this file, via any medium, 
 * is strictly prohibited. Proprietary and confidential.
 */

using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Cervis.ClArc.Host
{
	public class Program
	{
		public static void Main(string[] args)
		{
			IHost host = CreateHostBuilder(args).Build();

			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStaticWebAssets();
					webBuilder.UseStartup<Startup>();
				});
		}
	}
}
