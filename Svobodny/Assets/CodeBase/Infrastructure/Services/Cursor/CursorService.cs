using CodeBase.Data.StaticData.Cursor;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Cursor
{
    public class CursorService : ICursorService
    {
        private readonly IStaticDataService _staticData;

        public CursorService(IStaticDataService staticData)
        {
            _staticData = staticData;
        }

        public void ChangeCursor(CursorType cursorType)
        {
            var cursorData = _staticData.ForCursor(cursorType);

            UnityEngine.Cursor.SetCursor(cursorData.Texture, cursorData.HotSpot, CursorMode.Auto);
        }
    }
}