using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pwinty.Client
{
    public class CatalogueResource :BaseResource
    {
        private const string _cataloguePath = "/v2/Catalogue";
        public Catalogue Get(string countryCode, QualityLevel qualityLevel)
        {
            var request = new RestRequest
            {
                Resource = _cataloguePath,
                Method = Method.GET
            };
            request.AddParameter("countryCode", countryCode);
            request.AddParameter("qualityLevel", qualityLevel.ToString());
            var response = Client.ExecuteWithErrorCheck<Catalogue>(request);
            return response.Data;
        }
    }
}
