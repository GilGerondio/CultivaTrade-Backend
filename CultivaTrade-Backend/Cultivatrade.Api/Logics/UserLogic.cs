using Cultivatrade.Api.DatabaseConnection;
using Cultivatrade.Api.DTO;
using Cultivatrade.Api.Models;
using Cultivatrade.Api.Services;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace Cultivatrade.Api.Logics
{
    public class UserLogic
    {
        private readonly CultivatradeContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public Random random = new Random();
        public readonly EmailSender _emailSender;

        public UserLogic(CultivatradeContext context, IWebHostEnvironment webHostEnvironment, EmailSender emailSender)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
        }

        // RESET PASSWORD
        public bool ResetPassword(string email, string password)
        {
            int success = 0;
            int verificationCode = random.Next(1000, 9999);

            var data = CheckUserByEmail(email);
            data.Password = BC.HashPassword(password, BC.GenerateSalt());
            data.VerificationCode = verificationCode;

            _context.Users.Update(data);
            success = _context.SaveChanges();

            if(success > 0)
            {
                return true;
            }
            return false;
        }
        
        // VERIFY CODE
        public User VerifyCode(string email, int verificationCode)
        {
            int success = 0;

            var data = _context.Users.FirstOrDefault(x=>x.Email == email && x.VerificationCode == verificationCode);
           
            if (data != null)
            {
                return data;
            }
            return null;
        }

        // SEND CODE 
        public User SendCode(string email)
        {
            int success = 0;
            
            int verificationCode = random.Next(1000, 9999);

            var data = CheckUserByEmail(email);
            data.VerificationCode = verificationCode;
            _context.Users.Update(data);
            success = _context.SaveChanges();

            if(success > 0)
            {
                _emailSender.SendEmail(email, verificationCode);
                return data;
            }
            return null;
        }

        // CHECK USER
        public User CheckUserByEmail(string email)
        {
            var data = _context.Users.FirstOrDefault(x => x.Email == email);
            return data;
        }

        // LOGIN USER
        public UserDTO_GET LoginUser(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(x=>x.Email == email);

            if (user != null && BC.Verify(password, user.Password))
            {
                var profileImagePath = $"{_webHostEnvironment.WebRootPath}/Images/Profiles/{user.ProfileImage}";
                var businessPermitImagePath = $"{_webHostEnvironment.WebRootPath}/Images/BusinessPermits/{user.BusinessPermitImage}";
                var sanitaryPermitImagePath = $"{_webHostEnvironment.WebRootPath}/Images/SanitaryPermits/{user.SanitaryPermitImage}";

                var data = new UserDTO_GET
                {
                    UserId = user.UserId,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Phone = user.Phone,
                    Email = user.Email,
                    Address = user.Address,
                    IsSeller = user.IsSeller,
                    IsApproved = user.IsApproved,
                    ProfileDataUrl = System.IO.File.Exists(profileImagePath)
                        ? $"data:image/png;base64,{Convert.ToBase64String(System.IO.File.ReadAllBytes(profileImagePath))}"
                        : null,
                    BusinessPermitNumber = user.BusinessPermitNumber,
                    BusinessPermitDataUrl = System.IO.File.Exists(businessPermitImagePath)
                        ? $"data:image/png;base64,{Convert.ToBase64String(System.IO.File.ReadAllBytes(businessPermitImagePath))}"
                        : null,
                    SanitaryPermitDataUrl = System.IO.File.Exists(sanitaryPermitImagePath)
                        ? $"data:image/png;base64,{Convert.ToBase64String(System.IO.File.ReadAllBytes(sanitaryPermitImagePath))}"
                        : null,
                };

                return data;
            }
            return null;
        }

        // GET USER BY USER ID
        public List<UserDTO_GET> GetUserByUserId(Guid userId)
        {
            var users = new List<UserDTO_GET>();
           
            var data = _context.Users.Where(x => x.UserId == userId).ToList();

            foreach (var item in data)
            {
                string profileImageDataUrl = "";
                string businessPermitImageDataUrl = "";
                string sanitaryPermitImageDataUrl = "";

                var file = new UserDTO_GET();
                if (File.Exists($"{_webHostEnvironment.WebRootPath}/Images/BusinessPermit/{item.ProfileImage}"))
                {
                    var profileImageBytes = System.IO.File.ReadAllBytes($"{_webHostEnvironment.WebRootPath}/Images/Profiles/{item.ProfileImage}");
                    var profileImageBase64String = Convert.ToBase64String(profileImageBytes);
                    profileImageDataUrl = $"data:image/png;base64," + profileImageBase64String;
                }
                    
                if (File.Exists($"{_webHostEnvironment.WebRootPath}/Images/BusinessPermit/{item.BusinessPermitImage}"))
                {
                    var businessPermitImageBytes = System.IO.File.ReadAllBytes($"{_webHostEnvironment.WebRootPath}/Images/BusinessPermits/{item.BusinessPermitImage}");
                    var businessPermitImageBase64String = Convert.ToBase64String(businessPermitImageBytes);
                    businessPermitImageDataUrl = $"data:image/png;base64," + businessPermitImageBase64String;
                }
                    
                if (File.Exists($"{_webHostEnvironment.WebRootPath}/Images/BusinessPermit/{item.BusinessPermitImage}"))
                {
                    var sanitaryPermitImageBytes = System.IO.File.ReadAllBytes($"{_webHostEnvironment.WebRootPath}/Images/SanitaryPermits/{item.SanitaryPermitImage}");
                    var sanitaryPermitImageBase64String = Convert.ToBase64String(sanitaryPermitImageBytes);
                    sanitaryPermitImageDataUrl = $"data:image/png;base64," + sanitaryPermitImageBase64String;
                }
                    
                file.UserId = item.UserId;
                file.Firstname = item.Firstname;
                file.Lastname = item.Lastname;
                file.Phone = item.Phone;
                file.Email = item.Email;
                file.Address = item.Address;
                file.BusinessPermitNumber = item.BusinessPermitNumber;
                file.BusinessPermitDataUrl = businessPermitImageDataUrl;
                file.SanitaryPermitDataUrl = sanitaryPermitImageDataUrl;
                file.ProfileDataUrl = profileImageDataUrl;

                users.Add(file);
            }
            return users;
        }

        // ADD USER
        public bool AddUser(UserDTO_POST dto)
        {
            int success = 0;
            string profileName = "";
            Guid userId = Guid.NewGuid();

            // PROFILE
            if(dto.ProfileImage != null)
            {
                profileName = userId.ToString() + Path.GetExtension(dto.ProfileImage.FileName);
                var profilePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images/Profiles");
                string profileFilePath = Path.Combine(profilePath, profileName);

                using (var imageFileStream = new FileStream(profileFilePath, FileMode.Create))
                {
                     dto.ProfileImage.CopyTo(imageFileStream);
                }
            }
            
            var data = new User();
            data.UserId = userId;
            data.Firstname = dto.Firstname;
            data.Lastname = dto.Lastname;
            data.Phone = dto.Phone;
            data.Email = dto.Email;
            data.Password = BC.HashPassword(dto.Password, BC.GenerateSalt());
            data.Address = dto.Address;
            data.ProfileImage = profileName;
            data.IsSeller = false;
            data.IsApproved = false;
            data.DateTimeCreated = DateTime.UtcNow;

            _context.Users.Add(data);
            success = _context.SaveChanges();

            if (success > 0)
            {
                return true;
            }
            return false;
        }

        // UPDATE USER
        public bool UpdateUser(Guid userId, UserDTO_PUT dto)
        {
            int success = 0;
            string profileName = "";
            string businessPermitName = "";
            string sanitaryPermitName = "";

            var data = _context.Users.FirstOrDefault(x => x.UserId == userId);

            // PROFILE
            if (dto.ProfileImage != null)
            {
                if (File.Exists($"{_webHostEnvironment.WebRootPath}/Images/Profiles/{data.ProfileImage}"))
                {
                    File.Delete($"{_webHostEnvironment.WebRootPath}/Images/Profiles/{data.ProfileImage}");
                }
                profileName = userId.ToString() + Path.GetExtension(dto.ProfileImage.FileName);
                var profilePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images/Profiles");
                string profileFilePath = Path.Combine(profilePath, profileName);

                using (var profileFileStream = new FileStream(profileFilePath, FileMode.Create))
                {
                    dto.ProfileImage.CopyTo(profileFileStream);
                }

                data.ProfileImage = profileName;
            }

            // PASSWORD
            if (dto.Password != null || dto.Password != "")
            {
                data.Password = BC.HashPassword(dto.Password, BC.GenerateSalt());
            }

            data.Firstname = dto.Firstname;
            data.Lastname = dto.Lastname;
            data.Phone = dto.Phone;
            data.Address = dto.Address;

            _context.Users.Update(data);
            success = _context.SaveChanges();

            if (success > 0)
            {
                return true;
            }
            return false;
        }

        // SUBMIT SELLER REQUIREMENTS
        public bool SubmitSellerRequirements(Guid userId, UserDTO_PATCH dto)
        {
            int success = 0;
            string businessPermitName = "";
            string sanitaryPermitName = "";

            var data = _context.Users.FirstOrDefault(x => x.UserId == userId);

            // BUSINESS PERMIT
            businessPermitName = userId.ToString() + Path.GetExtension(dto.BusinessPermitImage.FileName);
            var businessPermitPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images/BusinessPermits");
            string businessPermitFilePath = Path.Combine(businessPermitPath, businessPermitName);

            using (var businessPermitFileStream = new FileStream(businessPermitFilePath, FileMode.Create))
            {
                dto.BusinessPermitImage.CopyTo(businessPermitFileStream);
            }

            // SANITARY PERMIT
            sanitaryPermitName = userId.ToString() + Path.GetExtension(dto.BusinessPermitImage.FileName);
            var sanitaryPermitPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images/SanitaryPermits");
            string sanitartPermitFilePath = Path.Combine(sanitaryPermitPath, sanitaryPermitName);

            using (var sanitaryPermitFileStream = new FileStream(sanitartPermitFilePath, FileMode.Create))
            {
                dto.SanitaryPermitImage.CopyTo(sanitaryPermitFileStream);
            }

            data.BusinessPermitNumber = dto.BusinessPermitNumber;
            data.BusinessPermitImage = businessPermitName;
            data.SanitaryPermitImage = sanitaryPermitName;
            data.IsSeller = true;

            _context.Update(data);
            success = _context.SaveChanges();
            if(success > 0)
            {
                return true;
            }
            return false;
        }

        // APPROVE USER AS SELLER
        public bool ApproveUserAsSeller(Guid usersId)
        {
            int success = 0;
            var data = _context.Users.FirstOrDefault(x => x.UserId == usersId);
            data.IsApproved = true;

            _context.Users.Update(data);
            success = _context.SaveChanges();

            if(success > 0)
            {
                return true;
            }
            return false;
        }

        
    }
}
