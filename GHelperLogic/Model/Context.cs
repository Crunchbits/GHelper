﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GHelperLogic.Utility;
using NDepend.Path;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NodaTime;

namespace GHelperLogic.Model
{
	/// <summary>
	/// Represents the concept referred to as "Game" within the GHUB app, and as an "application"
	/// within its JSON configuration/state file. A single Context can be associated with one or more Profiles.
	/// </summary>
	public class Context
	{
		[JsonIgnore] public Collection<Profile> Profiles { get; } = new Collection<Profile>();

		[JsonIgnore]
		public Guid? ID
		{
			get => this.ApplicationID;
		}

		[JsonConverter(typeof(PathJSONConverter))]
		[JsonProperty("applicationFolder")]
		public IPath? ApplicationFolder { get; set; }
		
		[JsonProperty("applicationId")]
		public Guid? ApplicationID { get; set; }
		
		[JsonConverter(typeof(PathJSONConverter))]
		[JsonProperty("applicationPath")]
		public IPath? ApplicationPath { get; set; }
		
		[JsonProperty("categoryColors")]
		public CategoryColor[]? CategoryColors { get; set; }
		
		[JsonProperty("commands")]
		public Command[]? Commands { get; set; }
		
		[JsonProperty("databaseId")]
		public Guid? DatabaseID { get; set; }
		
		[JsonProperty("isInstalled")]
		public Boolean? IsInstalled { get; set; }
		
		[JsonConverter(typeof(DateTimeJSONConverter))]
		[JsonProperty("lastRunTime")]
		public LocalDateTime? LastRunTime { get; set; }
		
		[JsonProperty("name")]
		public string? Name { get; set; }
		
		[JsonProperty("posterUrl")]
		public Uri? PosterURL { get; set; }
		
		[JsonProperty("profileUrl")]
		public Uri? ProfileURL { get; set; }
		
		[JsonProperty("version")]
		public UInt16? Version { get; set; }
		
		[JsonExtensionData]
		public IDictionary<string, JToken>? AdditionalData { get; set; }
	}

	public static class ContextExtensions
	{
		public static Context? GetByID(this IEnumerable<Context> contexts, Guid? id)
		{
			foreach (Context context in contexts)
			{
				if ((context.ID is { } contextID) && (contextID == id))
				{
					return context;
				}
			}

			return null;
		}
	} 

}