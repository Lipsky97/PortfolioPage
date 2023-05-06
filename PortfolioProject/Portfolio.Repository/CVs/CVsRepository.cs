using Portfolio.DB;
using Portfolio.DB.Models;
using Portfolio.Repository.CVs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Repository.CVs
{
    public interface ICVsRepository
    {
        double GetHighestVersion();
        string AddCV(CVVM cv);
        CVVM GetLatestCV();

    }

    public class CVsRepository : ICVsRepository
    {
        private readonly PortfolioDbContext _db;
        public CVsRepository(PortfolioDbContext db)
        {
            _db = db;
        }

        public double GetHighestVersion()
        {
            var versions = _db.CVs.Select(x => x.Version).ToList();
            if (versions.Count == 0) {
                versions.Add(0.0);
            }
            return versions.Max();
        }

        public string AddCV(CVVM cv)
        {
            var newCV = new CV()
            {
                SID = Guid.NewGuid().ToString(),
                File = cv.File,
                Version = GetHighestVersion() + 1,
            };
            _db.CVs.Add(newCV);
            _db.SaveChanges();

            return newCV.SID;
        }

        public CVVM GetLatestCV()
        {
            var latestVersion = GetHighestVersion();
            var cv = _db.CVs.FirstOrDefault(x => x.Version == latestVersion);
            var result = new CVVM
            {
                File = cv.File,
                Version = cv.Version,
            };

            return result;
        }
    }
}
