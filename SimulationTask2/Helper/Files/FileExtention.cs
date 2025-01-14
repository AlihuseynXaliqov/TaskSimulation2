namespace SimulationTask2.Helper.Files
{
    public static class FileExtention
    {
        public static string Upload(this IFormFile file, string rootpath, string foldername)
        {
            string fileName = file.FileName;
            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64);
            }
            fileName = Guid.NewGuid() + fileName;
            string path = Path.Combine(rootpath, foldername, fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
        public static bool Delete(string rootPath, string foldername, string fileName)
        {
            string path = Path.Combine(rootPath, foldername, fileName);
            if (!File.Exists(path))
            {
                return false;
            }
            File.Delete(path);
            return true;
        }

    }
}
