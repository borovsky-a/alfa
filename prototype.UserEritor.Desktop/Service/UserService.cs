using prototype.UserEritor.Desktop.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace prototype.UserEritor.Desktop.Service
{
    public class UserService : IUserService
    {
        const string USERS_FILE_PATH = @"./users.xml";
        const int _millisecondsRange = 100;
        static Random _random = new Random();

        public async Task<IResponse<User>> DeleteUserAsync(int id)
        {
            try
            {
                var allUsers = ReadUsers();
                var userToDelete = allUsers.FirstOrDefault(o => o.Id == id);
                if (userToDelete == null)
                {
                    return new Response<User> { Description = $"Пользователь с идентификатором {id} не найден." , IsValid = false};
                }
                allUsers.Remove(userToDelete);

                SaveChanges(allUsers);

                return 
                    await Task.FromResult(new Response<User> { Value = userToDelete, Description = $"Пользователь {userToDelete.LastName} {userToDelete.FirstName} {userToDelete.MiddleName} удален" });               
            }
            catch (Exception ex)
            {
                return new Response<User> { Description = $"Произошла ошибка {ex.Message}", IsValid = false };
            }
        }

      
        public async Task<IResponse<User>> CreateUserAsync(User user)
        {
            try
            {
                var allUsers = ReadUsers();
                var existsUser = 
                    allUsers.FirstOrDefault(o => o.MiddleName == user.MiddleName && o.LastName == user.LastName && o.FirstName == user.FirstName);
                if(existsUser != null)
                {
                    return new Response<User> { Value = user, Description = $"Пользователь с указанными именными компонентами ({user.LastName} {user.FirstName} {user.MiddleName}) уже существует " , IsValid = false };
                }
                existsUser =
                    allUsers.FirstOrDefault(o => o.EmailAddress == user.EmailAddress);
                if (existsUser != null)
                {
                    return new Response<User> { Value = user, Description = $"Пользователь с указанным email ({user.EmailAddress}) уже существует", IsValid = false };
                }
                var newId = allUsers.Select(o => o.Id).Max();
                user.Id = newId + 1;
                allUsers.Add(user);

                SaveChanges(allUsers);

                return await Task.FromResult(new Response<User> { Value = user , Description = $"Пользователь {user.EmailAddress} успешно добавлен"});
            }
            catch (Exception ex)
            {
                return new Response<User> { Description = ex.Message, IsValid = false };
            }
        }

       
        public async Task<IResponse<User>> GetUserByIdAsync(int id)
        {
            try
            {
                var allUsers = ReadUsers();
                var user = allUsers.FirstOrDefault(o => o.Id == id);
                return await Task.FromResult(new Response<User> { Value = user });
            }
            catch (Exception ex)
            {
                return new Response<User> {Description = ex.Message, IsValid = false };
            }
        }
       
        public async Task<IPagingResponse<User>> GetPagingListAsync(UserListRequest request)
        {
            try
            {
                var users = ReadUsers().AsEnumerable();
               
                if (!string.IsNullOrEmpty(request.Filter))
                {
                    users = users.Where(o =>  o.LastName.ToUpper().Contains(request.Filter.ToUpper()));
                }

                var count = users.Count();
                users = users.OrderByDescending(o=> o.Id).Skip(request.Offset).Take(request.PageSize);               
               

                var response = new PagingResponse<User>
                {
                    Value = users,
                    NavsCount = request.NavsCount,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalRecordCount = count
                };
                return await Task.FromResult(response); 
            }
            catch (Exception ex)
            {
                //TODO log
                return new PagingResponse<User> { Description = ex.Message };
            }  
        }

        private List<User> ReadUsers(bool force = false)
        {
            try
            {
                CreateDatabaseIfNotExists(force);
                var serializer = new XmlSerializer(typeof(List<User>));

                using (var reader = XmlReader.Create(USERS_FILE_PATH))
                {
                    var result = (List<User>)serializer.Deserialize(reader);
                    return result;
                }
            }
            catch(InvalidOperationException)
            {
                if(force == false)
                   return ReadUsers(true);
                throw;
            }          
        }

        private void CreateDatabaseIfNotExists(bool force = false)
        {
            if (!File.Exists(USERS_FILE_PATH) || force == true)
            {
                using (var manifestResourceStream = System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("prototype.UserEritor.Desktop.Resources.Doc.users.xml"))
                using (var fileStream = File.OpenWrite(USERS_FILE_PATH))
                {
                    manifestResourceStream.CopyTo(fileStream);
                }
            }
        }
        private void SaveChanges(List<User> allUsers)
        {
            var serializer = new XmlSerializer(typeof(List<User>));
            var doc = new XDocument();
            using (var writer = doc.CreateWriter())
            {
                serializer.Serialize(writer, allUsers);
            }
            var value = doc.ToString();
            File.WriteAllText(USERS_FILE_PATH, value);
        }
    }
}
