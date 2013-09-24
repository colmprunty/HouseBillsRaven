using Raven.Abstractions;
using Raven.Database.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using Raven.Database.Linq.PrivateExtensions;
using Lucene.Net.Documents;
using System.Globalization;
using System.Text.RegularExpressions;
using Raven.Database.Indexing;


public class Index_Temp_2fPeople_2fByAliveAndName : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Temp_2fPeople_2fByAliveAndName()
	{
		this.ViewText = @"from doc in docs.People
select new { Alive = doc.Alive, Name = doc.Name }";
		this.ForEntityNames.Add("People");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "People", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				Alive = doc.Alive,
				Name = doc.Name,
				__document_id = doc.__document_id
			});
		this.AddField("Alive");
		this.AddField("Name");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("Alive");
		this.AddQueryParameterForMap("Name");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("Alive");
		this.AddQueryParameterForReduce("Name");
		this.AddQueryParameterForReduce("__document_id");
	}
}
