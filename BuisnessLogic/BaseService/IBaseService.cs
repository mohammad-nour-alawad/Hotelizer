using AutoMapper;
using DBConnection.Repos;
using DBConnection.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLogic.BaseService
{
    public interface IBaseService<Dto, Repo, DbEntity>
        where Dto: class
        where Repo: IBaseRepo<DbEntity>
        where DbEntity: class
    {
        public IUnitOfWork UnitOfWork { get; }

        public abstract Repo DbRepo { get; }

        public IHttpContextAccessor HttpContextAccessor { get; }

        public IMapper Mapper { get; }

        public abstract Dto Create(Dto dto);

        public abstract Dto Update(Dto dto);
    }
}
