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


public class Index_Temp_2fDebts_2fByOwedBy_IdAndPaid : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Temp_2fDebts_2fByOwedBy_IdAndPaid()
	{
		this.ViewText = @"from doc in docs.Debts
select new { OwedBy_Id = doc.OwedBy.Id, Paid = doc.Paid }";
		this.ForEntityNames.Add("Debts");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "Debts", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				OwedBy_Id = doc.OwedBy.Id,
				Paid = doc.Paid,
				__document_id = doc.__document_id
			});
		this.AddField("OwedBy_Id");
		this.AddField("Paid");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("OwedBy.Id");
		this.AddQueryParameterForMap("Paid");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("OwedBy.Id");
		this.AddQueryParameterForReduce("Paid");
		this.AddQueryParameterForReduce("__document_id");
	}
}
