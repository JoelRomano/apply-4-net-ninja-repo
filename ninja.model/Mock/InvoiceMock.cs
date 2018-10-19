using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ninja.model.Entity;

namespace ninja.model.Mock {

    public class InvoiceMock {

        #region Singleton

        private static InvoiceMock _instance = null;

        public static InvoiceMock GetInstance() {

            if(_instance == null)
                _instance = new InvoiceMock();

            return _instance;

        }

        #endregion Singleton

        /// <summary>
        /// Mock DB
        /// </summary>
        private IList<Invoice> _db;

        private InvoiceMock() {

            this._db = new List<Invoice>();

            this.Init();

        }

        private void Init() {
            //Agregamos nuevo registro a la base para la realización de pruebas.
            this._db.Add(new Invoice()
            {
                Id = 4,
                Type = Invoice.Types.A.ToString()
            });

            this._db.Add(new Invoice() {
                Id = 1000,
                Type = Invoice.Types.A.ToString()
            });

            this._db.Add(new Invoice() {
                Id = 1002,
                Type = Invoice.Types.B.ToString()
            });

            Invoice invoice3 = new Invoice() {
                Id = 1001, Type = Invoice.Types.A.ToString()
            };

            invoice3.AddDetail(new InvoiceDetail() { Id = 1, InvoiceId = 3, Amount = 22, Description = "Venta varias", UnitPrice = 98.1 });

            this._db.Add(invoice3);

            this._db.Add(new Invoice() {
                Id = 1004,
                Type = Invoice.Types.A.ToString()
            });


            Invoice invoice5 = new Invoice() {
                Id = 1003, Type = Invoice.Types.A.ToString()
            };

            invoice5.AddDetail(new InvoiceDetail() { Id = 1001, InvoiceId = 5, Amount = 22, Description = "Venta varias", UnitPrice = 98.1 });
            invoice5.AddDetail(new InvoiceDetail() { Id = 1002, InvoiceId = 5, Description = "Venta insumos varios", Amount = 14, UnitPrice = 4.33 });
            invoice5.AddDetail(new InvoiceDetail() { Id = 1003, InvoiceId = 5, Description = "Venta insumos tóner", Amount = 5, UnitPrice = 87 });

            this._db.Add(invoice5);

        }

        public void Delete(Invoice invoice) {

            this._db.Remove(invoice);

        }

        public IList<Invoice> GetAll() {

            return this._db;

        }

        public Invoice GetById(long id) {

            return this._db.Where(x => x.Id == id).FirstOrDefault();

        }

        public void Insert(Invoice item) {

            //Añadimos validación de unicidad para evitar errores en la manipulación
            //de registros de la base de datos.
            if(this.Exists(item.Id)) throw new Exception("Object already exists in the database!");

            this._db.Add(item);

        }

        public Boolean Exists(long id) {

            return this._db.Where(x => x.Id == id).Any();

        }

        public void DeleteDetail(long id) {

            this.GetById(id).DeleteDetails();

        }

        public void AddDetail(long id, IList<InvoiceDetail> detail) {

            foreach(InvoiceDetail item in detail)
                this.GetById(id).AddDetail(item);

        }

        //Genera un ID único automaticamente.
        public long GetNewItemId() {
            long oldId = this._db.Max(p => p.Id);

            long newId = oldId + 1;

            return newId;
        }

    }

}