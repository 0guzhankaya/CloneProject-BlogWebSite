using ProgrammersBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Data.Abstract
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        // GETiren Method.
        // Sorgunun yazıldığı Expression'a predicate denir.
        // var kullanici = repository.GetAsync(k=>k.Id == 15); Expression
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        // predicate null gelirse tüm entityler yüklenecek. Eğer null gelmezse, filtreye göre yüklenecek.
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);

        // entity dönmesi gerekiyor çünkü alıp kullanıcıya gösterme işlemleri uygulanacak. json'a çevrilip frontende gönderilecek.
        Task<T> AddAsync(T entity);

        // entity dönmesi gerekiyor çünkü alıp kullanıcıya gösterme işlemleri uygulanacak. json'a çevrilip frontende gönderilecek.
        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        // Eklenecek veri daha önce var mı?
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }
}
