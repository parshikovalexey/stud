using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using studwebapimvc.Models;
using System.Web.Http.Results;
using System.Web.Http.Description;

namespace studwebapimvc.Controllers
{
    
        public class WeightController : ApiController
    {
        //Лист объектов класса Weight
        public List<Weight> data = new List<Weight>
        {
           new Weight
           {
               ID =1,
               Name ="Медь",
               Dayofweek = "1.07.17",
               Weig = 17,
               WeightName = "Тонна",
               Namecontainer = "Контейнер"
           },
           new Weight
           {
               ID =2,
               Name ="Железо",
               Dayofweek = "2.07.17",
               Weig = 17,
               WeightName = "Тонна",
               Namecontainer = "Контейнер"
           }
        };
        //добавил для добавления Resource Description в API
        [ResponseType(typeof(Weight))]
        //Берем элемент в списке по ID
        public IHttpActionResult Get(int id)
        {
            var weig = data.FirstOrDefault((p) => p.ID == id);
            if (weig == null)
            {
                return NotFound();
            }
            return Ok(weig);
        }
        //получение инф-----------------
        //вывод всей информации
        public IEnumerable<Weight> GetAllWeight()
        {
            return data;
        }
        //получение отдельной инфо
        //добавил для добавления Resource Description в API
        [ResponseType(typeof(Weight))]
        public IHttpActionResult GetWeight(string Name)
        {
            var weig = data.FirstOrDefault((p) => p.Name == Name);
            if (weig == null)
            {
                return NotFound();
            }
            return Ok(weig);
        }
        //------------------------------
        //сохрание----------------------
        //добавил для добавления Resource Description в API
        [ResponseType(typeof(Weight))]
        public IHttpActionResult Post(Weight newData)
        {
            if (newData.Name != null && newData.Weig > 0 &&
                newData.Dayofweek != null && newData.Namecontainer != null)
            {
                newData.ID = data.Count + 1;
                data.Add(newData);
                // вот тут не уверен
                return Created("DefaultApi", newData);
            }
            return BadRequest();

        }
        //------------------------------
        //обновление
        [ResponseType(typeof(Weight))]
        public IHttpActionResult Put(int id, Weight t)
        {
            // проверяю поля 
            if (t != null && t.ID == id && t.Weig > 0)
            {
                var temp = data.FirstOrDefault((p) => p.ID == id);
                // проверяю есть ли в temp что-то и разрешаю доступ на запись
                if (temp != null)
                {
                    temp.Name = t.Name;
                    temp.Dayofweek = t.Dayofweek;
                    temp.Namecontainer = t.Namecontainer;
                    return Ok(temp);
                }
                else
                    return NotFound();
            }
            else
                return BadRequest();


        }


    }
}

