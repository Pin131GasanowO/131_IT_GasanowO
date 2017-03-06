using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComputerShop.Models.Catalog
{
    public class CatalogManager
    {
        private static int _lastProductId = 0;
        private static Dictionary<int, Article> _articlesStore = new Dictionary<int, Article>();

        public IEnumerable<Article> GetArticles()
        {
            return _articlesStore.Select(a => a.Value).ToArray();
        }

        public Article GetArticleById(int id)
        {
            return _articlesStore.ContainsKey(id) ? _articlesStore[id] : null;
        }

        public void AddArticle(Article article)
        {
            while (article.Id == null || _articlesStore.ContainsKey((int)article.Id))
            {
                article.Id = ++_lastArticleId;
            }
            _articlesStore[(int)article.Id] = article;
        }
    }
}