namespace GherkinWebAPI.DTO
{
    public class RawMaterialDetailsDto
    {
        public int ID { get; set; }

        public string Raw_Material_Details_Code { get; set; }

        public string Raw_Material_Group_Code { get; set; }

        public string Raw_Material_Details_Name { get; set; }

        public string Raw_Material_QC_Norms { get; set; }

        public string Raw_Material_UOM { get; set; }

        public decimal Raw_Material_Reorder_Stock { get; set; }

        public int Raw_Material_HSN_CODE_No { get; set; }

        public decimal Raw_Material_IGST_Rate { get; set; }

        public decimal Raw_Material_CGST_Rate { get; set; }

        public decimal Raw_Material_SGST_Rate { get; set; }

        public decimal Raw_Material_Cess_Rate { get; set; }

        public RawMaterialMasterDto RawMaterialGroupMaster { get; set; }
    }
}