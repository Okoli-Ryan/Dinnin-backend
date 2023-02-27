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

            foreach(T _t in t) {
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





        public async Task<T> Update (T t) {

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







        public async Task<bool> Delete(Guid ID) {
            try {

                var Entity = await GetByID(ID);

                context.Entry(Entity).State = EntityState.Deleted;

                await context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex) {
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }






        public async Task<bool> Delete(List<T> t) {
            try {

                context.Set<T>().RemoveRange(t);
                return true;

            }
            catch (Exception ex) {
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }








        public async Task<T> GetByID(Guid ID) {

            try {
                var t = await context.Set<T>().Where(x => x.ID.Equals(ID)).FirstOrDefaultAsync();
                return t;
            }
            catch (Exception ex){
                Debug.WriteLine(ex.StackTrace);
                return default;
            }
        }





    }
}
