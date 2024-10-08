﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanGiayOnline.Models.EF
{
    [Table("Category")]
    public class Category : CommonAbstract
    {
        public Category()
        {
            this.News = new HashSet<News>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CategoryId {get; set; }

        [Required(ErrorMessage = "Tên danh mục không để trống")]
        [StringLength(150)]
        public string CategoryName { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        [StringLength(150)]
        public string SeoTitle { get; set; }
        [StringLength(250)]
        public string SeoDescription { get; set; }
        [StringLength(150)]
        public string SeoKeyWords { get; set; }

        [Index(IsUnique = true)]
        public int Position { get; set; }
        public bool IsActive { get; set; }
        public ICollection<News> News { get; set; }
        public ICollection<Posts> Posts { get; set; }

    }
} 