using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace studwebapimvc.Models
{
    
        /// <summary>
        /// класс взвешивания
        /// </summary>
        public class Weight
        {
            //индекс
            public int ID { get; set; }
            //Наименование что взвешивалось 
            public string Name { get; set; }
            //дата взвешивания
            public string Dayofweek { get; set; }
            //вес
            public int Weig { get; set; }
            //единица веса
            public string WeightName { get; set; }
            //тип тары
            public string Namecontainer { get; set; }


        }
    }
