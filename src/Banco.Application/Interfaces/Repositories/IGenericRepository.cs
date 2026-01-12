namespace Banco.Application.Interfaces.Repositories
{
    /// <summary>
    /// repositorio generico que define operaciones basicas de acceso a datos 
    /// el objetivo prinpical es evitar duplicacion de codigo CRUD y desacoplar la capa 
    /// Application de cualquier ORM especifico
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Obtiene una entidad por su indentificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T?> GetByIdAsync(int id);
        /// <summary>
        /// Obtiene todas las entidades del tipo especificado
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();
        /// <summary>
        /// Agrega una nueva entidad al contexto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(T entity);
        /// <summary>
        /// marca una entidad como modificada
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        /// <summary>
        /// Elimina una entidad del contexto
        /// </summary>
        /// <param name="entity"></param>
        void Remove(T entity);
    }
}