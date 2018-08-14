using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GIATA_Integration_V2.Models
{
    public class Hotel
    {
        public string GiataID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
    }

    public class TourplanHotel : Hotel
    {
        public string TourplanID { get; set; }
    }

    public class GiataHotel : Hotel
    {
        public string GiataReference { get; set; }
        public string GiataLastUpdate { get; set; }

        public GiataHotel(string givenGiataID, string givenLastUpdate, string givenReference)
        {
            this.GiataID = givenGiataID;
            this.GiataLastUpdate = givenLastUpdate;
            this.GiataReference = givenReference;

        }

    }


}