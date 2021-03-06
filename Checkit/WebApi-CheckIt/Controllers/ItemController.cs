﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CheckIt.WebApi_CheckIt.Models;
using CheckIt.DataAccessLayer;
using CheckIt.ManagerLayer;
using CheckIt.ServiceLayer;

namespace CheckIt.WebApi_CheckIt.Controllers
{
    public class ItemController : ApiController
    {

        [HttpPost]
        [Route("api/item/update")]
        public HttpResponseMessage UpdateItem([FromBody] ItemDTO item)
        {
            using (var db = new DataBaseContext())
            {
                var response = new HttpResponseMessage();
                var itemManager = new ItemManager(db);
                try
                {
                    //Fix double format
                    item.price = item.price.Replace(@"$", "");

                    itemManager.UpdateItem(item.itemName, Convert.ToDouble(item.price), item.url, item.picKey);
                }catch(Exception)
                {

                }
            }
                return new HttpResponseMessage();
        }

        [HttpPost]
        [Route("api/item/updateandalert")]
        public HttpResponseMessage UpdateItemAndAlert([FromBody] ItemDTO item)
        {
            using (var db = new DataBaseContext())
            {
                var response = new HttpResponseMessage();
                var itemManager = new ItemManager(db);
                try
                {
                    //Fix double format
                    item.price = item.price.Replace(@"$", "");

                    itemManager.UpdateItemAndAlert(item.itemName, Convert.ToDouble(item.price));
                    response.StatusCode = HttpStatusCode.OK;
                    return response;
                }catch(ItemDoesNotExistException e)
                {
                    response.Content = new StringContent(e.Message);
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }
            }
        }


    }
}
