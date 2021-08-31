using Comment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Application.Services.Dtos;

namespace Comment.Application
{
    public interface ICommentService: IDisposable
    {
        bool IsDisposed { get; }

        int Add(Comments comment);

        int Update(Comments comment);

        int Delete(string id);

        int Delete(string[] ids);

        ResultDto<Comments> Find(Comments comment,int page,int size);
    }
}
