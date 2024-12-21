using AutoMapper;
using CF.Infrastructure.Localizations;
using Nj.DAL.Repositories;
using Nj.Infrastructure.Models.Entities.Common;
using Nj.Infrastructure.Models.Enums;

namespace Nj.BLL.Domains
{
    public class BaseDomain<TRepository, TEntity, TDTO> where TEntity : class
                                                     where TRepository : BaseRepository<TEntity>
                                                     where TDTO : class
    {
        protected readonly IMapper _mapper;
        protected readonly TRepository _repository;

        public BaseDomain(IMapper mapper, TRepository repository)
        {
            _mapper = mapper;
            _repository = repository;

        }

        public virtual async Task<ResultEntity<TDTO>> Insert(TDTO dto)
        {
            ResultEntity<TDTO> result = new ResultEntity<TDTO>();

            try
            {
                var entity = _mapper.Map<TDTO, TEntity>(dto);
                var repoResult = await _repository.Insert(entity);
                result = _mapper.Map<ResultEntity<TEntity>, ResultEntity<TDTO>>(repoResult);
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

        public virtual async Task<ResultEntity<TDTO>> Update(TDTO dto)
        {
            ResultEntity<TDTO> result = new ResultEntity<TDTO>();

            try
            {
                var entity = _mapper.Map<TDTO, TEntity>(dto);
                var repoResult = await _repository.Update(entity);
                result = _mapper.Map<ResultEntity<TEntity>, ResultEntity<TDTO>>(repoResult);
            }
            catch (Exception ex)
            {
                result.Status = StatusEnum.Exception;
                result.Messages.Add(Localization.GeneralError); // حدث خطأ ، الرجاء المحاولة مرى أخرى
                result.Messages.Add(ex.Message);
                result.Details = ex.StackTrace;
            }

            return result;
        }

        public virtual async Task<ResultEntity<TDTO>> Delete(TDTO dto)
        {
            ResultEntity<TDTO> result = new ResultEntity<TDTO>();

            try
            {
                var entity = _mapper.Map<TDTO, TEntity>(dto);
                var repoResult = await _repository.Delete(entity);
                result = _mapper.Map<ResultEntity<TEntity>, ResultEntity<TDTO>>(repoResult);
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

        public virtual async Task<ResultEntity<TDTO>> GetById(int id)
        {
            ResultEntity<TDTO> result = new ResultEntity<TDTO>();

            try
            {
                var repoResult = await _repository.GetById(id);
                result = _mapper.Map<ResultEntity<TEntity>,ResultEntity<TDTO>>(repoResult);
            }

            catch (Exception ex)
            {
                result.Status = StatusEnum.Exception;
                result.Messages.Add(Localization.GeneralError); // حدث خطأ ، الرجاء المحاولة مرى أخرى
                result.Messages.Add(ex.Message);
                result.Details = ex.StackTrace;
            }

            return result;
        }

        public virtual async Task<ResultList<TDTO>> GetAll()
        {
            ResultList<TDTO> result = new ResultList<TDTO>();

            try
            {
                var repoResult = await _repository.GetAll();
                result = _mapper.Map<ResultList<TEntity>, ResultList<TDTO>>(repoResult);
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

