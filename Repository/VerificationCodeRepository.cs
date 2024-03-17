namespace OrderUp_API.Repository {
    public class VerificationCodeRepository : AbstractRepository<VerificationCode> {

        public VerificationCodeRepository(OrderUpDbContext context) : base(context) { }

        public async Task<bool> DisableVerificationCode(Guid UserId) {

            var OldVerificationCode = await context.VerificationCode.FirstOrDefaultAsync(x => x.UserID.Equals(UserId));

            if (OldVerificationCode is not null) {
                OldVerificationCode.ActiveStatus = false;

                context.SaveChanges();
            }

            return true;
        }

        public async Task<VerificationCode> GetVerificationModelByCode(string Code) {

            var VerificationModel = await context.VerificationCode.Where(x => x.Code.Equals(Code)).FirstOrDefaultAsync();

            return VerificationModel;
        }

        public async Task<VerificationCode> GetPendingVerificationCode(Guid UserId) {

            var VerificationModel = await context.VerificationCode.Where(x => x.UserID.Equals(UserId) && x.ActiveStatus).FirstOrDefaultAsync();

            return VerificationModel;
        }

        public async Task<VerificationCode> GetActiveVerificationCodeByCode(Guid UserId, string Code) {

            var VerificationModel = await context.VerificationCode.Where(x => x.Code.Equals(Code) && x.UserID.Equals(UserId) && x.ActiveStatus).FirstOrDefaultAsync();

            return VerificationModel;
        }

        public async Task<VerificationCode> GetActiveVerificationCodeByCode(string Code) {

            var VerificationModel = await context.VerificationCode.Where(x => x.Code.Equals(Code) && x.ActiveStatus).FirstOrDefaultAsync();

            return VerificationModel;
        }
    }
}
