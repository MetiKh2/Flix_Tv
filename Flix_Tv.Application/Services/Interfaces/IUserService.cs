using Flix_Tv.Application.DTOs.AdminPage;
using Flix_Tv.Application.DTOs.User.Admin;
using Flix_Tv.Application.DTOs.User.Auth;
using Flix_Tv.Application.DTOs.User.Profile;
using Flix_Tv.Domain.Entites.Enums;
using Flix_Tv.Domain.Entites.SettingSite;
using Flix_Tv.Domain.Entites.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.Services.Interfaces
{
   public interface IUserService
    {
        Task<bool> IsUserNameExist(string username);
        Task<bool> IsEmailExist(string email);
        Task RegisterUser(UserRegisterDto dto);
        Task<bool> IsActivateCodeExists(string activateCode);
        Task ActiveUser(string activeCode);
        Task UpdateUser(User user);
        Task<User> UserLogin(UserLoginDto dto);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByActiveCode(string activeCode);
        Task ChangeForgotPassword(ChangePasswordDto change);
        Task<User> GetUserById(long id);
        Task<User> GetUserByUserName(string username);
        Task<long> GetUserIdByUserName(string userName);
        Task<string> GetUserNameById(long id);
        Task<string> GetUserImageById(long id);
        string GetUserNameByIdNoAsync(long id);
        string GetUserImageByIdNoAsync(long id);
        Task<List<LastUserInAdminPageDto>> GetLastUserInAdminPage(int take);


        Task<long> CreateUserInAdmin(CreateUserDto dto);
        Task EditUserInAdmin(EditUserDto dto);
        Task<Tuple<List<UsersListDto>, int,int>> GetUsersInAdmin(int pageId=0,string filter="",string sort="");
        Task<bool> DeleteUserInAdmin(long id);



        Task<bool> ChangeFullNameInProfile(string username,string fullName);
        Task<long> CreateWallet(long amount,long userId,WalletType walletType,bool isPay);
        Task<Wallet> GetWalletById(long id);
        Task UpdateWallet(Wallet wallet);
        Task<long> GetSumWallets(long userId);
        Task<int> GetUserCommentCount(long userId);
        Task<int> GetUserRateCount(long userId);
        //Task<bool> ChangePasswordInProfile(string username,string currentPassword,string newPassword,string reNewPassword);

        Task CreateUserLoginLog(long userId,string ip);




        Task<bool> ExistUserPlan(long userId);
        Task CreatePlan(long userId,PlanType planType);
        Task<GetLastPlanDto> GetLastPlan(long userId);
      long GetAmountByPlanType(PlanType planType);




        Task CreateContactUs(ContactUs contactUs);
        Task<Tuple<List<ContactUs>, int, int>> GetContactUsInAdmin(string filter="",int pageId=1);
        Task<bool> DeleteContactUs(long id);
    }
}
