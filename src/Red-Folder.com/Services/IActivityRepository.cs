using Red_Folder.com.Models.Activity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Red_Folder.com.Services
{
    public interface IActivityRepository
    {
        Weekly Weekly(string year, string weekNumber);
        Task<List<Book>> BooksInYear(string year);
        Task<List<Skill>> SkillsInYear(string year);
    }
}