﻿using System.Collections.Generic;
using UniversalDashboard.Models;
using System.IO;
using System.Reflection;

namespace UniversalDashboard.Services
{
    public abstract class ComponentWriter
	{
		public abstract bool CanWrite(Component component);
		public abstract ComponentParts Write(Component component);

		public string GetResource(string resourcePath)
		{
			var assemblyBasePath = Path.GetDirectoryName(this.GetType().GetTypeInfo().Assembly.Location);

			resourcePath = Path.Combine(assemblyBasePath, "Resources", resourcePath);

			return File.ReadAllText(resourcePath);
		}

    }

	public class ComponentParts
	{
		public string Markup { get; set; }
		public string JavaScript { get; set; }
		public List<string> ScriptIncludes { get; set; } = new List<string>();
		public List<string> StylesheetIncludes { get; set; } = new List<string>();
		public List<Endpoint> Endpoints { get; set; } = new List<Endpoint>();
		public Dictionary<int, string> ElementScripts { get; set; } = new Dictionary<int, string>();

		public void Combine(ComponentParts parts)
		{
			Markup += parts.Markup;
			JavaScript += parts.JavaScript;
			ScriptIncludes.AddRange(parts.ScriptIncludes);
			StylesheetIncludes.AddRange(parts.StylesheetIncludes);
			Endpoints.AddRange(parts.Endpoints);

			foreach(var script in parts.ElementScripts) {
				if (!ElementScripts.ContainsKey(script.Value.GetHashCode()))
					ElementScripts.Add(script.Value.GetHashCode(), script.Value);
			}
		} 
	}
}
