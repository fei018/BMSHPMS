using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;
using WalkingTec.Mvvm.Core;
using NPOI.OpenXmlFormats.Dml.WordProcessing;
using BMSHPMS.Models.DharmaService;
using BMSHPMS.Helper;
using System.Linq;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.Areas.DSReception.ViewModels
{
    public class DSRegSubmittedVM
    {
        public Guid DonationProjectID { get; set; }

        public int Count { get; set; }
      
    }
}
