using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyNews.Core.Data;
using MyNews.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNews.Components
{
    public class FooterComponent : ViewComponent
    {
        private AplicationDbContext context;

        public FooterComponent(AplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync(int count=5)
        {
            var recentNews = await GetRecentNews(count);
            return View(recentNews);
        }

        public async Task<IEnumerable<News>> GetRecentNews(int count)
        {
            
            return await context.News.OrderByDescending(n => n.PublishDate).Take(count).ToListAsync();
                }

    }
}
