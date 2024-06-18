using CodeBase.Data.StaticData.Cursor;

namespace CodeBase.Infrastructure.Services.Cursor
{
    public interface ICursorService : IService
    {
        void ChangeCursor(CursorType cursorType);
    }
}