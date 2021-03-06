﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRMWebApp.Models;
using CRMWebApp.ActionFilter;
using System.Data.Entity.Validation;

namespace CRMWebApp.Controllers
{
    [HandleError(ExceptionType = typeof(DbEntityValidationException), View = "Error_DbEntityValidationException")]
    public class 客戶資料Controller : Controller
    {
        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();
        

        //private 客戶資料Entities db = new 客戶資料Entities();
        [ExecutionTiming]
        // GET: 客戶資料
        public ActionResult Index(string search, int? 客戶分類)
        {
            //var data = db.客戶資料.Include(p => p.客戶名稱);

            //if (!string.IsNullOrEmpty(search))
            //{
            //    data = db.客戶資料.Where(p => p.客戶名稱.Contains(search));
            //}

            //var data = repo.All().Where(p => p.客戶名稱.Contains(search));
            var data = repo.Get所有資料依據姓名查詢條件(search,10);
            data = data.OrderByDescending(p => p.客戶名稱).Take(10);

            if (客戶分類.HasValue)
                data = data.Where(p => p.客戶分類== 客戶分類);

            var options = (from p in data select p.客戶分類).Distinct().OrderBy(p => p).ToList();
            ViewBag.客戶分類 = new SelectList(options);

            return View(data);
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類,是否已刪除")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶資料);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類,是否已刪除")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                var db = repo.UnitOfWork.Context;
                db.Entry(客戶資料).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = repo.Find(id);
            //db.客戶資料.Remove(客戶資料);
            客戶資料.是否已刪除 = true;
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");

        }

        public ActionResult 客戶明細()
        {
            vw_CustomerContactAndBankListRepository repo2 = RepositoryHelper.Getvw_CustomerContactAndBankListRepository();
            //var data = db.vw_CustomerContactAndBankList.OrderByDescending(p => p.客戶名稱).Take(10).ToList();
            var data = repo2.All().OrderByDescending(p => p.客戶名稱).Take(10).ToList();
            return View(data);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
