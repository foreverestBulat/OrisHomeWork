using MyHttpServer.Attribuete;
using MyHttpServer.Attribuets;
using MyHttpServer.Model;
using MyORMApplyPostgreSQL;

namespace MyHttpServerCopy.Controllers
{
    [Controller("Account")]
    public class AccountController
    {
        //private readonly MyDataContext _context =
        //new("User ID=postgres;Password=root;Host=localhost;Port=5432;Database=myhttpserver;");

        private readonly IMyDataContext dataBase = new MyDataContext("localhost", "postgres", "postgres", "5432", "subuhankulov");

        private static Account[] accounts = new Account[]
        {
            new Account { Login = "bulat", Password = "subuh"},
            new Account { Login = "islam", Password = "bagav"}
        };

        [Get("GetAccountById")]
        public Account GetAccountById(int id)
        {
            return dataBase.SelectByID<Account>(id);
        }

        [Get("GetAccountsAll")]
        public List<Account> GetAccounsAll()
        {
            return dataBase.Select<Account>();
        }
    }
}
