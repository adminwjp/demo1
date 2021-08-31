using Comment.Domain.Entities;
using Comment.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
//using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility.Application.Services.Dtos;
using Utility.Attributes;
using Utility.Domain.Services;
using Utility.Domain.Uow;
using Utility.Ef;
using Utility.Ef.Uow;
using Utility.Extensions;

namespace Comment.Application
{
   // [Transtation]
    public class CommentServiceImpl:DomainService, ICommentService
    {
        private bool _isDisposed;

        public CommentServiceImpl(CommentDbContent commentDbContent)
        {
            this.UnitWork = new EfUnitWork(new DbContextProvider(commentDbContent));
            UnitWork.UseTransaction = false;
        }

        public void Dispose()
        {
            _isDisposed = true;
        }

        void Check()
        {
            if (_isDisposed)
                throw new ObjectDisposedException("CommentServiceImpl disposed");
        }

        public int Add(Comments comment)
        {
            comment.Id = Guid.NewGuid().ToString("N");
            comment.CreationTime = DateTime.Now;
            UnitWork.Insert(comment);
            UnitWork.Save();
            return 1;
        }

        public int Update(Comments comment)
        {
            comment.LastModificationTime = DateTime.Now;
            UnitWork.Update(comment); 
            UnitWork.Save();
            return 1;
        }

        public int Delete(string id)
        {
            UnitWork.Delete<Comments>(id);
            UnitWork.Save();
            return 1;
        }

        public int Delete(string[] ids)
        {
            Expression<Func<Comments, bool>> where = null;
            foreach (var item in ids)
            {
                where = LinqExpression.Or(where,it => it.Id == item);
            }
            UnitWork.Update<Comments>(where,it=>new Comments() { 
                LastModificationTime=DateTime.Now,
                IsDeleted=true
            });
            return 1;
        }

        public ResultDto<Comments> Find(Comments comment, int page, int size)
        {
            var data= UnitWork.FindListByPageOrEntity<Comments>(null, page, size).ToList();
            var count = UnitWork.Count<Comments>();
            return new ResultDto<Comments>()
            {
                Data = data,
                Result = new PageResultDto(page, size, 
                (int)(count / size == 0 ? count / size : count / size + 1), count)
            };
        }

        public bool IsDisposed => _isDisposed;
    }
}
