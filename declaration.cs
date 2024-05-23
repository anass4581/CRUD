using System;

namespace WebApplication7
{
    public interface IStudentManagement
    {
        void LoadRecord();
    }
    public interface IStudentUpdate
    {
        void UpdateRecord();
    }
    public interface IStudentDelete
    {
        void DeleteRecord();
    }
    public interface IClearAll
    {
        void ClearAll();
    }
}
