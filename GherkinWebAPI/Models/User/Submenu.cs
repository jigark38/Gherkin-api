using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GherkinWebAPI.Models
{
    public class SubMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("moduleName")]
        public string ModuleName { get; set; }

        [JsonProperty("moduleShortCut")]
        public string ModuleShortCut { get; set; }

        [JsonProperty("parentId")]

        public int ParentId { get; set; }
    }

    public class SubMenuList
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("moduleName")]
        public string ModuleName { get; set; }

        [JsonProperty("isSelected")]
        public bool IsSelected { get; set; }

    }


    public class SubMenuListV2
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("moduleName")]
        public string ModuleName { get; set; }

        [JsonProperty("isSelected")]
        public bool IsSelected { get; set; }

    }
    public class MasterMenu
    {
       
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("moduleName")]
            public string ModuleName { get; set; }

            [JsonProperty("moduleShortCut")]
            public string ModuleShortCut { get; set; }

            [JsonProperty("menuId")]
            public string MenuId { get; set; }

            [JsonProperty("moduleId")]
            public string ModuleId { get; set; }

            [JsonProperty("subMenuId")]
            public string SubMenuId { get; set; }

            [JsonProperty("pageRoute")]
            public string PageRoute { get; set; }

            [JsonProperty("pageUrl")]
            public string PageUrl { get; set; }
        
    }

    public class Menu
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("moduleName")]
        public string ModuleName { get; set; }

        [JsonProperty("moduleShortCut")]
        public string ModuleShortCut { get; set; }

    }

    public class UserMenu
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("moduleName")]
        public string ModuleName { get; set; }

        [JsonProperty("moduleShortCut")]
        public string ModuleShortCut { get; set; }

        public List<Menu> Children { get; set; }
    }

   
}