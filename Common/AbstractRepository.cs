namespace OrderUp_API.Common {
    public class AbstractRepository<T> where T : AbstractEntity {

        public readonly OrderUpDbContext context;

        public AbstractRepository(OrderUpDbContext context) {
            this.context = context;
        }





        public async Task<T> Save(T t) {

            t.CreatedAt = DateTime.Now;
            t.UpdatedAt = DateTime.Now;
            t.ActiveStatus = true;

            try {
                await context.Set<T>().AddAsync(t);
                await context.SaveChangesAsync();
                return t;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.StackTrace);
                return default;
            }
        }





        public async Task<List<T>> Save(List<T> t) {

            foreach (T _t in t) {
                _t.CreatedAt = DateTime.Now;
                _t.UpdatedAt = DateTime.Now;
                _t.ActiveStatus = true;
            }

            try {
                await context.Set<T>().AddRangeAsync(t);
                await context.SaveChangesAsync();
                return t;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.StackTrace);
                return default;
            }
        }





        public async Task<T> Update(T t) {

            try {

                t.UpdatedAt = DateTime.Now;
                context.Set<T>().Update(t);
                await context.SaveChangesAsync();
                return t;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.StackTrace);
                return default;
            }
        }




        public async Task<List<T>> Update(List<T> entities) {
            try {
                var updatedEntities = new List<T>();

                foreach (var entity in entities) {
                    entity.UpdatedAt = DateTime.Now;
                    context.Set<T>().Update(entity);
                    updatedEntities.Add(entity);
                }

                await context.SaveChangesAsync();

                return updatedEntities;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.StackTrace);
                return null;
            }
        }




        public async Task<bool> Delete(Guid ID) {
            try {

                var Entity = await GetByID(ID) ?? throw new Exception();

                Entity.ActiveStatus = false;

                var isUpdated = await Update(Entity) ?? throw new Exception();

                return true;

            }
            catch (Exception ex) {
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }






        public async Task<bool> Delete(List<T> t) {
            try {
                foreach (var entity in t) {
                    entity.ActiveStatus = false;
                    context.Entry(entity).State = EntityState.Modified;
                }

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }








        public async Task<T> GetByID(Guid? ID, bool filterByActiveStatus = false) {
            try {
                var query = context.Set<T>().Where(x => x.ID.Equals(ID));

                if (filterByActiveStatus) {
                    query = query.Where(x => x.ActiveStatus);
                }

                var t = await query.AsNoTracking().FirstOrDefaultAsync();
                return t;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.StackTrace);
                return default;
            }
        }



        public async Task<PaginatedResponse<T>> GetPaginatedList(IQueryable<T> query, PaginationRequest paginationRequest) {
            
            try {


                if (paginationRequest.StartDate.HasValue) {
                    query = query.Where(x => x.CreatedAt >= paginationRequest.StartDate);
                }

                if (paginationRequest.EndDate.HasValue) {
                    query = query.Where(x => x.CreatedAt <= paginationRequest.EndDate);
                }

                if (paginationRequest.Page == 0) paginationRequest.Page = 1;

                var countTask = query.CountAsync();

                var dataTask = query
                                 .OrderByDescending(x => x.CreatedAt)
                                 .Skip((paginationRequest.Page - 1) * paginationRequest.Size)
                                 .Take(paginationRequest.Size)
                                 .AsNoTracking()
                                 .ToListAsync();

                await Task.WhenAll(countTask, dataTask);

                var total = await countTask;
                var data = await dataTask;

                return new PaginatedResponse<T> { Data = data, Page = paginationRequest.Page, Size = paginationRequest.Size, Total = total };
            }

            catch (Exception ex) {
                Debug.WriteLine(ex.StackTrace);
                return default;
            }
        }




        public async Task<PaginatedResponse<T>> GetPaginatedList(PaginationRequest paginationRequest) {

            var query = context.Set<T>().AsQueryable();

            return await GetPaginatedList(query, paginationRequest);
        }

    }
}
