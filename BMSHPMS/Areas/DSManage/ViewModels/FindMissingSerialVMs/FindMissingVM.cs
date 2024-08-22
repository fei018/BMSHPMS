using BMSHPMS.Models.DharmaService;
using BMSHPMS.Models.DharmaServiceExtention;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS.DSManage.ViewModels.FindMissingSerialVMs
{
    public class FindMissingVM : BaseVM
    {
        public FindSearchVM FindSearch { get; set; }


        public List<ComboSelectListItem> GetDonationProjectSelectItems(string serviceID)
        {
            var list = new List<ComboSelectListItem>();
            if (string.IsNullOrEmpty(serviceID))
            {
                return list;
            }

            list = DC.Set<Opt_DonationProject>().CheckID(serviceID, x => x.DharmaServiceID)
                        .OrderBy(x => x.DonationCategory)
                        .ThenBy(x => x.Sum)
                        .GetSelectListItems(Wtm, x => $"{x.DonationCategory}, ${x.Sum}", y => y.ID);
            return list;
        }

        public FindResult FindResult {  get; set; }

        public void FindMissingSerials(FindSearchVM search)
        {
            List<int> numbers = new();

            var donation = DC.Set<Opt_DonationProject>()
                                .CheckID(search.DonationProjectID, x => x.ID)
                                .Single();

            switch (donation.DonationCategory)
            {   
                case DonationProjectOptions.Category.功德主:
                    var donors = DC.Set<Info_Donor>()
                                    .AsNoTracking()
                                    .CheckID(search.DonationProjectID,x => x.DonationProjectId)
                                    .CheckEqual(search.DharmaServiceYear,x=>x.Receipt.DharmaServiceYear)
                                    .ToList();
                    numbers.AddRange(donors.Select(x => x.DProjectSerialNumber ?? x.DProjectSerialNumber.Value));
                    break;

                case DonationProjectOptions.Category.附薦位:
                    var mems = DC.Set<Info_Memorial>()
                                    .AsNoTracking() 
                                    .CheckID(search.DonationProjectID, x => x.DonationProjectId)
                                    .CheckEqual(search.DharmaServiceYear, x => x.Receipt.DharmaServiceYear)
                                    .ToList();
                    numbers.AddRange(mems.Select(x => x.DProjectSerialNumber ?? x.DProjectSerialNumber.Value));
                    break;

                case DonationProjectOptions.Category.延生位:
                    var longs = DC.Set<Info_Longevity>()
                                    .AsNoTracking()
                                    .CheckID(search.DonationProjectID, x => x.DonationProjectId)
                                    .CheckEqual(search.DharmaServiceYear, x => x.Receipt.DharmaServiceYear)
                                    .ToList();
                    numbers.AddRange(longs.Select(x => x.DProjectSerialNumber ?? x.DProjectSerialNumber.Value));
                    break;

                default:
                    break;
            }

            numbers.Sort((x, y) => x >= y ? 1 : -1);

            int last = donation.UsedNumber;

            var miss = Enumerable.Range(1, last).Except(numbers).ToList();

            var serials = new List<string>();

            foreach ( var m in miss)
            {
                serials.Add(donation.SerialCode + m.ToString().PadLeft(4, '0'));
            }

            FindResult = new()
            {
                DharmaServiceYear = search.DharmaServiceYear,
                DharmaServiceName = DC.Set<Opt_DharmaService>().CheckID(search.DharmaServiceID, x => x.ID).Single().ServiceName,
                DonationProjectCategory = donation.DonationCategory,
                MissingSerials = serials
            };
        }
    }
}
