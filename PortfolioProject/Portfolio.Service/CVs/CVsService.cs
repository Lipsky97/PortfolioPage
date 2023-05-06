using Portfolio.Repository.CVs;
using Portfolio.Repository.CVs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Service.CVs
{
    public interface ICVsService
    {
        double GetLatestVersion();
        string AddCV(CVVM cv);
        CVVM GetLatestCV();
    }

    public class CVsService : ICVsService
    {
        private readonly ICVsRepository _cvsRepository;
        public CVsService(ICVsRepository cvsRepository)
        {
            _cvsRepository = cvsRepository;
        }

        public double GetLatestVersion()
        {
            return _cvsRepository.GetHighestVersion();
        }

        public string AddCV(CVVM cv)
        {
            return _cvsRepository.AddCV(cv);
        }

        public CVVM GetLatestCV()
        {
            return _cvsRepository.GetLatestCV();
        }
    }
}
