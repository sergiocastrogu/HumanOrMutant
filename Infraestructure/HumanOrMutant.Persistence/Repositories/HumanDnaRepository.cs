using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using HumanOrMutant.Domain.Entities;
using HumanOrMutant.Persistence.Contracts;

namespace HumanOrMutant.Persistence.Repositories
{
    public class HumanDnaRepository : IHumanDnaRepository
    {
        private readonly DynamoDBContext _dbContext;
        public HumanDnaRepository()
        {
            var credentials = new BasicAWSCredentials(Environment.GetEnvironmentVariable("ACCESSKEY"), Environment.GetEnvironmentVariable("SECRETKEY"));
            var region = RegionEndpoint.USEast1;
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(credentials, region);
            
            _dbContext = new DynamoDBContext(client);
        }
        public int getTotalRegisters()
        {
            var result = _dbContext.ScanAsync<HumanDnaEntity>(new List<ScanCondition>()).GetRemainingAsync().Result;
            List<HumanDnaEntity> list = result.ToList();
            return list.Count();
        }

        public int getMutantRegisters()
        {
            var result = _dbContext.ScanAsync<HumanDnaEntity>(new List<ScanCondition>()).GetRemainingAsync().Result;
            List<HumanDnaEntity> list = result.ToList();
            return list.Where(x => x.IsMutant == true).Count();
        }

        public int setHumanDna(HumanDnaEntity humanDnaEntity)
        {
            try
            {
                _dbContext.SaveAsync(humanDnaEntity);
                return 0;
            }
            catch (Exception)
            {
               return 1;
            }
        }

        public HumanDnaEntity getHumanDnaEntity(string dnaSequence)
        {
            var humanDnaEntity = _dbContext.LoadAsync<HumanDnaEntity>(dnaSequence).Result;
            if (humanDnaEntity == null)
            {
                return new HumanDnaEntity();
            }
            return humanDnaEntity;
        }
    }
}
