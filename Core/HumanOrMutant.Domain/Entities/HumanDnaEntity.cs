using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanOrMutant.Domain.Entities
{
    [DynamoDBTable("HumanDna")]
    public class HumanDnaEntity
    {
        [DynamoDBHashKey("DnaSecuence")]
        public string? DnaSecuence { get; set; }


        [DynamoDBProperty("IsMutant")]
        public bool IsMutant { get; set; }
    }
}
