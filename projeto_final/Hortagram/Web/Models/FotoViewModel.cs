using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class FotoViewModel
    {

        public FotoViewModel()
        {
            this.USUARIO1 = new HashSet<UsuarioViewModel>();
        }

        public int FOTO_ID { get; set; }
        public int USUARIO_ID { get; set; }
        public string FOTO_URL { get; set; }
        [Display(Name = "Comentário")]
        public string CAPTION { get; set; }

        public virtual UsuarioViewModel USUARIO { get; set; }

        public virtual ICollection<UsuarioViewModel> USUARIO1 { get; set; }
    }
}