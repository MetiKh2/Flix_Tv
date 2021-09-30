using Flix_Tv.Application.DatabaseInterfaces;
using Flix_Tv.Application.DTOs.AdminPage;
using Flix_Tv.Application.DTOs.User.Admin;
using Flix_Tv.Application.DTOs.User.Auth;
using Flix_Tv.Application.DTOs.User.Profile;
using Flix_Tv.Application.Services.Interfaces;
using Flix_Tv.Common.Convertors;
using Flix_Tv.Common.Generators;
using Flix_Tv.Common.Security;
using Flix_Tv.Domain.Entites.Enums;
using Flix_Tv.Domain.Entites.Plans;
using Flix_Tv.Domain.Entites.SettingSite;
using Flix_Tv.Domain.Entites.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IFlixTvContext _context;
        public UserService(IFlixTvContext context)
        {
            _context = context;
        }

        public async Task ActiveUser(string activeCode)
        {
            var user = await GetUserByActiveCode(activeCode);
            user.IsActive = true;
            user.ActiveCode = GeneratCode.GenerateUniqCode();
            await UpdateUser(user);
        }

        public async Task ChangeForgotPassword(ChangePasswordDto change)
        {
            var user = await GetUserByActiveCode(change.ActiveCode);
            if (user != null)
            {
                user.ActiveCode = GeneratCode.GenerateUniqCode();
                user.Password = MyPasswordHasher.EncodePasswordMd5(change.Password);
                await UpdateUser(user);
            }
        }

        public async Task<bool> ChangeFullNameInProfile(string username, string fullName)
        {
            try
            {
                var user = await GetUserByUserName(username);
                user.FullName = fullName;
                await UpdateUser(user);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public async Task CreateContactUs(ContactUs contactUs)
        {
            await _context.ContactUs.AddAsync(contactUs);
            await _context.SaveChangesAsync();
        }

        public async Task CreatePlan(long userId, PlanType planType)
        {
            var plan = new Plan { 
            CraeteDate=DateTime.Now,
            PlanType=planType,
           UserId=userId,
            };
            switch (planType)
            {
                case PlanType.OneMonth:
                {
                        plan.PlanAmount = 15000;
                        plan.PlanEnd = DateTime.Now.AddMonths(1).AddDays(1);
                        break;
                }
                case PlanType.ThreeMonth:
                    {
                        plan.PlanAmount = 40000;
                        plan.PlanEnd = DateTime.Now.AddMonths(3).AddDays(1);
                        break;
                    }
                case PlanType.SixMonth:
                    {
                        plan.PlanAmount = 80000;
                        plan.PlanEnd = DateTime.Now.AddMonths(6).AddDays(1);
                        break;
                    }
                case PlanType.TwelveMonth:
                    {
                        plan.PlanAmount = 160000;
                        plan.PlanEnd = DateTime.Now.AddYears(1).AddDays(1);
                        break;
                    }
                
            }
            await _context.Plans.AddAsync(plan);
            await _context.SaveChangesAsync();
        }


        //public async Task<string> ChangePasswordInProfile(string username,string currentPassword, string newPassword, string reNewPassword)
        //{
        //    try
        //    {

        //        return "true";
        //    }
        //    catch (Exception)
        //    {
        //        return "false";
        //        throw;
        //    }
        //}

        public async Task<long> CreateUserInAdmin(CreateUserDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Password = MyPasswordHasher.EncodePasswordMd5(dto.Password),
                UserName = dto.UserName,
                ActiveCode = GeneratCode.GenerateUniqCode(),
                IsActive = true,
                CraeteDate = DateTime.Now,
                Avatar = "Default.png"
            };
            if (dto.Avatar != null&&dto.Avatar.IsImage())
            {
                user.Avatar = GeneratCode.GenerateUniqCode() + Path.GetExtension(dto.Avatar.FileName);
                string imageName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Users/Images", user.Avatar);
                using (var stream = new FileStream(imageName, FileMode.Create))
                {
                    await dto.Avatar.CopyToAsync(stream);
                }
                ImageResizer resizer = new ImageResizer();
                string resizeImageName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Users/ThumbImages", user.Avatar);
                resizer.Image_resize(imageName, resizeImageName, 100);
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task CreateUserLoginLog(long userId, string ip)
        {
            await _context.UserLoginLogs.AddAsync(new UserLoginLog
            {
                CraeteDate = DateTime.Now,
                IP = ip,
                UserId = userId
            });
            await _context.SaveChangesAsync();
        }

        public async Task<long> CreateWallet(long amount,long userId,WalletType walletType,bool isPay)
        {
            var wallet = new Wallet { 
            Amount=amount,
            CraeteDate=DateTime.Now,
            Description="شارژ کیف پول",
            IsPay=isPay,
            UserId=userId,
            WalletType=walletType,
            };
           await _context.Wallets.AddAsync(wallet);
          await  _context.SaveChangesAsync();
            return wallet.Id;

           }

        public async Task<bool> DeleteContactUs(long id)
        {
            if (await _context.ContactUs.AnyAsync(p=>p.Id==id))
            {
                var contatcUs =await _context.ContactUs.FindAsync(id);
                contatcUs.IsRemoved = true;
                contatcUs.RemoveDate = DateTime.Now;
               await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> DeleteUserInAdmin(long id)
        {
            try
            {
                var user = await GetUserById(id);
                if (user == null)
                {
                    return false;
                }
                user.IsRemoved = true;
                user.RemoveDate = DateTime.Now;
                await UpdateUser(user);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public async Task EditUserInAdmin(EditUserDto dto)
        {
            var user = await GetUserById(dto.Id);
            user.FullName = dto.FullName;
            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.IsActive = dto.IsActive;
            if (!string.IsNullOrWhiteSpace(dto.Password))
                user.Password = MyPasswordHasher.EncodePasswordMd5(dto.Password);

            if (dto.Avatar != null&&dto.Avatar.IsImage())
            {
                if (user.Avatar != "Default.png")
                {
                    string deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Users/Images", user.Avatar);
                    string deleteResizeImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Users/ThumbImages", user.Avatar);
                    if (File.Exists(deleteImagePath))
                        File.Delete(deleteImagePath);

                    if (File.Exists(deleteResizeImagePath))
                        File.Delete(deleteResizeImagePath);
                }
                user.Avatar = GeneratCode.GenerateUniqCode() + Path.GetExtension(dto.Avatar.FileName);
                string imageName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Users/Images", user.Avatar);
                using (var stream = new FileStream(imageName, FileMode.Create))
                {
                    await dto.Avatar.CopyToAsync(stream);
                }
                ImageResizer resizer = new ImageResizer();
                string resizeImageName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Users/ThumbImages", user.Avatar);
                resizer.Image_resize(imageName, resizeImageName, 100);
            }
            await UpdateUser(user);
        }

        public async Task<bool> ExistUserPlan(long userId)
        {
            return await _context.Plans.AnyAsync(p=>p.UserId==userId&&p.PlanEnd>DateTime.Now);
        }

        public long GetAmountByPlanType(PlanType planType)
        {
            long amount = 0;
            switch (planType)
            {
                case PlanType.OneMonth:
                    {
                        amount=  15000;
                        break;
                    }
                case PlanType.ThreeMonth:
                    {
                        amount = 40000;
                        break;
                    }
                case PlanType.SixMonth:
                    {
                        amount = 80000;
                        break;
                    }
                case PlanType.TwelveMonth:
                    {
                        amount = 160000;
                        break;
                    }

            }
            return amount;
        }

        public async Task<Tuple<List<ContactUs>, int, int>> GetContactUsInAdmin(string filter = "", int pageId = 1)
        {
            var contactsUs = _context.ContactUs.OrderByDescending(p=>p.CraeteDate).AsQueryable();
            int take = 3;
            int skip = (pageId - 1) * take;
            int count = contactsUs.Count();
            if (!string.IsNullOrEmpty(filter))
            {
                contactsUs = contactsUs.Where(p => p.Name.Contains(filter) || p.Subject.Contains(filter) || p.Text.Contains(filter) || p.Email.Contains(filter)).AsQueryable() ;
            }
            return Tuple.Create(await contactsUs.Skip(skip).Take(take).ToListAsync(),contactsUs.Count()/take,count);
        }

        public async Task<GetLastPlanDto> GetLastPlan(long userId)
        {
            return await _context.Plans.Where(p => p.UserId == userId && p.PlanEnd > DateTime.Now).Select(p => new GetLastPlanDto
            {
                Amount = p.PlanAmount,
                EndDate = p.PlanEnd.ToShamsi(),
                Title = p.PlanType.ToString()
            }).FirstOrDefaultAsync();
        }

        public async Task<List<LastUserInAdminPageDto>> GetLastUserInAdminPage(int take)
        {
            return await _context.Users.OrderByDescending(p => p.CraeteDate).Take(take).Select(p => new LastUserInAdminPageDto { 
            Email=p.Email,
            FullName=p.FullName,
            UserName=p.UserName
            }).ToListAsync();
        }

        public async Task<long> GetSumWallets(long userId)
        {
            var despoitWallets = await _context.Wallets.Where(p => p.IsPay == true && p.WalletType == Domain.Entites.Enums.WalletType.Deposit&&p.UserId==userId).SumAsync(p=>p.Amount);
            var payWallets = await _context.Wallets.Where(p => p.IsPay == true && p.WalletType == Domain.Entites.Enums.WalletType.pay&&p.UserId==userId).SumAsync(p=>p.Amount);
            if (payWallets >= despoitWallets) return 0;
            return despoitWallets - payWallets;
        }

        public async Task<User> GetUserByActiveCode(string activeCode)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.ActiveCode == activeCode);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<User> GetUserById(long id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserName(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.UserName == username);
        }

        public async Task<int> GetUserCommentCount(long userId)
        {
            var movieCommentsCount = await _context.MovieComments.CountAsync(p => p.UserId == userId);
            var episodeCommentsCount = await _context.SerialEpisodeComments.CountAsync(p => p.UserId == userId);
            var serialCommentsCount = await _context.SerialComments.CountAsync(p => p.UserId == userId);
            return movieCommentsCount + episodeCommentsCount + serialCommentsCount;
        }

        public async Task<long> GetUserIdByUserName(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.UserName == userName);
            if (user == null)
            {
                return 0;
            }
            return user.Id;
        }

        public async Task<string> GetUserImageById(long id)
        {
            return await _context.Users.Where(p => p.Id == id).Select(p => p.Avatar).FirstOrDefaultAsync();
        }

        public string GetUserImageByIdNoAsync(long id)
        {
            return  _context.Users.Where(p => p.Id == id).Select(p => p.Avatar).FirstOrDefault();
        }

        public async Task<string> GetUserNameById(long id)
        {
            return await _context.Users.Where(p => p.Id == id).Select(p => p.UserName).FirstOrDefaultAsync();
        }

        public string GetUserNameByIdNoAsync(long id)
        {
            return  _context.Users.Where(p => p.Id == id).Select(p => p.UserName).FirstOrDefault();
        }

        public async Task<int> GetUserRateCount(long userId)
        {
            var movieRateCount =await _context.MovieComments.CountAsync(p=>p.UserId==userId&&p.Rate!=null);
            var episodeRateCount =await _context.SerialEpisodeComments.CountAsync(p => p.UserId == userId && p.Rate != null);
            var serialRateCount = await _context.SerialComments.CountAsync(p => p.UserId == userId && p.Rate != null);
            return movieRateCount + episodeRateCount + serialRateCount;
        }

        public async Task<Tuple<List<UsersListDto>, int,int>> GetUsersInAdmin(int pageId = 1, string filter = "", string sort = "")
        {
            int take = 3;
            int skip = (pageId - 1) * take;
            var users = _context.Users.OrderByDescending(p => p.CraeteDate).AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                users = users.Where(p => p.FullName.Contains(filter) || p.UserName.Contains(filter) || p.Email.Contains(filter)).AsQueryable();
            }
            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort)
                {
                    case "date":
                        {
                            users = users.OrderByDescending(p => p.CraeteDate).AsQueryable();
                            break;
                        }
                    //case "plan":
                    //    {
                    //        users = users.OrderByDescending(p => p.CraeteDate).AsQueryable();
                    //        break;
                    //    }
                    case "activate":
                        {
                            users = users.OrderByDescending(p => p.IsActive).AsQueryable();
                            break;
                        }

                }
            }
            return Tuple.Create(await users.Select(p => new UsersListDto
            {
                Id = p.Id,
                CreateDate = p.CraeteDate.ToShamsi(),
                Email = p.Email,
                FullName = p.FullName,
                IsActive = p.IsActive,
                UserName = p.UserName,
                Avatar = p.Avatar
            }).Skip(skip).Take(take).ToListAsync(), users.Count() / take,users.Count());
        }

        public async Task<Wallet> GetWalletById(long id)
        {
            return await _context.Wallets.FindAsync(id);
        }

        public async Task<bool> IsActivateCodeExists(string activateCode)
        {
            return await _context.Users.AnyAsync(p => p.ActiveCode == activateCode);
        }

        public async Task<bool> IsEmailExist(string email)
        {
            return await _context.Users.AnyAsync(p => p.Email == email);
        }

        public async Task<bool> IsUserNameExist(string username)
        {
            return await _context.Users.AnyAsync(p => p.UserName == username);
        }

        public async Task RegisterUser(UserRegisterDto dto)
        {
            var user = new User
            {
                IsActive = false,
                ActiveCode = dto.ActiveCode,
                CraeteDate = DateTime.Now,
                Email = FixText.FixEmail(dto.Email),
                Password = MyPasswordHasher.EncodePasswordMd5(dto.Password),
                UserName = dto.UserName,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWallet(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
           await _context.SaveChangesAsync();
        }

        public Task<User> UserLogin(UserLoginDto dto)
        {
            var hashPassword = MyPasswordHasher.EncodePasswordMd5(dto.Password);
            var fixEmail = FixText.FixEmail(dto.Email);
            var user = _context.Users.FirstOrDefaultAsync(p => p.Password == hashPassword && p.Email == fixEmail && p.IsActive);
            if (user == null)
            {
                return null;
            }
            return user;

        }
    }
}
