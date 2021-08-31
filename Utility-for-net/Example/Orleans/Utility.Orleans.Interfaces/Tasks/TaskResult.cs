using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Application.Services.Dtos;

namespace Tasks
{
    [Serializable]
    public  class TaskResult: ResultDto<TaskEntity>
    {
    }
}
