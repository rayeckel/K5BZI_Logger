using K5BZI_Models.EntityModels;
using K5BZI_Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace K5BZI_Services
{
    public class ExcelFileService : IExcelFileService
    {
        #region Properties

        private readonly IFileStoreService _fileStoreService;

        #endregion

        #region Constructors

        public ExcelFileService(IFileStoreService fileStoreService)
        {
            _fileStoreService = fileStoreService;
        }

        #endregion

        #region Public Methods

        public List<DXCC> ReadDxccExcelData()
        {
            var dataRows = _fileStoreService.ReadResourceFile("DXCC.xlsx");

            return dataRows
                .Select(row =>
                    new DXCC
                    {
                        Prefix = row["Prefix"].ToString(),
                        Entity = row["Entity"].ToString(),
                        Continent = row["Continent"].ToString(),
                        //ItuZone = (int)row["ItuZone"],
                        //CqZone = (CqZone)row["CqZone"],
                        UtcOffset = (double)row["UtcOffset"],
                        Lattitude = row["Lattitude"].ToString(),
                        Longitude = row["Longitude"].ToString()
                        //ItuAllocations = row["ItuAllocations"],
                        //OtherAmateurPrefixes = row["OtherAmateurPrefixes"]
                    })
                .ToList();
        }

        #endregion
    }
}
