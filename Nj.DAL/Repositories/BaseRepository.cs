using Microsoft.EntityFrameworkCore;
using CF.Infrastructure.Localizations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nj.DAL;
using Nj.Infrastructure.Models.Entities.Common;
using Nj.Infrastructure.Models.Enums;

namespace Nj.DAL.Repositories
{
   public class BaseRepository<TEntity> where TEntity : class
    {
        protected readonly NjDBContext _db;

        public BaseRepository(NjDBContext db)
        {
            _db = db;
        }

        public virtual async Task<ResultEntity<TEntity>> Delete(TEntity entity)
        {
            ResultEntity<TEntity> result = new ResultEntity<TEntity>();

            try
            {
                _db.Entry(entity).State = EntityState.Modified;

                _db.Remove(entity);
                await _db.SaveChangesAsync();

                result.Messages.Add(Localization.DeletedSuccesfully);
                result.Status = StatusEnum.Success;
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

        public virtual async Task<ResultList<TEntity>> GetAll()
        {
            ResultList<TEntity> result = new ResultList<TEntity>();

            try
            {
                var list = await _db.Set<TEntity>().ToListAsync();

                if (list.Count != 0)
                {
                    result.Items = list;
                    result.Messages.Add(Localization.ItemsFound);
                    result.Status = StatusEnum.Success;
                }
                else
                {
                    result.Messages.Add(Localization.NoItemsFound);
                    result.Status = StatusEnum.Warning;
                }
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

        public virtual async Task<ResultEntity<TEntity>> GetById(int id)
        {
            ResultEntity<TEntity> result = new ResultEntity<TEntity>();

            try
            {
                var entity = await _db.Set<TEntity>().FindAsync(id);
                _db.Entry(entity).State = EntityState.Detached;

                if (entity != null)
                {
                    result.Entity = entity;
                    result.Messages.Add(Localization.ItemFound);
                    result.Status = StatusEnum.Success;
                }
                else
                {
                    result.Messages.Add(Localization.NoItemFound);
                    result.Status = StatusEnum.Warning;
                }
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

        public virtual async Task<ResultEntity<TEntity>> Insert(TEntity entity)
        {
            ResultEntity<TEntity> result = new ResultEntity<TEntity>();

            try
            {
                _db.Set<TEntity>().Add(entity);
                await _db.SaveChangesAsync();


                result.Entity = entity;
                result.Messages.Add(Localization.InsertedSuccesfully);
                result.Status = StatusEnum.Success;
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

        public virtual async Task<ResultEntity<TEntity>> Update(TEntity entity)
        {
            ResultEntity<TEntity> result = new ResultEntity<TEntity>();

            try
            {
                _db.Entry(entity).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                result.Entity = entity;
                result.Messages.Add(Localization.UpdatedSuccesfully);
                result.Status = StatusEnum.Success;
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
