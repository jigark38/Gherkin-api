
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class ProformaInvoice
    {
        public ProformaInvoice()
        {
            ProductionDetails = new List<ProductionDetails>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [JsonProperty("profInvNo")]
        public string Prof_Inv_No { get; set; }

        [JsonProperty("profInvDate")]
        public DateTime Prof_Inv_Date { get; set; }

        [JsonProperty("exportDomesticsales")]
        public string Export_Domestic_sales { get; set; }

        [JsonProperty("exportersRefNo")]
        public string Exporters_Ref_No { get; set; }

        [JsonProperty("iGSTSGST")]
        public string IGST_SGST { get; set; }

        [JsonProperty("consigneeCbCode")]
        public string C_B_Code { get; set; }

        [JsonProperty("wCountryId")]
        public string W_Country_Id { get; set; }

        [JsonProperty("buyerCBCode")]
        public string Buyer_C_B_Code { get; set; }

        [JsonProperty("buyerOrderNo")]
        public string Buyer_Order_No { get; set; }

        [JsonProperty("buyerOrderDate")]
        public DateTime Buyer_Order_Date { get; set; }

        [JsonProperty("preCarriageBy")]
        public string Pre_Carriage_By { get; set; }

        [JsonProperty("placeOfReceiptPC")]
        public string Place_of_Receipt_PC { get; set; }

        [JsonProperty("vesselFlightNo")]
        public string Vessel_Flight_No { get; set; }

        [JsonProperty("portOfLoading")]
        public string Port_of_Loading { get; set; }

        [JsonProperty("portOfDischarge")]
        public string Port_of_Discharge { get; set; }

        [JsonProperty("finalDestination")]
        public string Final_Destination { get; set; }

        [JsonProperty("shipmentDate")]
        public DateTime Shipment_Date { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("paymentTerms")]
        public string Payment_Terms { get; set; }

        [JsonProperty("termsConditions")]
        public string Terms_Conditions { get; set; }

        [JsonProperty("bankCode")]
        public string Bank_Code { get; set; }

        [JsonProperty("totalNos")]
        public int Total_Nos { get; set; }

        [JsonProperty("profInvoiceAmount")]
        public decimal Prof_Invoice_Amount { get; set; }

        [JsonProperty("oceanFreight")]
        public decimal Ocean_Freight { get; set; }

        [JsonProperty("totalProfInvoiceAmount")]
        public decimal Total_Prof_Invoice_Amount { get; set; }

        [JsonProperty("totalNetWtKgs")]
        public decimal Total_Net_Wt_Kgs { get; set; }

        [JsonProperty("totalGrossWtKgs")]
        public decimal Total_Gross_Wt_Kgs { get; set; }

        [JsonProperty("employeeID")]
        public string Employee_ID { get; set; }

        [JsonProperty("approvedEmployeeId")]
        public string Approved_Employee_Id { get; set; }

        [JsonProperty("productionDetails")]
        public List<ProductionDetails> ProductionDetails { get; set; }
    }

}