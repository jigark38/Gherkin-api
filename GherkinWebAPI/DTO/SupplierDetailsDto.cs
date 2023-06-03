using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.DTO
{
    public class SupplierDetailsDto
    {
        public int ID { get; set; }

        public string supplierOrgID { get; set; }


        public DateTime creationDate { get; set; }


        public string userName { get; set; }


        public string organisationName { get; set; }


        public string legalStatus { get; set; }


        public string address { get; set; }


        public int countryID { get; set; }


        public int stateID { get; set; }
        public int districtID { get; set; }

        public int placeCode { get; set; }

        public int pinCode { get; set; }


        public string mgmName { get; set; }


        public string designation { get; set; }


        public long mgmContactNumber { get; set; }


        public string correspondanceMailID { get; set; }


        public string altCorrespondanceMailID { get; set; }


        public string contactPerson { get; set; }


        public string contactPersonDesignation { get; set; }


        public long? contactPersonNumber { get; set; }


        public string contactPersonMailID { get; set; }


        public long? officeNumber { get; set; }


        public string activity { get; set; }


        public string gstNo { get; set; }


        public string website { get; set; }


        public string licenseNo { get; set; }


        public string bankName { get; set; }


        public string bankBranch { get; set; }


        public long bankActNo { get; set; }


        public string iFSCCode { get; set; }


        public string approvedBy { get; set; }

        public string districtName { get; set; }

        public string countryName { get; set; }

        public string stateName { get; set; }

        public string placeName { get; set; }

    }
}