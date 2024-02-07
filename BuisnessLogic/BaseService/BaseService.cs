using AutoMapper;
using DBConnection.Repos;
using DBConnection.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLogic.BaseService
{
    public abstract class BaseService<Dto, Repo, DbEntity> : IBaseService<Dto,
        Repo, DbEntity>
        where Dto : class
        where DbEntity : class
        where Repo : IBaseRepo<DbEntity>
    {
        public BaseService(IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            this.UnitOfWork = unitOfWork;
            this.HttpContextAccessor = httpContextAccessor;
        }
        public IUnitOfWork UnitOfWork { get; }
        public abstract Repo DbRepo { get; }
        public IHttpContextAccessor HttpContextAccessor
        {
            get;
        }
        public IMapper Mapper
        {
            get
            {
                return (IMapper)HttpContextAccessor?.HttpContext?.RequestServices.GetService(typeof(IMapper));
            }
        }
        public virtual Dto Create(Dto dto)
        {
            DbEntity dbEntity = Mapper.Map<DbEntity>(dto);
            dbEntity = DbRepo.Insert(dbEntity);
            UnitOfWork.Save();
            return Mapper.Map<Dto>(dbEntity);
        }
        public virtual Dto Update(Dto dto)
        {
            DbEntity dbEntity = Mapper.Map<DbEntity>(dto);
            dbEntity = DbRepo.Update(dbEntity);
            UnitOfWork.Save();
            return Mapper.Map<Dto>(dbEntity);
        }
        public virtual void Delete(Dto dto)
        {
            DbEntity dbEntity = Mapper.Map<DbEntity>(dto);
            DbRepo.Delete(dbEntity);
            UnitOfWork.Save();
        }

    }
}
