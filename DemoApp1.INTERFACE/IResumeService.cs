using DemoApp1.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp1.INTERFACE
{
    public interface IResumeService : IRepository<ResumeEntity>
    {

        Task<ResumeEntity> GetResumeByIdAsync(int resumeId);
    }
}
