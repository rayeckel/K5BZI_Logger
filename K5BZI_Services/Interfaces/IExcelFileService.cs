using K5BZI_Models.EntityModels;
using System.Collections.Generic;

namespace K5BZI_Services.Interfaces
{
    public interface IExcelFileService
    {
        List<DXCC> ReadDxccExcelData();
    }
}
