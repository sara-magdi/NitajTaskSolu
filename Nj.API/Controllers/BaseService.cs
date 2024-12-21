
using AutoMapper;
using CF.Infrastructure.Localizations;
using Microsoft.AspNetCore.Mvc;
using Nj.BLL.Domains;
using Nj.DAL.Repositories;
using Nj.Infrastructure.Models.Entities.Common;
using Nj.Infrastructure.Models.Enums;
using System;
using System.Threading.Tasks;

namespace Nj.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TRepository"></typeparam>
    /// <typeparam name="TDomain"></typeparam>
    /// <typeparam name="TDTO"></typeparam>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseService<TEntity, TRepository, TDomain, TDTO> : ControllerBase
         where TEntity : class
         where TDTO : class
         where TRepository : BaseRepository<TEntity>
         where TDomain : BaseDomain<TRepository, TEntity, TDTO>
    {
       /// <summary>
       /// 
       /// </summary>
       
       
        protected readonly TDomain _domain;
        /// <summary>
        /// 
        /// </summary>
        protected readonly IMapper _mapper;

       /// <summary>
       /// 
       /// </summary>
       /// <param name="domain"></param>
       /// <param name="mapper"></param>
        public BaseService(TDomain domain, IMapper mapper)
        {
            _domain = domain;
            _mapper = mapper;
        }
        /// <summary>
        /// insert 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ResultEntity<TDTO>> Insert(TDTO dto)
        {
            ResultEntity<TDTO> result = new ResultEntity<TDTO>();

            try
            {
                result = await _domain.Insert(dto);
            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Exception;
                result.Messages.Add(Localization.GeneralError);
                result.Messages.Add(ex.Message);
                result.Details = ex.StackTrace;
            }

            return result;

        }
        /// <summary>
        /// delete 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        public virtual async Task<ResultEntity<TDTO>> Delete(TDTO dto)
        {
            ResultEntity<TDTO> result = new ResultEntity<TDTO>();

            try
            {
                result = await _domain.Delete(dto);
            }

            catch (Exception ex)
            {
                result.Status = StatusEnum.Exception;
                result.Messages.Add(Localization.GeneralError); 
                result.Messages.Add(ex.Message);
                result.Details = ex.StackTrace;
            }

            return result;
        }


        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<ResultList<TDTO>> GetAll()
        {
            ResultList<TDTO> result = new ResultList<TDTO>();

            try
            {
                result = await _domain.GetAll();
            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Exception;
                result.Messages.Add(Localization.GeneralError); 
                result.Messages.Add(ex.Message);
                result.Details = ex.StackTrace;
            }

            return result;

        }
        /// <summary>
        /// Get  By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public virtual async Task<ResultEntity<TDTO>> GetById(int id)
        {
            ResultEntity<TDTO> result = new ResultEntity<TDTO>();

            try
            {
                result = await _domain.GetById(id);
            }

            catch (Exception ex)
            {
                result.Status = StatusEnum.Exception;
                result.Messages.Add(Localization.GeneralError);  
                result.Messages.Add(ex.Message);
                result.Details = ex.StackTrace;
            }

            return result;

        }
        /// <summary>
        /// update 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public virtual async Task<ResultEntity<TDTO>> Update(TDTO dto)
        {
            ResultEntity<TDTO> result = new ResultEntity<TDTO>();

            try
            {
                result = await _domain.Update(dto);

            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Exception;
                result.Messages.Add(Localization.GeneralError); 
                result.Messages.Add(ex.Message);
                result.Details = ex.StackTrace;
            }

            return result;
        }

    }
}
