using System;
using Nancy.Hosting.Self;
using Nancy;

namespace ConsoleLauncher
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var url = "http://localhost:8081";
			var host = new NancyHost(new Uri(url));
			host.Start();
			Console.WriteLine ("Server now running on: " + url);
			Console.ReadLine ();
			host.Stop();
		}
	}
}
