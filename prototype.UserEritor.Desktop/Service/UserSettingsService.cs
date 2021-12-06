using prototype.UserEritor.Desktop.Data;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace prototype.UserEritor.Desktop.Service
{
    public class UserSettingsService : IUserSettingsService
    {
        const string USERS_SETTINGS_FILE_PATH = @"./users_table_settings.xml";


        /// <summary>
        ///     эти настройки должны быть получены по идентификатору пользователя
        /// </summary>
        /// <returns></returns>
        public async Task<IResponse<TableSettings>> GetUserSettingsAsync()
        {
            try
            {
                var settings = ReadSettings();
                return await Task.FromResult(new Response<TableSettings> { Value = settings });
            }
            catch (Exception ex)
            {
                return new Response<TableSettings> { Description = ex.Message, IsValid = false };
            }
        }

        public async Task<IResponse<TableSettings>> SaveUserSettingsAsync(TableSettings settings)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(TableSettings));
                var doc = new XDocument();
                using (var writer = doc.CreateWriter())
                {
                    serializer.Serialize(writer, settings);
                }
                var value = doc.ToString();
                File.WriteAllText(USERS_SETTINGS_FILE_PATH, value);
                return await Task.FromResult(new Response<TableSettings> { Value = settings });
            }
            catch (Exception ex)
            {
                return new Response<TableSettings> { Description = ex.Message, IsValid = false };
            }
        }

        private TableSettings ReadSettings(bool force = false)
        {
            try
            {
                CreateSettingsIfNotExists(force);
                var serializer = new XmlSerializer(typeof(TableSettings));

                using (var reader = XmlReader.Create(USERS_SETTINGS_FILE_PATH))
                {
                    var result = (TableSettings)serializer.Deserialize(reader);
                    return result;
                }
            }
            catch (InvalidOperationException)
            {
                if (force == false)
                    return ReadSettings(true);
                throw;
            }
        }

        private void CreateSettingsIfNotExists(bool force = false)
        {
            if (!File.Exists(USERS_SETTINGS_FILE_PATH) || force == true)
            {
                using (var manifestResourceStream = System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("prototype.UserEritor.Desktop.Resources.Doc.users_table_settings.xml"))
                using (var fileStream = File.OpenWrite(USERS_SETTINGS_FILE_PATH))
                {
                    manifestResourceStream.CopyTo(fileStream);
                }
            }
        }

        private void SaveChanges(TableSettings settings)
        {
            var serializer = new XmlSerializer(typeof(TableSettings));
            var doc = new XDocument();
            using (var writer = doc.CreateWriter())
            {
                serializer.Serialize(writer, settings);
            }
            var value = doc.ToString();
            File.WriteAllText(USERS_SETTINGS_FILE_PATH, value);
        }
    }
}
