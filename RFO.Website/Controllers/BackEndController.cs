// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BackEndController.cs" company="TNL">
//   Copyright (c) TNL, 2016
//   All rights are reserved. Reproduction or transmission in whole or in part, in
//   any form or by any means, electronic, mechanical or otherwise, is prohibited
//   without the prior written consent of the copyright owner.
// </copyright>
// <summary>
//   Defines the BackEndController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.Mvc;
using RFO.AspNet.Utilities.Attribute;

namespace RFO.Website.Controllers
{
    public class BackEndController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [CompressContent]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "MenuManagement", new { Area = "Admin" });
        }
    }
}