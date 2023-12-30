using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;

namespace DataLayer
{
    public class Ticket
    {
        //Properties


        [Key]
        public int TicketID { get; set; }



        [Display(Name = "گروه صفحه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int GroupID { get; set; }


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250)]
        public string Title { get; set; }



        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350)]
        [DataType(DataType.MultilineText)]
        public string Discription { get; set; }

        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [MaxLength(1000)]
        public string Text { get; set; }


        [Display(Name = "بازدید")]
        public int Visit {  get; set; }


        [Display(Name = "تصویر")]
        public string ImageName { get; set; }


        [Display(Name = "اسلایدر")]
        public bool ShowSlider { get; set; }


        [Display(Name = "تاریخ ایجاد")]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime CreateDate { get; set; }



        [Display(Name = "کلمات کلیدی")]
        public string Tags { get; set; }





        //navigation Properties

        public virtual TicketGroup TicketGroupnav { get; set; }
        public virtual List<TicketComment> TicketCommentnav { get; set; }

        public Ticket()
        {
            
        }

    }
}
