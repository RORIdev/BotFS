﻿using MongoDB.Driver;
using System.Collections.Generic;

namespace BotFS.MongoDB
{
	public class MongoDatabase : IDatabase<MongoProvider, MongoClient>
	{
		public string Name { get; set; }
		public BaseServerProvider<MongoClient> Provider { get; set; }
		public int Size { get; set; }
		public List<string> Tables { get; set; }

		public DBResponse<bool> Drop()
		{
			var response = new DBResponse<bool>
			{
				HasValue = false
			};
			if (Provider.Databases == null) Provider.Refresh();
			if (Provider.Databases.Contains(Name))
			{
				Provider.Server.DropDatabase(Name);
				response.HasValue = true;
				response.HasValue = true;
			}
			return response;
		}

		public DBResponse<List<string>> Refresh()
		{
			var response = new DBResponse<List<string>>
			{
				HasValue = false
			};
			if (Provider.Databases == null) Provider.Refresh();
			if (Provider.Databases.Contains(Name))
			{
				Tables = Provider.Server.GetDatabase(Name).ToBFS(Provider).Tables;
				response.HasValue = true;
				response.Response = Tables;
			}
			return response;
		}

		public static implicit operator MongoDatabase(DBResponse<IDatabase<MongoProvider, MongoClient>> o) => (MongoDatabase)o.Response;
	}
}